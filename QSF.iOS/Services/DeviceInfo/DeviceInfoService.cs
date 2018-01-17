using QSF.iOS.Services.DeviceInfo;
using QSF.Services.DeviceInfo;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInfoService))]
namespace QSF.iOS.Services.DeviceInfo
{
    public class DeviceInfoService : IDeviceInfoService
    {
        public DeviceInfoService()
        {
            this.PixelDensity = 1;
        }

        public double PixelDensity { get; }

        public Size GetScreenSize()
        {
            var width = double.Parse(UIScreen.MainScreen.Bounds.Width.ToString());
            var height = double.Parse(UIScreen.MainScreen.Bounds.Height.ToString());

            return new Size(width, height);
        }
    }
}