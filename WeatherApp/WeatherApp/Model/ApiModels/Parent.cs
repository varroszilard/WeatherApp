using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Model.ApiModels
{
    public class Parent
    {
        public List<LocationApi> Locations { get; set; }
    }
}
