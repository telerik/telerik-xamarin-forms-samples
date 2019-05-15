using QSF.Services;
using QSF.UWP.Services;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystemService))]

namespace QSF.UWP.Services
{
    public class FileSystemService : IFileSystemService
    {
        public string GetLocalFolder()
        {
            return ApplicationData.Current.LocalFolder.Path;
        }
    }
}
