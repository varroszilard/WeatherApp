
using System;

namespace WeatherApp.Mapper
{
    public class Map
    {
        public static DAL.Entities.Location Mapper(Model.Location _location)
        {
            var location = new DAL.Entities.Location
            {
                Id = Guid.NewGuid(),
                Name = _location.Name,
                Woeid = _location.Woeid
            };

            return location;
        }

        public static Model.Location Mapper(DAL.Entities.Location _location)
        {
            var location = new Model.Location
            {
                Name = _location.Name,
                Woeid = _location.Woeid
            };

            return location;
        }

        public static Model.Weather Mapper(DAL.Entities.Weather _weather)
        {
            return new Model.Weather
            {
                Image = _weather.Image,
                MaxTemp = $"{_weather.MaxTemp.ToString()}°C",
                MinTemp = $"{_weather.MinTemp.ToString()}°C",
                Temperature = $"{_weather.Temperature.ToString()}°C",
                WeatherState = _weather.WeatherState
            };
        }
    }
}
