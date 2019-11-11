using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TodoApp.Controls.ExtendedLabel), typeof(TodoApp.UWP.Renderers.ExtendedLabelRenderer))]
namespace TodoApp.UWP.Renderers
{
    public class ExtendedLabelRenderer : LabelRenderer
    {
        private Controls.ExtendedLabel ExtendedElement => (Controls.ExtendedLabel)Element;

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateTextDecorations(Control);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Controls.ExtendedLabel.IsStrikeThroughProperty.PropertyName)
            {
                UpdateTextDecorations(this.Control);
            }
        }

        void UpdateTextDecorations(TextBlock textBlock)
        {
            if (textBlock == null)
                return;

            if (ExtendedElement.IsStrikeThrough)
            {
                textBlock.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
            }
            else
            {
                textBlock.ClearValue(TextBlock.TextDecorationsProperty);
            }
        }
    }
}
