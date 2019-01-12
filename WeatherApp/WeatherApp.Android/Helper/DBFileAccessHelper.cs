using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WeatherApp.Common;
using WeatherApp.Droid.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(DBFileAccessHelper))]
namespace WeatherApp.Droid.Helper
{
    public class DBFileAccessHelper : IDBFileAccessHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            return System.IO.Path.Combine(path, filename);
        }
    }
}