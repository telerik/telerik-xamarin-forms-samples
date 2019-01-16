using Android.Widget;
using QSF.Droid.Services.Toast;
using QSF.Services.Toast;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using QSF.Services.DeviceInfo;

[assembly: Dependency(typeof(ToastMessageService))]
namespace QSF.Droid.Services.Toast
{
    public class ToastMessageService : IToastMessageService
    {
        private const string DefaultBackgroundColor = "#1d1d1e";

        public void LongAlert(string message)
        {
            var toast = Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long);

            toast.View.Background.SetColorFilter(Color.FromHex(DefaultBackgroundColor).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);

            var textView = toast.View.FindViewById<TextView>(Android.Resource.Id.Message);
            textView.SetTextColor(Color.White.ToAndroid());

            var deviceInfoService = DependencyService.Get<IDeviceInfoService>();
            var pixelDensity = (int)deviceInfoService.PixelDensity;

            toast.SetGravity(Android.Views.GravityFlags.Bottom | Android.Views.GravityFlags.CenterHorizontal, 0, 100 * pixelDensity);

            toast.Show();
        }

        public void ShortAlert(string message)
        {
            var toast = Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short);

            toast.View.Background.SetColorFilter(Color.FromHex(DefaultBackgroundColor).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);

            var textView = toast.View.FindViewById<TextView>(Android.Resource.Id.Message);
            textView.SetTextColor(Color.White.ToAndroid());

            var deviceInfoService = DependencyService.Get<IDeviceInfoService>();
            var pixelDensity = (int)deviceInfoService.PixelDensity;

            toast.SetGravity(Android.Views.GravityFlags.Bottom | Android.Views.GravityFlags.CenterHorizontal, 0, 100 * pixelDensity);

            toast.Show();
        }
    }
}