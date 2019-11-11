using QSF.UWP.Effects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("TelerikQSF")]
[assembly: ExportEffect(typeof(ButtonBorderEffect), nameof(ButtonBorderEffect))]
namespace QSF.UWP.Effects
{
    public class ButtonBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var formsButton = (FormsButton)this.Control;
                var style = (Windows.UI.Xaml.Style)App.Current.Resources["ButtonStyle"];
                formsButton.Style = style;
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
