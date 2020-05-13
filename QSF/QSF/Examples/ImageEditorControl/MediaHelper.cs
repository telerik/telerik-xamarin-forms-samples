using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using QSF.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QSF.Examples.ImageEditorControl
{
    internal static class MediaHelper
    {
        public static bool CanTakePhoto
        {
            get
            {
                var mediaPlugin = CrossMedia.Current;

                return mediaPlugin.IsTakePhotoSupported &&
                       mediaPlugin.IsCameraAvailable;
            }
        }

        public static bool CanPickPhoto
        {
            get
            {
                var mediaPlugin = CrossMedia.Current;

                return mediaPlugin.IsPickPhotoSupported;
            }
        }

        public static async Task<bool> InitializeAsync()
        {
            var mediaPlugin = CrossMedia.Current;

            return await mediaPlugin.Initialize();
        }

        public static async Task<string> TakePhotoAsync()
        {
            if (!CanTakePhoto)
            {
                return null;
            }

            if (!await PermissionsHelper.RequestPermissionsAsync(Permission.Camera, Permission.Storage))
            {
                return null;
            }

            var mediaPlugin = CrossMedia.Current;
            var mediaOptions = new StoreCameraMediaOptions();
            var mediaFile = await mediaPlugin.TakePhotoAsync(mediaOptions);

            return mediaFile?.Path;
        }

        public static async Task<string> PickPhotoAsync()
        {
            if (!CanPickPhoto)
            {
                return null;
            }

            if (!await PermissionsHelper.RequestPermissionsAsync(Permission.Photos, Permission.Storage))
            {
                return null;
            }

            var mediaPlugin = CrossMedia.Current;
            var mediaOptions = new PickMediaOptions();
            var mediaFile = await mediaPlugin.PickPhotoAsync(mediaOptions);

            return mediaFile?.Path;
        }
    }
}
