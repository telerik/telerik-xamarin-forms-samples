using Com.Telerik.Android.Primitives.Widget.Sidedrawer;
using QSF.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(SideDrawerTouchTargetEffect), nameof(SideDrawerTouchTargetEffect))]
namespace QSF.Droid.Effects
{
    public class SideDrawerTouchTargetEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                RadSideDrawer nativeSideDrawer = this.Control as RadSideDrawer;
                nativeSideDrawer.TouchTargetThreshold = 0;
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