using Android.Content;
using Android.Graphics;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TodoApp.Controls.ExtendedLabel), typeof(TodoApp.Droid.Renderers.ExtendedLabelRenderer))]
namespace TodoApp.Droid.Renderers
{
    public class ExtendedLabelRenderer : LabelRenderer
    {
        public ExtendedLabelRenderer(Context context)
            : base(context)
        { }

        private Controls.ExtendedLabel ExtendedElement => (Controls.ExtendedLabel)Element;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Controls.ExtendedLabel.IsStrikeThroughProperty.PropertyName)
            {
                UpdateStrikeThrough();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null || (((Controls.ExtendedLabel)e.OldElement).IsStrikeThrough !=
                ((Controls.ExtendedLabel)e.NewElement).IsStrikeThrough))
            {
                UpdateStrikeThrough();
            }
        }

        private void UpdateStrikeThrough()
        {
            this.Control.PaintFlags = ExtendedElement.IsStrikeThrough ?
                this.Control.PaintFlags | PaintFlags.StrikeThruText :
                this.Control.PaintFlags & ~PaintFlags.StrikeThruText;
        }
    }
}