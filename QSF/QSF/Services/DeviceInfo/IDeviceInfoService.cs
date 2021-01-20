using Xamarin.Forms;

namespace QSF.Services.DeviceInfo
{
    public interface IDeviceInfoService
    {
        double PixelDensity { get; }

        double OSVersion { get; }

        Size GetScreenSize();
    }
}
