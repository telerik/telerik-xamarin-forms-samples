using QSF.Services.DeviceInfo;
using QSF.UWP.Services.DeviceInfo;
using System;
using Windows.Foundation;
using Windows.System.Profile;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInfoService))]
namespace QSF.UWP.Services.DeviceInfo
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
                var version = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
                if (ulong.TryParse(version, out var v))
                {
                    var buildVersion = (v & 0x00000000FFFF0000L) >> 16;
                    return buildVersion;
                }

                throw new Exception("Version could not be provided.");
            }
        }

        public Xamarin.Forms.Size GetScreenSize()
        {
            Rect bounds;
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                bounds = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().VisibleBounds;
            }
            else
            {
                bounds = Windows.UI.Xaml.Window.Current.Bounds;
            }

            return new Xamarin.Forms.Size(bounds.Width, bounds.Height);
        }
    }
}