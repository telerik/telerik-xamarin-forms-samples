using QSF.iOS.Effects;
using TelerikUI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(SideDrawerTouchTargetEffect), nameof(SideDrawerTouchTargetEffect))]
namespace QSF.iOS.Effects
{
    public class SideDrawerTouchTargetEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                TKSideDrawerView nativeSideDrawer = this.Control as TKSideDrawerView;
                TKSideDrawer defaultSideDrawer = nativeSideDrawer.DefaultSideDrawer;
                defaultSideDrawer.EdgeSwipeTreshold = 0;
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