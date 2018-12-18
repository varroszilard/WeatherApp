using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Helper;
using WeatherApp.Model;
using WeatherApp.Model.ApiModels;
using Plugin.Connectivity;
using DAL.Repository.Location;
using Xamarin.Forms;
using DAL.Repository.Weather;
using DAL.Entities;

namespace WeatherApp.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private ILocationRepository locationRepository;

        public MainPageViewModel()
        {

        }

        public async Task Init()
        {
            try
            {
                IsVisible = true;

                locationRepository = DependencyService.Get<ILocationRepository>();

                if (!locationRepository.GetAll().Any())
                {
                    locationRepository.InitDatabase();
                }

                var location = locationRepository.GetAll().FirstOrDefault(l => l.IsCurrent);

                if (location == null || location.Name == null)
                {
                    location = await SetDefaultLocation();
                }

                if (location.Woeid == 0)
                {
                    var currLocation = await GetLocationWoeid(location.Name);

                    if (currLocation == null || currLocation.Woeid == 0)
                    {
                        MessagingCenter.Send<MainPageViewModel, string>(this, "LocationNotFound", location.Name);

                        locationRepository.DeselectCurrentLocation();

                        location = await SetDefaultLocation();
                    }
                    else
                    {
                        location.Woeid = currLocation.Woeid;

                        locationRepository.Update(location);
                    }
                }

                Name = location.Name;

                GetWeatherAsync();
            }
            finally
            {
                IsVisible = false;
            }
        }

        public string Name { get; set; }

        public string Temperature { get; set; }

        public string Image { get; set; }

        public string WeatherState { get; set; }

        public bool IsVisible { get; set; }

        public string MinTemp { get; set; }
        public string MaxTemp { get; set; }

        public async Task<DAL.Entities.Location> GetLocationWoeid(string _location)
        {
            try
            {
                IsVisible = true;

                var location = await LocationHelper.GetLocationWoeid(_location);

                return location ?? null;
            }
            finally
            {
                IsVisible = false;
            }
        }

        public async Task GetWeatherAsync()
        {
            try
            {
                IsVisible = true;

                var weather = await LocationHelper.GetWeatherAsync();

                Name = weather.Location.Name;
                Temperature = $"{weather.Temperature}{"°C"}";
                Image = weather.Image;
                WeatherState = weather.WeatherState;
                MinTemp = $"{weather.MinTemp}°C";
                MaxTemp = $"{weather.MaxTemp}°C";
            }
            finally
            {
                IsVisible = false;
            }
        }

        private async Task<DAL.Entities.Location> SetDefaultLocation()
        {
            try
            {
                IsVisible = true;

                var tempLocation = Mapper.Map.Mapper(await GetLocationWoeid("Bucharest"));

                var location = locationRepository.GetAll().FirstOrDefault(l => l.Name.Equals("Bucharest"));

                location.IsCurrent = true;
                location.Woeid = tempLocation.Woeid;

                locationRepository.Update(location);

                return location;
            }
            finally
            {
                IsVisible = false;
            }
        }

        private bool IsLocationDatabaseEmpty()
        {
            return !locationRepository.GetAll().Any();
        }
    }
}
