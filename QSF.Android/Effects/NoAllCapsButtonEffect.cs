using QSF.Droid.Effects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(NoAllCapsButtonEffect), nameof(NoAllCapsButtonEffect))]
namespace QSF.Droid.Effects
{
    public class NoAllCapsButtonEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var button = (Android.Widget.Button)this.Control;
                button.SetAllCaps(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}