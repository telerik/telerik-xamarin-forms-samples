using System.Threading.Tasks;

namespace QSF.Services
{
    public interface IFilePickerService
    {
        Task<IFilePickerEntry> PickFileAsync();
    }
}