using System.IO;
using System.Threading.Tasks;
using Telerik.XamarinForms.ImageEditor;
using Xamarin.Forms;

namespace QSF.Examples.ImageEditorControl.FirstLookExample
{
    public class ImageContext : BindableObject, IImageContext
    {
        public static readonly BindableProperty ImageEditorProperty =
            BindableProperty.Create(nameof(ImageEditor), typeof(RadImageEditor), typeof(ImageContext));

        public RadImageEditor ImageEditor
        {
            get
            {
                return (RadImageEditor)this.GetValue(ImageEditorProperty);
            }
            set
            {
                this.SetValue(ImageEditorProperty, value);
            }
        }

        public async Task SaveAsync(Stream stream)
        {
            if (this.ImageEditor != null)
            {
                await this.ImageEditor.SaveAsync(stream, ImageFormat.Jpeg, 1);
            }
        }
    }
}
