using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using Xamarin.Forms;
using DAL.Repository.Location;
using DAL.Repository.Weather;
using static WeatherApp.Collection.Enumeration;
using WeatherApp.Helper;

namespace WeatherApp.ViewModel
{
    public class SetLocationViewModel : BaseViewModel
    {
        private ILocationRepository locationRepository;
        public ObservableCollection<Location> LocationList { get; set; } = new ObservableCollection<Location>();
        public INavigation Navigation { get; set; }

        public async Task Init(INavigation navigation)
        {
            Navigation = navigation;

            await Task.Run(() =>
            {
                locationRepository = DependencyService.Get<ILocationRepository>();
                var list = locationRepository.GetAll().ToList().OrderBy(o => o.Name);

                foreach (var item in list)
                {
                    LocationList.Add(new Location { Name = item.Name, IsCurrent = item.IsCurrent });
                }
            });
        }

        public async Task SetLocation(Location location)
        {
            await Task.Run(() =>
            {
                var locationRepository = DependencyService.Get<ILocationRepository>();
                var weatherRepository = DependencyService.Get<IWeatherRepository>();

                weatherRepository.DeleteAll();

                locationRepository.DeselectCurrentLocation();

                var loc = locationRepository.GetAll().FirstOrDefault(l => l.Name.Equals(location.Name));

                loc.IsCurrent = true;

                locationRepository.Update(loc);
            });
        }

        public async Task<LocationStatus> AddNewLocation(string _location)
        {

            var locationRepository = DependencyService.Get<ILocationRepository>();

            var locationDb = locationRepository.GetAll().Where(l => l.Name.ToUpper().Equals(_location.ToUpper())).FirstOrDefault();

            if (locationDb == null)
            {
                var locationApi = await LocationHelper.GetLocationWoeid(_location, Navigation);

                if (locationApi != null)
                {
                    LocationHelper.SaveNewLocation(locationApi);

                    await LocationHelper.GetWeatherAsync();

                    return LocationStatus.Found;
                }
            }
            else
            {
                return LocationStatus.AlreadyExists;
            }

            return LocationStatus.NotFound;
        }
    }
}
