using DAL.Repository.Location;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DAL.Entities
{
    public class Weather
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public int Temperature { get; set; }
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
        public string WeatherState { get; set; }
        public string Image { get; set; }
        public Guid LocationId { get; set; }

        //lazy loading
        private Location _location;
        [Ignore]
        public Location Location
        {
            get
            {
                if (_location == null)
                {
                    _location = DependencyService.Get<ILocationRepository>().GetById(LocationId);
                }
                return _location;
            }
        }
    }
}
