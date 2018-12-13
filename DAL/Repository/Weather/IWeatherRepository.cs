using DAL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Weather
{
    public interface IWeatherRepository : IBaseRepository<Entities.Weather>
    {
    }
}
