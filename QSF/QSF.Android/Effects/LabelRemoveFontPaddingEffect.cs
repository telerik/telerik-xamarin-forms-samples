using QSF.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(LabelRemoveFontPaddingEffect), nameof(LabelRemoveFontPaddingEffect))]
namespace QSF.Droid.Effects
{
    public class LabelRemoveFontPaddingEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var textView = (Android.Widget.TextView)this.Control;
                textView.SetIncludeFontPadding(false);
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
