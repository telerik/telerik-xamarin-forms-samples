using System.Threading.Tasks;
using Xamarin.Essentials;

namespace QSF.Services
{
    public interface IFilePickerService
    {
        Task<FileResult> PickFileAsync();
    }
}