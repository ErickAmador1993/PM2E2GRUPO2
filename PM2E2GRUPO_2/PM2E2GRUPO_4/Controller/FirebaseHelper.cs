using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using PM2E2GRUPO_4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2E2GRUPO_4.Controller
{
   public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://exameniip-afdaa-default-rtdb.firebaseio.com/ExamenBD");
        static FirebaseStorage stroageImage = new FirebaseStorage("exameniip-afdaa.appspot.com");

        public async Task AddPerson(String personId, string name, string descripcion, string DescripcionCorta, string img)
        {

            await firebase
              .Child("ExamenBD")
              .PostAsync(new Ubicaciones() { latitud = personId, longitud = name, descripcion= descripcion, DescripcionCorta = DescripcionCorta, img= img });
        }

        public static async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            var imageUrl = await stroageImage
                .Child("ExamenImagenes")
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }

        public async Task AddUbicacion(string latitud, string longitud, string descripcion)
        {
            Ubicaciones i = new Ubicaciones() { latitud = latitud, longitud = longitud, descripcion = descripcion };
            await firebase.Child("ExamenBD")
                .PostAsync(i);
        }

        public ObservableCollection<Ubicaciones> getUbicaciones()
        {
            var itemData = firebase.Child("ExamenBD").AsObservable<Ubicaciones>()
                .AsObservableCollection();
            return itemData;
        }
    }
}
