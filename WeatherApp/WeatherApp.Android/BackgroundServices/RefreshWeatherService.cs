using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using WeatherApp.BackgroundServices;
using Xamarin.Forms;

namespace WeatherApp.Droid.BackgroundServices
{
    [Service]
    public class RefreshWeatherService : Service
    {
        private CancellationTokenSource _cts;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            Task.Run(() => {
                try
                {
                    var refreshTask = new RefreshWeatherTaskService();
                    refreshTask.RunTask(_cts.Token).Wait();
                }
                catch (Exception e) { }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        Device.BeginInvokeOnMainThread(
                            () => MessagingCenter.Send(new StopRefreshWeather(), typeof(StopRefreshWeather).FullName)
                        );
                    }
                }
            }, _cts.Token);

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();

                _cts.Cancel();
            }
            base.OnDestroy();
        }
    }
}