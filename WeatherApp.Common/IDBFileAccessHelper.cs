using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Common
{
    public interface IDBFileAccessHelper
    {
        string GetLocalFilePath(string filename);
    }
}
