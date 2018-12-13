using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Android.Content;
using WeatherApp.BackgroundServices;
using WeatherApp.Droid.BackgroundServices;
using PCLAppConfig;

namespace WeatherApp.Droid
{
    [Activity(Label = "WeatherApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

            LoadApplication(new App());

            WireUpLongRunningTask();
        }

        private void WireUpLongRunningTask()
        {
            MessagingCenter.Subscribe<StartRefreshWeather>(this, typeof(StartRefreshWeather).FullName, message =>
            {
                var intent = new Intent(this, typeof(RefreshWeatherService));
                StartService(intent);
            });

            MessagingCenter.Subscribe<StopRefreshWeather>(this, typeof(StopRefreshWeather).FullName, message =>
            {
                var intent = new Intent(this, typeof(RefreshWeatherService));
                StopService(intent);
            });
        }
    }
}

