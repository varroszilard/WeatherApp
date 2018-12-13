using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using UIKit;
using WeatherApp.Common;
using WeatherApp.Helper;
using WeatherApp.iOS.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteHelper))]
namespace WeatherApp.iOS.Helper
{
    public class SQLiteHelper : ISQLiteHelper
    {
        public ISQLitePlatform GetPlatform()
        {
            return new SQLitePlatformIOS();
        }
    }
}