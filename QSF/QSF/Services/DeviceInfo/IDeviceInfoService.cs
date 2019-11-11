using Xamarin.Forms;

namespace QSF.Services.DeviceInfo
{
    public interface IDeviceInfoService
    {
        double PixelDensity { get; }

        Size GetScreenSize();
    }
}
