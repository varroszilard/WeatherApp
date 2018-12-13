using DAL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Location
{
    public interface ILocationRepository : IBaseRepository<Entities.Location>
    {
        void DeselectCurrentLocation();
        void InitDatabase();
    }
}
