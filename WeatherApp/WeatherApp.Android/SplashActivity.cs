using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using DAL.Entities;
using DAL.Repository.Location;
using forms = Xamarin.Forms;

namespace WeatherApp.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            
            try
            {
                var locationRepository = forms.DependencyService.Get<ILocationRepository>();
                //locationRepository.DeleteAll();
                if (!locationRepository.GetAll().Any())
                {
                    locationRepository.InitDatabase();
                }
            }
            catch (Exception e)
            {
                Log.Debug(TAG, e.Message);
            }
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });

            

            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            //await Task.Delay(1000); // Simulate a bit of startup work.

            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public override void OnBackPressed() { }
    }
}