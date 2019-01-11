using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace WeatherApp.BackgroundServices
{
    public class RefreshWeatherTaskService
    {
        public async Task RunTask(CancellationToken token)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();

                    await Task.Delay(10000);
                    //Vibration.Vibrate(TimeSpan.FromMilliseconds(10));

                    //refresh weather here
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MessagingCenter.Send<RefreshWeather>(new RefreshWeather(), typeof(RefreshWeather).FullName);
                    });
                }
            });
        }
    }
}
