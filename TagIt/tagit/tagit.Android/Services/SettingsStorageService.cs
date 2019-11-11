using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Newtonsoft.Json;
using tagit.Droid.Services;
using tagit.Models;
using tagit.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsStorageService))]
namespace tagit.Droid.Services
{
    /// Contains methods for saving and and retrieving local storage
    public class SettingsStorageService : ISettingsStorageService
    {
        private const string SettingsAppName = "tagit_Android";

        public T Read<T>(string name, T defaultValue)
        {
            var prefs = Android.App.Application.Context.GetSharedPreferences(SettingsAppName, FileCreationMode.Private);

            if (!prefs.Contains(name))
            {
                return defaultValue;
            }

            if (typeof(T) == typeof(bool))
            {
                return (T) Convert.ChangeType(prefs.GetBoolean(name, false), typeof(T));
            }

            if (typeof(T) == typeof(double))
            {
                return (T) Convert.ChangeType((double) prefs.GetFloat(name, -1.0f), typeof(T));
            }

            if (typeof(T) == typeof(string))
            {
                return (T) Convert.ChangeType(prefs.GetString(name, string.Empty), typeof(T));
            }

            try
            {
                var jsonText = Read(name, string.Empty);

                if (string.IsNullOrEmpty(jsonText))
                {
                    return defaultValue;
                }

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
            var prefs = Android.App.Application.Context.GetSharedPreferences(SettingsAppName, FileCreationMode.Private);

            var prefEditor = prefs.Edit();

            if (typeof(T) == typeof(bool))
            {
                var b = (bool) Convert.ChangeType(value, typeof(bool));
                prefEditor.PutBoolean(name, b);
            }
            else if (typeof(T) == typeof(double))
            {
                var d = (double) Convert.ChangeType(value, typeof(double));
                prefEditor.PutFloat(name, (float) d);
            }
            else if (typeof(T) == typeof(string))
            {
                var str = (string) Convert.ChangeType(value, typeof(string));
                prefEditor.PutString(name, str);
            }
            else
            {
                try
                {
                    var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

                    var json = JsonConvert.SerializeObject(value, Formatting.None, settings);

                    Write(name, json);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"SettingsStorageService.Write Exception: {ex}");
                }
            }

            prefEditor.Commit();
        }

        public async Task WriteAsync(string name, List<ImageInformation> value)
        {
        }

        public async Task<List<ImageInformation>> ReadAsync(string name)
        {
            return null;
        }
    }
}