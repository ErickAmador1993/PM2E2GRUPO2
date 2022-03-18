using PM2E2GRUPO_4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E2GRUPO_4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UbicacionesPage : ContentPage
    {
        public UbicacionesPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private  void tlbeliminar_Clicked(object sender, EventArgs e)
        {

           

        }

        private async void tlbmostrar_Clicked(object sender, EventArgs e)
        {
            var lat = Convert.ToDouble(txtlatitud.Text);
            var log = Convert.ToDouble(txtlongitud.Text);
            var descricion = txtdcorta.Text;


            MapLaunchOptions options = new MapLaunchOptions { Name = descricion };
            await Map.OpenAsync(lat, log, options);
        }

        private async void tlbver_Clicked(object sender, EventArgs e)
        {

            var page = new Views.MapPage();
            await Navigation.PushAsync(page);
            //var Gps = CrossGeolocator.Current;
            //if (Gps.IsGeolocationEnabled)//Servicio de Geolocalizacion existente
            //{


            //    if (Gps.IsGeolocationEnabled)//VALIDA QUE EL GPS ESTA ENCENDIDO
            //    {


        }
    }
}