using QSF.Droid.Services.DeviceInfo;
using QSF.Services.DeviceInfo;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInfoService))]
namespace QSF.Droid.Services.DeviceInfo
{
    public class DeviceInfoService : IDeviceInfoService
    {
        public double PixelDensity
        {
            get
            {
                return this.Metrics.Density;
            }
        }

        private Android.Util.DisplayMetrics Metrics
        {
            get
            {
                var context = Android.App.Application.Context;
                return context.Resources.DisplayMetrics;
            }
        }

        public Size GetScreenSize()
        {
            return new Size(this.Metrics.WidthPixels, this.Metrics.HeightPixels);
        }
    }
}