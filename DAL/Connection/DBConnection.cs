using DAL.Entities;
using DAL.Repository.Base;
using DAL.Repository.Location;
using DAL.Repository.Weather;
using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Common;
using Xamarin.Forms;

namespace DAL.Connection
{
    public class DBConnection
    {
        private static SQLiteConnection _connection;

        public static SQLiteConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SQLiteConnection(DependencyService.Get<ISQLiteHelper>().GetPlatform(), DependencyService.Get<IDBFileAccessHelper>().GetLocalFilePath("weather.db3"));

                DependencyService.Get<LocationRepository>().CreateTable();
                DependencyService.Get<WeatherRepository>().CreateTable();
            }

            return _connection;
        }
    }
}
