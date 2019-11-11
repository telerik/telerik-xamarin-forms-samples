using QSF.Services.DeviceInfo;
using QSF.UWP.Services.DeviceInfo;
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