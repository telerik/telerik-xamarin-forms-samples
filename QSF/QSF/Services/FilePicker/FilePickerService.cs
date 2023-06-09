using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace QSF.Services
{
    public class FilePickerService : IFilePickerService
    {
        public async Task<FileResult> PickFileAsync()
        {
            FileResult result;
            try
            {
                result = await FilePicker.PickAsync(PickOptions.Default);
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }
    }
}
