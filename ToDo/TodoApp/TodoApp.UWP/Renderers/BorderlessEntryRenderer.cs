using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TodoApp.Controls.BorderlessEntry), typeof(TodoApp.UWP.Renderers.BorderlessEntryRenderer))]
namespace TodoApp.UWP.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
    	protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
 
            if (Control != null)
            {
            	Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
            }
        }
    }
}