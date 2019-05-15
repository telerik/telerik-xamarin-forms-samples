using System.IO;
using System.Threading.Tasks;

namespace QSF.Examples.ImageEditorControl.FirstLookExample
{
    public interface IImageContext
    {
        Task SaveAsync(Stream stream);
    }
}