using QSF.Droid.Effects;
using System;
using Telerik.XamarinForms.DataControlsRenderer.Android;
using Telerik.XamarinForms.DataControlsRenderer.Android.ListView;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(ListViewSizeToContentEffect), nameof(ListViewSizeToContentEffect))]
namespace QSF.Droid.Effects
{
    public class ListViewSizeToContentEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                RadListViewWrapper control = (RadListViewWrapper)this.Control;
                RadExtendedListView listView = (RadExtendedListView)control.ListView;
                listView.RestrictToScreenSize = false;
                listView.NestedScrollingEnabled = false;
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