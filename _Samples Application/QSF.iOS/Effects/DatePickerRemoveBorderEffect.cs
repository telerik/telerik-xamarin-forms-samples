using QSF.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(DatePickerRemoveBorderEffect), nameof(DatePickerRemoveBorderEffect))]
namespace QSF.iOS.Effects
{
    public class DatePickerRemoveBorderEffect : PlatformEffect
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
