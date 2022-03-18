using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Plugin.Geolocator;
using System.Threading.Tasks;
using Xamarin.Forms;
using PM2E2GRUPO_4.Controller;
using Plugin.Media;
using Xamarin.Essentials;
using System.IO;
using Plugin.Media.Abstractions;
using System.Diagnostics;
using Firebase.Storage;

namespace PM2E2GRUPO_4
{
    public partial class MainPage : ContentPage
    {
        MediaFile File;
        BaseFirebase firebaseHelper = new BaseFirebase();
        FirebaseHelper firebaseHelperq = new FirebaseHelper();
        public MainPage()
        {
            InitializeComponent();
        }


        public async void GetLocation()
        {
            Xamarin.Essentials.Location Location = await Geolocation.GetLastKnownLocationAsync();

            if (Location == null)
            {
                Location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                }); ;
            }

            txtlatitud.Text = Location.Latitude.ToString();
            txtlongitud.Text = Location.Longitude.ToString();


        }
        private void btn01_Clicked(object sender, EventArgs e)
        {

            GetLocation();
        }

        private async void btn02_Clicked(object sender, EventArgs e)
        {
            // var ubicaciones = new Views.ubicacionesPage();
            // await NavigationPage.PushAsync(ubicaciones);
            await Navigation.PushModalAsync(new Views.UbicacionesPage());


        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {

            await firebaseHelperq.AddPerson(txtlatitud.Text, txtlongitud.Text, txtdescripcion.Text, txtdcorta.Text, image1.ToString());

            txtlatitud.Text = string.Empty;
            txtlongitud.Text = string.Empty;
            await DisplayAlert("Success", "Person Added Successfully", "OK");
            await FirebaseHelper.UploadFile(File.GetStream(), Path.GetFileName(File.Path));


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                File = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (File == null)
                    return;
                image1.Source = ImageSource.FromStream(() =>
                {
                 var imageStram = File.GetStream();
                    return imageStram;
                });

              
               // StreamReader reader = new StreamReader(imageStram);
               // imageStram1 = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private  void Button_Clicked_1(object sender, EventArgs e)
        {
            

        }

        private async void ImagenEli()
        {
            await CrossMedia.Current.Initialize();
            try
            {

                image1.Source = ImageSource.FromResource("images.jpg");
                File = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (File == null)
                    return;
                image1.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = File.GetStream();
                    return imageStram;
                });
                await StoreImages(File.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<string> StoreImages(Stream imageStream)
        {
            var stroageImage = await new FirebaseStorage("gs://exameniip-afdaa.appspot.com")
                .Child("ExamenImagenes")
                .Child("image.jpg")
                .PutAsync(imageStream);
            string imgurl = stroageImage;
            return imgurl;
        }
    }
}
