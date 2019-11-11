using System;
using QSF.iOS.Services;
using QSF.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystemService))]

namespace QSF.iOS.Services
{
    public class FileSystemService : IFileSystemService
    {
        public string GetLocalFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}