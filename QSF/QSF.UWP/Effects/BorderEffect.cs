using QSF.UWP.Effects;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportEffect(typeof(BorderEffect), nameof(BorderEffect))]
namespace QSF.UWP.Effects
{
    public class BorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var searchBarRenderer = (SearchBarRenderer)this.Container;
                searchBarRenderer.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.White);
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
