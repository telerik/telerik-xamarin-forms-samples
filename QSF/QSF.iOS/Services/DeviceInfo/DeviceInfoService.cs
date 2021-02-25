using System;
using System.Globalization;
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

        public double OSVersion
        {
            get
            {
                var os = UIDevice.CurrentDevice.SystemVersion;
                if (Version.TryParse(os, out var version))
                {
                    return double.Parse(string.Format("{0}.{1}", version.Major, version.Minor), CultureInfo.InvariantCulture);
                }

                if (int.TryParse(os, out var major))
                {
                    return major;
                }

                throw new Exception("Version could not be parsed.");
            }
        }

        public Size GetScreenSize()
        {
            var width = double.Parse(UIScreen.MainScreen.Bounds.Width.ToString());
            var height = double.Parse(UIScreen.MainScreen.Bounds.Height.ToString());

            return new Size(width, height);
        }
    }
}