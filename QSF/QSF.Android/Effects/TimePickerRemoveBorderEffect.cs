using QSF.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(TimePickerRemoveBorderEffect), nameof(TimePickerRemoveBorderEffect))]
namespace QSF.Droid.Effects
{
    public class TimePickerRemoveBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                this.Control.Background = null;
                this.Control.SetPadding(0, 0, 0, 0);
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