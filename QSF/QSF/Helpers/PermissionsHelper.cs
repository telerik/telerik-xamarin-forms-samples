using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;


namespace QSF.Helpers
{
    internal static class PermissionsHelper
    {
        internal static async Task<bool> RequestStorrageAccess()
        {
            return await RequestPermissionsAsync(Permission.Storage);
        }

        internal static async Task<bool> RequestPermissionsAsync(params Permission[] permissions)
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
