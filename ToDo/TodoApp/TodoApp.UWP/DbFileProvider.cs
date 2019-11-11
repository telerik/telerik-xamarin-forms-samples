using System.IO;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(TodoApp.UWP.DbFileProvider))]
namespace TodoApp.UWP
{
    public class DbFileProvider : IDbFileProvider
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
