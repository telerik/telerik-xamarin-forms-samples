using Android.Content;
using Android.OS;
using Java.IO;
using QSF.Droid.Permissions;
using QSF.Droid.Services.FileViewer;
using QSF.Services;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FileViewerService))]
namespace QSF.Droid.Services.FileViewer
{
    public class FileViewerService : IFileViewerService
    {
        public async Task<bool> View(Stream stream, string filename)
        {
            try
            {
                if ((int)Build.VERSION.SdkInt >= 23)
                {
                    bool result = await PermissionsHelper.RequestStorrageAccess();
                    if (!result)
                    {
                        return false;
                    }
                }

                string root = null;

                if (Android.OS.Environment.IsExternalStorageEmulated)
                {
                    root = Android.OS.Environment.ExternalStorageDirectory.ToString();
                }
                else
                {
                    root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                }

                Java.IO.File myDir = new Java.IO.File(root + "/Telerik");
                if (!myDir.Exists())
                {
                    if (!myDir.Mkdir())
                    {
                        return false;
                    }
                }

                Java.IO.File file = new Java.IO.File(myDir, filename);

                if (file.Exists())
                {
                    file.Delete();
                }

                if (!file.CreateNewFile())
                {
                    return false;
                }

                using (FileOutputStream outs = new FileOutputStream(file))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    outs.Write(buffer);
                }

                if (file.Exists())
                {
                    Android.Net.Uri path = Android.Net.Uri.FromFile(file);

                    string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                    string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                    Intent intent = new Intent(Intent.ActionView);
                    intent.SetDataAndType(path, mimeType);

                    Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}