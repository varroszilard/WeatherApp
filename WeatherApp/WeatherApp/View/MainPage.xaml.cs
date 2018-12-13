using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.BackgroundServices;
using WeatherApp.Helper;
using WeatherApp.Model;
using WeatherApp.View;
using WeatherApp.ViewModel;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm;

        public MainPage()
        {
            vm = new MainPageViewModel();

            BindingContext = vm;
            
            MessagingCenter.Subscribe<MainPageViewModel, string>(this, "LocationNotFound", (sender, arg) =>
            {
                DisplayAlert($"{arg} not found!", "Please try again later...", "Ok");
            });

            HandleReceivedMessages();

            InitializeComponent();
        }

        private void StartRefreshWeather()
        {
            MessagingCenter.Send<StartRefreshWeather>(new StartRefreshWeather(), typeof(StartRefreshWeather).FullName);
        }

        private void StopRefreshWeather()
        {
            MessagingCenter.Send<StopRefreshWeather>(new StopRefreshWeather(), typeof(StopRefreshWeather).FullName);
        }


        private async void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<RefreshWeather>(this, typeof(RefreshWeather).FullName, sender =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    vm.GetWeatherAsync();
                });
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (CrossConnectivity.Current.IsConnected)
            {
                await vm.Init();
            }
            else
            {
                await DisplayAlert("No internet connectivity!", "Please try again later...", "Ok");
            }

            StartRefreshWeather();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            StopRefreshWeather();
        }

        private async void OnChangeLocation(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectLocation());
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                //api call to refresh weather
                vm.GetWeatherAsync();
            }
            else
            {
                await DisplayAlert("No internet connectivity!", "Please try again later...", "Ok");
            }
        }
    }
}
