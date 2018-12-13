using DAL.Repository.Base;
using DAL.Repository.Weather;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(WeatherRepository))]
namespace DAL.Repository.Weather
{
    public class WeatherRepository : BaseRepository<Entities.Weather>, IWeatherRepository
    {
    }
}
