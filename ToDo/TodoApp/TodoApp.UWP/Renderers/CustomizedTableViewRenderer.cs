using TodoApp.UWP.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly:ExportRenderer(typeof(TableView), typeof(CustomizedTableViewRenderer))]
namespace TodoApp.UWP.Renderers
{
    public class CustomizedTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);
            this.Control?.GroupStyle.Clear();
        }
    }
}