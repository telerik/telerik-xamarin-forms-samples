using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tagit.Models;
using tagit.Services;
using Xamarin.Forms;

namespace tagit.Helpers
{
    /// <summary>
    ///     Used to access the platform-specific SettingsStorageService methods
    /// </summary>
    internal static class StorageHelper
    {
        internal static void SaveIsFirstRun(bool isFirstRun)
        {
            var service = DependencyService.Get<ISettingsStorageService>();
            service.Write("IsFirstRun", isFirstRun);
        }

        internal static bool GetIsFirstRun()
        {
            var service = DependencyService.Get<ISettingsStorageService>();
            return service.Read("IsFirstRun", true);
        }

        internal static void SaveThemePreference(bool useDarkTheme)
        {
            var service = DependencyService.Get<ISettingsStorageService>();
            service.Write("UseDarkTheme", useDarkTheme);
        }

        internal static bool GetThemePreference()
        {
            var service = DependencyService.Get<ISettingsStorageService>();
            return service.Read("UseDarkTheme", false);
        }

        internal static async Task SaveTaggedImagesAsync(List<ImageInformation> images)
        {
            var service = DependencyService.Get<ISettingsStorageService>();

            if (Device.RuntimePlatform == Device.UWP)
                await service.WriteAsync("TaggedImages", images);
            else
                service.Write("TaggedImages", images);

            return;
        }

        internal static async Task<List<ImageInformation>> GetTaggedImagesAsync()
        {
            var service = DependencyService.Get<ISettingsStorageService>();

            if (Device.RuntimePlatform == Device.UWP)
            {
                var taggedImages = await service.ReadAsync("TaggedImages");

                return taggedImages;
            }

            var images = service.Read("TaggedImages", new List<ImageInformation>());

            return images;
        }
    }
}