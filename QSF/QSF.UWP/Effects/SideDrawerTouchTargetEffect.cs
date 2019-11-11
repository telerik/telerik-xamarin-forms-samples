using Telerik.Core;
using Telerik.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportEffect(typeof(QSF.UWP.Effects.SideDrawerTouchTargetEffect), nameof(QSF.UWP.Effects.SideDrawerTouchTargetEffect))]
namespace QSF.UWP.Effects
{
    public class SideDrawerTouchTargetEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                RadSideDrawer nativeSideDrawer = this.Control as RadSideDrawer;
                nativeSideDrawer.TouchTargetThreshold = 0;
                nativeSideDrawer.Loaded += this.OnNativeSideDrawerLoaded;
            }
            catch
            {
            }
        }

        protected override void OnDetached()
        {
        }

        private void OnNativeSideDrawerLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RadSideDrawer nativeSideDrawer = (RadSideDrawer)sender;
            Border swipeBorder = ElementTreeHelper.FindVisualDescendant<Border>(nativeSideDrawer);
            if (swipeBorder != null)
            {
                swipeBorder.IsHitTestVisible = false;
            }

            nativeSideDrawer.Loaded -= this.OnNativeSideDrawerLoaded;
        }
    }
}
