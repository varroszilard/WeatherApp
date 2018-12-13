using DAL.Entities;
using DAL.Repository.Location;
using DAL.Repository.Weather;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model.ApiModels;
using WeatherApp.View;
using Xamarin.Forms;

namespace WeatherApp.Helper
{
    public static class LocationHelper
    {
        public async static Task<Weather> GetWeatherAsync()
        {
            var locationRepository = DependencyService.Get<ILocationRepository>();
            var location = locationRepository.GetAll().FirstOrDefault(l => l.IsCurrent);

            var woeid = location.Woeid;

            string url = String.Empty;

            try
            {
                url = Path.Combine(ConfigurationManager.AppSettings["BaseURL"], ConfigurationManager.AppSettings["Woeid"]);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            RootObject weatherDetails = await NetAdapter.Instance.GetAsync<RootObject>(url, new { woeid });

            return SaveWeather(weatherDetails.Consolidated_weather.OrderBy(date => date.Applicable_date).FirstOrDefault(), location);
        }

        public async static Task<Location> GetLocationWoeid(string query, INavigation navigation = null)
        {
            try
            {
                var url = Path.Combine(ConfigurationManager.AppSettings["BaseURL"], ConfigurationManager.AppSettings["Search"]);

                List<LocationApi> locationDetails = await NetAdapter.Instance.GetListAsync<LocationApi>(url, new { query });

                if (locationDetails == null)
                    return null;

                var location = locationDetails.FirstOrDefault();

                if (locationDetails.Count > 1)
                {
                    await navigation.PushAsync(new SelectOneLocation());
                }

                return new Location
                {
                    Name = location.Title,
                    Woeid = location.Woeid
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static void SaveNewLocation(Location location)
        {
            var locationRepository = DependencyService.Get<ILocationRepository>();

            locationRepository.DeselectCurrentLocation();

            location.Id = Guid.NewGuid();
            location.IsCurrent = true;

            locationRepository.Insert(location);

            var locationTemp = locationRepository.GetAll().FirstOrDefault(l => l.IsCurrent);
        }

        private static Weather SaveWeather(ConsolidatedWeather weather, Location location)
        {
            var weatherRepository = DependencyService.Get<IWeatherRepository>();

            var weatherToDb = new Weather
            {
                Id = Guid.NewGuid(),
                Image = weather.Weather_state_abbr + ".png",
                LocationId = location.Id,
                Temperature = (int)weather.The_temp,
                WeatherState = weather.Weather_state_name,
                MinTemp = (int) weather.Min_temp,
                MaxTemp = (int) weather.Max_temp
            };

            weatherRepository.Insert(weatherToDb);

            return weatherToDb;
        }
    }
}
