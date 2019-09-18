using System.IO;
using System.Threading.Tasks;
using Telerik.XamarinForms.ImageEditor;
using Xamarin.Forms;

namespace QSF.Examples.ImageEditorControl
{
    public interface IImageContext
    {
        Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality);
        Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality, Size maximumSize);
        Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality, double scaleFactor);
    }
}