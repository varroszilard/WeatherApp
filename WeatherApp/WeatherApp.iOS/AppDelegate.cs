using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using WeatherApp.BackgroundServices;
using WeatherApp.iOS.BackgroundServices;
using Xamarin.Forms;

namespace WeatherApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        RefreshWeatherService refreshWeather;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            LoadApplication(new App());

            WireUpLongRunningTask();

            return base.FinishedLaunching(app, options);
        }

        private void WireUpLongRunningTask()
        {
            MessagingCenter.Subscribe<StartRefreshWeather>(this, typeof(StartRefreshWeather).FullName, async message =>
            {
                refreshWeather = new RefreshWeatherService();
                await refreshWeather.Start();
            });

            MessagingCenter.Subscribe<StopRefreshWeather>(this, typeof(StopRefreshWeather).FullName, message =>
            {
                refreshWeather.Stop();
            });
        }
    }
}
