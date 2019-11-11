using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(TodoApp.iOS.DbFileProvider))]
namespace TodoApp.iOS
{
    public class DbFileProvider : IDbFileProvider
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}