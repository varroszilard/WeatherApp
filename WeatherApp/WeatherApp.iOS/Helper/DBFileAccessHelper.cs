using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using WeatherApp.Common;
using WeatherApp.iOS.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(DBFileAccessHelper))]
namespace WeatherApp.iOS.Helper
{
    public class DBFileAccessHelper : IDBFileAccessHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var library = System.IO.Path.Combine(path, "..", "Library");

            if (!System.IO.Directory.Exists(library))
            {
                System.IO.Directory.CreateDirectory(library);
            }

            return System.IO.Path.Combine(library, filename);
        }
    }
}