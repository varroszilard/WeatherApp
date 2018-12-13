using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Common
{
    public interface ISQLiteHelper
    {
        ISQLitePlatform GetPlatform();
    }
}
