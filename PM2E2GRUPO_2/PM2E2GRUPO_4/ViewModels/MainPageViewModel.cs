using PM2E2GRUPO_4.Controller;
using PM2E2GRUPO_4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PM2E2GRUPO_4.ViewModels
{
    public class MainPageViewModel
    {
        public int codigo { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string descripcion { get; set; }
        // BaseFirebase firebaseHelper = new BaseFirebase();
        FirebaseHelper services;
        private Command AddUbicacionCommand { get; }
        private ObservableCollection<Ubicaciones> _ubicaciones = new ObservableCollection<Ubicaciones>();
        public ObservableCollection<Ubicaciones> Ubicaciones
        {
            get { return _ubicaciones; }
            set
            {
                _ubicaciones = value;
             // OnPropertyChanged();
            }
        }
        //public ObservableCollection<Models.Ubicaciones>Products { get; set; }

       public MainPageViewModel()
        {
            services = new FirebaseHelper();
            Ubicaciones = services.getUbicaciones();
            AddUbicacionCommand = new Command(async() => await addUbicacionAsync(latitud,longitud, descripcion));

       
        }
        public async Task addUbicacionAsync(string latitud,string longitud, string descripcion)
        {
            await services.AddUbicacion(latitud,longitud, descripcion);
        }

    }
}
