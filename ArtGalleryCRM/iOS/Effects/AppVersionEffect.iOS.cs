using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Telerik")]
[assembly: ExportEffect(typeof(ArtGalleryCRM.iOS.Effects.AppVersionEffect), "AppVersionEffect")]
namespace ArtGalleryCRM.iOS.Effects
{
    public class AppVersionEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Control is UILabel label)
                {
                    label.Text = $"v{NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"]}";
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