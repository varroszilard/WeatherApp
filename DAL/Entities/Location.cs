using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Location
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public int Woeid { get; set; }
        public string Name { get; set; }

        [Default(value: false)]
        public bool IsCurrent { get; set; }
        public bool ToBeDeleted { get; set; }
    }
}
