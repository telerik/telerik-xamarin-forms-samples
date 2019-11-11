using QSF.iOS.Renderers.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(SafeAreaPageRenderer))]
namespace QSF.iOS.Renderers.Pages
{
    public class SafeAreaPageRenderer : PageRenderer
    {
        public override void ViewSafeAreaInsetsDidChange()
        {
            base.ViewSafeAreaInsetsDidChange();

            var page = (Element as Page);
            if (page != null && UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                var insets = NativeView.SafeAreaInsets;
                if (page.Parent is TabbedPage)
                {
                    insets.Bottom = 0;
                }

                var padding = new Thickness(insets.Left, insets.Top, insets.Right, insets.Bottom);

                page.Padding = padding;
            }
        }
    }
}