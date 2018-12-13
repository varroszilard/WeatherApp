using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Model
{
    public class Location
    {
        public string Name { get; set; }
        public int Woeid { get; set; }
        public bool IsCurrent { get; set; }
    }
}
