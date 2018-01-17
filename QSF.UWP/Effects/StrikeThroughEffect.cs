using QSF.UWP.Effects;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

using FontStyle = Windows.UI.Text.FontStyle;

[assembly: ExportEffect(typeof(StrikeThroughEffect), nameof(StrikeThroughEffect))]

namespace QSF.UWP.Effects
{
    public class StrikeThroughEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var textBlock = this.Control as TextBlock;

            if (textBlock != null)
            {
                textBlock.FontStyle = FontStyle.Oblique;
            }
        }

        protected override void OnDetached()
        {
            var textBlock = this.Control as TextBlock;

            if (textBlock != null)
            {
                textBlock.FontStyle = FontStyle.Normal;
            }
        }
    }
}
