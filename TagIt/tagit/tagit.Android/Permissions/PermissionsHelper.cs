using Android;
using Android.App;
using Android.Content.PM;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tagit.Droid.Permissions
{
    internal static class PermissionsHelper
    {
        public static Activity Activity { get; set; }
        public static Dictionary<PermissionsRequestCode, TaskCompletionSource<bool[]>> requestCodeToCompletitionSource;

        static PermissionsHelper()
        {
            requestCodeToCompletitionSource = new Dictionary<PermissionsRequestCode, TaskCompletionSource<bool[]>>();
        }
        
        internal static Task<bool> RequestStorageAccess()
        {
            string[] permissions = { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage };

            return RequestAccessPermission(PermissionsRequestCode.ExternalStorageAccess, permissions);
        }

        private static async Task<bool> RequestAccessPermission(PermissionsRequestCode permissionCode, string[] permissions)
        {
            if (Activity == null)
            {
                throw new ArgumentNullException(nameof(Activity));
            }

            List<string> permissionsToRequest = new List<string>();

            for (int i = 0; i < permissions.Length; i++)
            {
                string permission = permissions[i];
                if (Activity.CheckSelfPermission(permission) != Permission.Granted)
                {
                    permissionsToRequest.Add(permission);
                }
            }

            if (permissionsToRequest.Count == 0)
            {
                return true;
            }

            if (requestCodeToCompletitionSource.ContainsKey(permissionCode))
            {
                return false;
            }

            TaskCompletionSource<bool[]> completitionSource = new TaskCompletionSource<bool[]>();

            requestCodeToCompletitionSource[permissionCode] = completitionSource;

            Activity.RequestPermissions(permissionsToRequest.ToArray(), (int)permissionCode);

            bool[] results = await completitionSource.Task;

            for (int i = 0; i < results.Length; i++)
            {
                if (!results[i])
                {
                    return false;
                }
            }

            return true;
        }

        internal static void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsRequestCode code = (PermissionsRequestCode)requestCode;

            TaskCompletionSource<bool[]> completitionSource = requestCodeToCompletitionSource[code];

            requestCodeToCompletitionSource.Remove(code);

            bool[] results = new bool[grantResults.Length];

            for (int i = 0; i < grantResults.Length; i++)
            {
                results[i] = grantResults[i] == Permission.Granted;
            }

            completitionSource.SetResult(results);
        }
    }
}