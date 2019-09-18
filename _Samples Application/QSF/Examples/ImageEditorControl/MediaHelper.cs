using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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

            if (!await RequestPermissionsAsync(Permission.Camera, Permission.Storage))
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

            if (!await RequestPermissionsAsync(Permission.Photos, Permission.Storage))
            {
                return null;
            }

            var mediaPlugin = CrossMedia.Current;
            var mediaOptions = new PickMediaOptions();
            var mediaFile = await mediaPlugin.PickPhotoAsync(mediaOptions);

            return mediaFile?.Path;
        }

        private static async Task<bool> RequestPermissionsAsync(params Permission[] permissions)
        {
            var permissionsPlugin = CrossPermissions.Current;
            var missingPermissions = new List<Permission>();

            foreach (var permission in permissions)
            {
                var permissionStatus = await permissionsPlugin.CheckPermissionStatusAsync(permission);

                if (permissionStatus != PermissionStatus.Granted)
                {
                    if (await permissionsPlugin.ShouldShowRequestPermissionRationaleAsync(permission))
                    {
                        return false;
                    }

                    missingPermissions.Add(permission);
                }
            }

            if (missingPermissions.Count > 0)
            {
                var requestedPermissions = missingPermissions.ToArray();
                var permissionStatuses = await permissionsPlugin.RequestPermissionsAsync(requestedPermissions);

                foreach (var permissionStatus in permissionStatuses.Values)
                {
                    if (permissionStatus != PermissionStatus.Granted)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
