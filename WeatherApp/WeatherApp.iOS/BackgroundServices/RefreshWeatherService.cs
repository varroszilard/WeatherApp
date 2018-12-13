using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using WeatherApp.BackgroundServices;
using Xamarin.Forms;

namespace WeatherApp.iOS.BackgroundServices
{
    public class RefreshWeatherService
    {
        nint _taskId;
        CancellationTokenSource _cts;

        public async Task Start()
        {
            _cts = new CancellationTokenSource();

            _taskId = UIApplication.SharedApplication.BeginBackgroundTask("LongRunningTask", OnExpiration);

            try
            {
                var refreshTask = new RefreshWeatherTaskService();
                await refreshTask.RunTask(_cts.Token);
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

            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        private void OnExpiration()
        {
            _cts.Cancel();
        }
    }
}