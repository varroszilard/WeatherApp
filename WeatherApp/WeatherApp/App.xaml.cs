using DAL.Connection;
using DAL.Repository.Location;
using PCLAppConfig;
using SQLite.Net;
using System;
using WeatherApp.BackgroundServices;
using WeatherApp.Helper;
using WeatherApp.View.Master;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace WeatherApp
{
	public partial class App : Application
    { 
		public App ()
		{
			InitializeComponent();

            ConfigurationManager.Initialise(PCLAppConfig.FileSystemStream.PortableStream.Current);

            MainPage = new MasterWeather();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
            StopRefreshWeather();
        }

		protected override void OnResume ()
		{
            MessagingCenter.Send(new StartRefreshWeather(), typeof(StartRefreshWeather).FullName);

        }

        private void StopRefreshWeather()
        {
            MessagingCenter.Send(new StopRefreshWeather(), typeof(StopRefreshWeather).FullName);
        }
    }
}
