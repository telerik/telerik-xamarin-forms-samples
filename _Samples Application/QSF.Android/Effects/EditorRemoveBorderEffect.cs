using Android.Graphics.Drawables;
using QSF.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(EditorRemoveBorderEffect), nameof(EditorRemoveBorderEffect))]
namespace QSF.Droid.Effects
{
    public class EditorRemoveBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                GradientDrawable gd = new GradientDrawable();
                this.Control.Background = gd;
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