using System;
using Xamarin.Forms;
using Plugin.Geolocator;
using Xamarin.Essentials;

namespace GeolocalizadorXM
{
    public partial class MainPage : ContentPage
    {
        double latitud;
        double longitud;

        public MainPage()
        {
            InitializeComponent();
            IniciarGPS();
        }

        private async void IniciarGPS()
        {
            var geolocator = CrossGeolocator.Current;
            geolocator.DesiredAccuracy = 50;

            if (geolocator.IsGeolocationEnabled)
            {
                if (!geolocator.IsListening)
                {
                    await geolocator.StartListeningAsync(TimeSpan.FromSeconds(1), 5);
                }

                geolocator.PositionChanged += (cambio, args) =>
                {
                    var loc = args.Position;
                    lon.Text = loc.Longitude.ToString();
                    longitud = loc.Longitude;

                    lat.Text = loc.Latitude.ToString();
                    latitud = loc.Latitude;
                };
            }
            else
            {
                // Handle the case where geolocation is not enabled.
                await DisplayAlert("Error", "El GPS no está habilitado", "OK");
            }
        }

        private async void MostrarMapa(object sender, EventArgs args)
        {
            if (latitud != 0 && longitud != 0)
            {
                MapLaunchOptions options = new MapLaunchOptions { Name = "Mi Posicion Actual" };
                await Map.OpenAsync(latitud, longitud, options);
            }
            else
            {
                await DisplayAlert("Error", "No se ha podido obtener la ubicación actual.", "OK");
            }
        }
    }
}
