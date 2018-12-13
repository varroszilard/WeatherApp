using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using WeatherApp.Common;
using WeatherApp.Droid.Helper;
using WeatherApp.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteHelper))]
namespace WeatherApp.Droid.Helper
{
    public class SQLiteHelper : ISQLiteHelper
    {
        public ISQLitePlatform GetPlatform()
        {
            return new SQLitePlatformAndroidN();
        }
    }
}