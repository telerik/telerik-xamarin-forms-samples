using Foundation;
using QSF.iOS.Effects;
using Telerik.XamarinForms.DataControlsRenderer.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(ListViewRemoveBackgroundEffect), nameof(ListViewRemoveBackgroundEffect))]

namespace QSF.iOS.Effects
{
    public class ListViewRemoveBackgroundEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var listView = this.Control as TKExtendedListView;
            if (listView != null)
            {
                listView.ShouldRemoveCellsBackground = true;
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
