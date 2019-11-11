using QSF.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(TimePickerRemoveBorderEffect), nameof(TimePickerRemoveBorderEffect))]
namespace QSF.iOS.Effects
{
    public class TimePickerRemoveBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                ((UITextField)this.Control).BorderStyle = UITextBorderStyle.None;
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
