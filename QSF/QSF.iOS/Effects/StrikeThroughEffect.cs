using Foundation;
using QSF.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(StrikeThroughEffect), nameof(StrikeThroughEffect))]

namespace QSF.iOS.Effects
{
    public class StrikeThroughEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var label = this.Control as UILabel;

            if (label != null && !string.IsNullOrEmpty(label.Text))
            {
                var text = new NSMutableAttributedString(label.Text);
                var range = new NSRange(0, text.Length);
                var value = NSNumber.FromInt32((int)NSUnderlineStyle.Single);

                text.AddAttribute(UIStringAttributeKey.StrikethroughStyle, value, range);
                label.AttributedText = text;
            }
        }

        protected override void OnDetached()
        {
            var label = this.Control as UILabel;

            if (label != null && !string.IsNullOrEmpty(label.Text))
            {
                var text = new NSMutableAttributedString(label.Text);
                var range = new NSRange(0, text.Length);
                var value = NSNumber.FromInt32((int)NSUnderlineStyle.None);

                text.AddAttribute(UIStringAttributeKey.StrikethroughStyle, value, range);
                label.AttributedText = text;
            }
        }
    }
}
