using Android.Widget;
using QSF.Droid.Services.Toast;
using QSF.Services.Toast;
using Xamarin.Forms;
using QSF.Services.DeviceInfo;

[assembly: Dependency(typeof(ToastMessageService))]
namespace QSF.Droid.Services.Toast
{
    public class ToastMessageService : IToastMessageService
    {
        public void LongAlert(string message)
        {
            var toast = Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long);
            ShowAlert(toast);
        }

        public void ShortAlert(string message)
        {
            var toast = Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short);
            ShowAlert(toast);
        }

        private static void ShowAlert(Android.Widget.Toast toast)
        {
            var deviceInfoService = DependencyService.Get<IDeviceInfoService>();
            var pixelDensity = (int)deviceInfoService.PixelDensity;

            toast.SetGravity(Android.Views.GravityFlags.Bottom | Android.Views.GravityFlags.CenterHorizontal, 0, 100 * pixelDensity);
            toast.Show();
        }
    }
}