using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Model.ApiModels
{
    public class Source
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public int Crawl_rate { get; set; }
    }
}
