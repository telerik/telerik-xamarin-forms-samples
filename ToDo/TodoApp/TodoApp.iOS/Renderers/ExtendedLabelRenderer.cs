using Foundation;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TodoApp.Controls.ExtendedLabel), typeof(TodoApp.iOS.Renderers.ExtendedLabelRenderer))]
namespace TodoApp.iOS.Renderers
{
    public class ExtendedLabelRenderer : LabelRenderer
    {
        private Controls.ExtendedLabel ExtendedElement => (Controls.ExtendedLabel)Element;

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateTextDecorations();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Controls.ExtendedLabel.IsStrikeThroughProperty.PropertyName)
            {
                UpdateTextDecorations();
            }
        }

        void UpdateTextDecorations()
        {
            if (string.IsNullOrEmpty(Element.Text))
                return;

            var strikethrough = ExtendedElement.IsStrikeThrough ? NSUnderlineStyle.Single : NSUnderlineStyle.None;
            Control.AttributedText = new NSMutableAttributedString(Element.Text, Control.Font, strikethroughStyle: strikethrough);
        }
    }
}