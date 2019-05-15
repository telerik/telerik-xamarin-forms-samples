using System;
using QSF.Droid.Services;
using QSF.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystemService))]

namespace QSF.Droid.Services
{
    public class FileSystemService : IFileSystemService
    {
        public string GetLocalFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}
