using tagit.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Switch), typeof(ToggleRenderer))]
namespace tagit.iOS.Renderers
{
    public class ToggleRenderer : SwitchRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var nativeSwitch = Control;

                nativeSwitch.OnTintColor = UIColor.FromRGB(0, 151, 223);
                nativeSwitch.ThumbTintColor = UIColor.FromRGB(0, 118, 174);
            }
        }
    }
}