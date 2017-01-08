using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace EasyRecipes.Common
{
    class StorageHelper
    {
        public static void AddKeyValue(string key, object value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }
        public static T GetKeyValueFromJson<T>(string key) where T : class
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
                return JsonConvert.DeserializeObject<T>( ApplicationData.Current.LocalSettings.Values[key].ToString());
            return null;
        }
        public static string GetKeyValue(string key) 
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
                return ApplicationData.Current.LocalSettings.Values[key].ToString();
            return "";
        }
    }
}
