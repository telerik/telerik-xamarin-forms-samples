using Android.Graphics;
using Android.Widget;
using QSF.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(StrikeThroughEffect), nameof(StrikeThroughEffect))]

namespace QSF.Droid.Effects
{
    public class StrikeThroughEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var textView = this.Control as TextView;

            if (textView != null)
            {
                textView.PaintFlags |= PaintFlags.StrikeThruText;
            }
        }

        protected override void OnDetached()
        {
            var textView = this.Control as TextView;

            if (textView != null)
            {
                textView.PaintFlags &= ~PaintFlags.StrikeThruText;
            }
        }
    }
}