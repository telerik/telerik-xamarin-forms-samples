using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Newtonsoft.Json;
using tagit.Models;
using tagit.Services;
using tagit.UWP.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsStorageService))]
namespace tagit.UWP.Services
{
    /// Contains methods for saving and and retrieving local storage
    public class SettingsStorageService : ISettingsStorageService
    {
        public T Read<T>(string name, T defaultValue)
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            try
            {
                if (localSettings.Values.ContainsKey(name))
                {
                    var value = localSettings.Values[name];

                    if (typeof(T) == typeof(bool))
                    {
                        return (T) Convert.ChangeType(value, typeof(bool));
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"SettingsStorageService.Read Exception: {ex}");
            }

            return defaultValue;
        }

        public async void Write<T>(string name, T value)
        {
            try
            {
                var localSettings = ApplicationData.Current.LocalSettings;

                if (typeof(T) == typeof(bool))
                    localSettings.Values[name] = value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SettingsStorageService.Write Exception: {ex}");
            }
        }

        public async Task WriteAsync(string name, List<ImageInformation> value)
        {
            var jsonContents = JsonConvert.SerializeObject(value);
            var localFolder = ApplicationData.Current.LocalFolder;
            var textFile = await localFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);

            try
            {
                using (var textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (var textWriter = new DataWriter(textStream))
                    {
                        textWriter.WriteString(jsonContents);
                        await textWriter.StoreAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SettingsStorageService.WriteAsync Exception: {ex}");
            }
        }

        public async Task<List<ImageInformation>> ReadAsync(string name)
        {
            var value = new List<ImageInformation>();

            try
            {
                var localFolder = ApplicationData.Current.LocalFolder;
                var textFile = await localFolder.GetFileAsync(name);
                using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
                {
                    using (var textReader = new DataReader(textStream))
                    {
                        var textLength = (uint) textStream.Size;
                        await textReader.LoadAsync(textLength);
                        var jsonContents = textReader.ReadString(textLength);
                        value = JsonConvert.DeserializeObject<List<ImageInformation>>(jsonContents);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SettingsStorageService.ReadAsync Exception: {ex}");
            }

            return value == null ? new List<ImageInformation>() : value;
        }
    }
}