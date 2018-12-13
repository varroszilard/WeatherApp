using DAL.Connection;
using DAL.Repository.Base;
using DAL.Repository.Location;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;

[assembly: Dependency(typeof(LocationRepository))]
namespace DAL.Repository.Location
{
    public class LocationRepository : BaseRepository<Entities.Location>, ILocationRepository
    {
        public void DeselectCurrentLocation()
        {
            var location = GetAll().Where(l => l.IsCurrent == true).FirstOrDefault();
            location.IsCurrent = false;
            Update(location);

            var locationList = GetAll().ToList();

            foreach (var item in locationList)
            {
                Console.WriteLine(item);
            }
        }

        public void InitDatabase()
        {
            var list = new List<Entities.Location>
            {
                new Entities.Location { Id = Guid.NewGuid(), Name = "Bucharest" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Budapest" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "New York" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Paris" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Berlin" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Moscow" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Sydney" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Oslo" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "London" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Tokyo" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "San Francisco" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Ottawa" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Anchorage" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Cape Town" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Bogota" },
                new Entities.Location { Id = Guid.NewGuid(), Name = "Buenos Aires" }
            };

            InsertAll(list);
        }
    }
}
