using QSF.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.ViewGroup;

[assembly: ExportEffect(typeof(DatePickerRemoveBorderEffect), nameof(DatePickerRemoveBorderEffect))]
namespace QSF.Droid.Effects
{
    public class DatePickerRemoveBorderEffect : PlatformEffect
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
