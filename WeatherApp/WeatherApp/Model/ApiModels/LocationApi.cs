using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Model.ApiModels
{
    public class LocationApi
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("location_type")]
        public string Location_type { get; set; }
        [JsonProperty("woeid")]
        public int Woeid { get; set; }
        [JsonProperty("latt_long")]
        public string Latt_long { get; set; }
    }
}
