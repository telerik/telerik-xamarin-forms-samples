using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Windows.ApplicationModel;

[assembly: ResolutionGroupName("Telerik")]
[assembly: ExportEffect(typeof(ArtGalleryCRM.Uwp.Effects.AppVersionEffect), "AppVersionEffect")]
namespace ArtGalleryCRM.Uwp.Effects
{
    public class AppVersionEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Control is TextBlock tb)
                {
                    var version = Package.Current.Id.Version;
                    tb.Text = $"v{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
                }
            }
            catch
            {
            }
        }

        protected override void OnDetached()
        {

        }
    }
}
