using Firebase.Database;
using Firebase.Database.Query;
using PM2E2GRUPO_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2E2GRUPO_4.Controller
{
    public class BaseFirebase
    {

        FirebaseClient firebase = new FirebaseClient("https://exameniip-afdaa-default-rtdb.firebaseio.com/");

        public async Task<List<Ubicaciones>> GetAllPersons()
        {

            return (await firebase
              .Child("ExamenBD")
              .OnceAsync<Ubicaciones>()).Select(item => new Ubicaciones
              {
                  latitud = item.Object.latitud,
                  longitud = item.Object.longitud,
                  descripcion = item.Object.descripcion,
                  DescripcionCorta = item.Object.DescripcionCorta
              }).ToList();
        }

        public async Task AddPerson(string personId, string log)
        {

            await firebase
              .Child("ExamenBD")
              .PostAsync(new Ubicaciones() { latitud = personId, longitud = log });
        }

        public async Task<Ubicaciones> GetPerson(string personId)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("ExamenBD")
              .OnceAsync<Ubicaciones>();
            return allPersons.Where(a => a.latitud == personId).FirstOrDefault();
        }

        public async Task UpdatePerson(string personId, string name)
        {
            var toUpdatePerson = (await firebase
              .Child("ExamenBD")
              .OnceAsync<Ubicaciones>()).Where(a => a.Object.latitud == personId).FirstOrDefault();

            await firebase
              .Child("ExamenBD")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Ubicaciones() { latitud = personId, longitud = name });
        }

        public async Task DeletePerson(string personId)
        {
            var toDeletePerson = (await firebase
              .Child("ExamenBD")
              .OnceAsync<Ubicaciones>()).Where(a => a.Object.latitud == personId).FirstOrDefault();
            await firebase.Child("ExamenBD").Child(toDeletePerson.Key).DeleteAsync();

        }


    }
}
