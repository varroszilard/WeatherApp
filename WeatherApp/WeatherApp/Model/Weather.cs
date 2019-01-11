using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Model
{
    public class Weather
    {
        public string Name { get; set; }
        public string Temperature { get; set; }
        public string Image { get; set; }
        public string WeatherState { get; set; }
        public string MinTemp { get; set; }
        public string MaxTemp { get; set; }
    }
}
