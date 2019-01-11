using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherApp.View.Master;

namespace WeatherApp.ViewModel
{
    class MasterWeatherMasterViewModel : BaseViewModel
    {
        public ObservableCollection<MasterWeatherMenuItem> MenuItems { get; set; }

        public MasterWeatherMasterViewModel()
        {
            MenuItems = new ObservableCollection<MasterWeatherMenuItem>(new[]
            {
                    new MasterWeatherMenuItem { Id = 0, Title = "Show weather" },
                    new MasterWeatherMenuItem { Id = 1, Title = "Select location" },
            });
        }
    }
}
