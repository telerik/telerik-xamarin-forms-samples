using System.Threading.Tasks;
using Plugin.FilePicker;

namespace QSF.Services
{
    public class FilePickerService : IFilePickerService
    {
        public async Task<IFilePickerEntry> PickFileAsync()
        {
            var filePicker = CrossFilePicker.Current;
            var fileData = await filePicker.PickFile();

            if (fileData == null)
            {
                return null;
            }

            return new FilePickerEntry(fileData);
        }
    }
}
