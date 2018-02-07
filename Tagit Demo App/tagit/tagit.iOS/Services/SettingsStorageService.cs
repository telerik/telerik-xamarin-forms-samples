using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using Newtonsoft.Json;
using tagit.iOS.Services;
using tagit.Models;
using tagit.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsStorageService))]

namespace tagit.iOS.Services
{
    /// Contains methods for saving
    /// and and retrieving local storage
    public class SettingsStorageService : ISettingsStorageService
    {
        public T Read<T>(string name, T defaultValue)
        {
            if (NSUserDefaults.StandardUserDefaults[name] == null)
                return defaultValue;

            if (typeof(T) == typeof(bool))
                return (T) Convert.ChangeType(NSUserDefaults.StandardUserDefaults.BoolForKey(name), typeof(T));
            if (typeof(T) == typeof(double))
                return (T) Convert.ChangeType(NSUserDefaults.StandardUserDefaults.DoubleForKey(name), typeof(T));
            if (typeof(T) == typeof(string))
                return (T) Convert.ChangeType(NSUserDefaults.StandardUserDefaults.StringForKey(name), typeof(T));
            try
            {
                var jsonText = Read(name, string.Empty);
                if (string.IsNullOrEmpty(jsonText))
                    return defaultValue;

                var jsonObject = JsonConvert.DeserializeObject<T>(jsonText);
                return (T) Convert.ChangeType(jsonObject, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        public void Write<T>(string name, T value)
        {
            if (typeof(T) == typeof(bool))
            {
                var b = (bool) Convert.ChangeType(value, typeof(bool));
                NSUserDefaults.StandardUserDefaults.SetBool(b, name);
            }
            else if (typeof(T) == typeof(double))
            {
                var d = (double) Convert.ChangeType(value, typeof(double));
                NSUserDefaults.StandardUserDefaults.SetDouble(d, name);
            }
            else if (typeof(T) == typeof(string))
            {
                var str = (string) Convert.ChangeType(value, typeof(string));
                NSUserDefaults.StandardUserDefaults.SetString(str, name);
            }
            else
            {
                try
                {
                    var settings = new JsonSerializerSettings();
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    var json = JsonConvert.SerializeObject(value, Formatting.None, settings);
                    Write(name, json);
                }
                catch
                {
                }
            }
        }

        public async Task WriteAsync(string name, List<ImageInformation> value)
        {   
            //NOT IMPLEMENTED
        }

        public async Task<List<ImageInformation>> ReadAsync(string name)
        {
            //NOT IMPLEMENTED
            return null;
        }
    }
}