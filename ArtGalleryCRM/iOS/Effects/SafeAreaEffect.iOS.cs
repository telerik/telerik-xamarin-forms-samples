using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(ArtGalleryCRM.iOS.Effects.SafeAreaEffect), "SafeAreaEffect")]
namespace ArtGalleryCRM.iOS.Effects
{
    public class SafeAreaEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Element is Xamarin.Forms.View view)
                {
                    // Get the safe area depending on the iOS device. This is especially handy for the iPhone X
                    var sa = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;

                    // The SupportPage uses this to account for the iPhone x's start button
                    view.Margin = new Thickness(view.Margin.Left, view.Margin.Top, view.Margin.Right, sa.Bottom);
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