using Android.Graphics;
using Android.Widget;
using QSF.Droid.Effects;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(SliderColorEffect), nameof(SliderColorEffect))]
namespace QSF.Droid.Effects
{
    public class SliderColorEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var xamarinFormsEffect = (QSF.Effects.SliderColorEffect) this.Element
                    .Effects
                    .FirstOrDefault(e => e is QSF.Effects.SliderColorEffect);

                SeekBar seekBar = this.Control as SeekBar;
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Q)
                {
                    seekBar.ProgressDrawable.SetColorFilter(new BlendModeColorFilter(xamarinFormsEffect.Color.ToAndroid(), BlendMode.SrcAtop));
                    seekBar.Thumb.SetColorFilter(new BlendModeColorFilter(xamarinFormsEffect.Color.ToAndroid(), BlendMode.SrcAtop));
                }
                else
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    seekBar.ProgressDrawable.SetColorFilter(xamarinFormsEffect.Color.ToAndroid(), PorterDuff.Mode.SrcAtop);
                    seekBar.Thumb.SetColorFilter(xamarinFormsEffect.Color.ToAndroid(), PorterDuff.Mode.SrcAtop);
#pragma warning restore CS0618 // Type or member is obsolete
                }
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