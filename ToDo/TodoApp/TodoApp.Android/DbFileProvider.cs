using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(TodoApp.Droid.DbFileProvider))]
namespace TodoApp.Droid
{
    public class DbFileProvider : IDbFileProvider
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}