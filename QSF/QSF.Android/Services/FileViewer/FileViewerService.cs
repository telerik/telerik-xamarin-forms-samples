using Android.Content;
using Android.OS;
using Android.Support.V4.Content;
using Java.IO;
using QSF.Droid.Services.FileViewer;
using QSF.Helpers;
using QSF.Services;
using System.IO;
using System.Threading.Tasks;

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

                var context = Android.App.Application.Context;
                Java.IO.File externalFilesDir = context.GetExternalFilesDir(null);
                Java.IO.File myDir;

                if (externalFilesDir != null)
                {
                    myDir = new Java.IO.File(externalFilesDir, "/Telerik");
                }
                else
                {
                    myDir = new Java.IO.File(context.FilesDir, "/TelerikFiles");
                }

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
                    Android.Net.Uri path = FileProvider.GetUriForFile(context, "com.telerik.xamarin.fileprovider", file);

                    string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                    string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                    Intent intent = new Intent(Intent.ActionView);
                    intent.SetDataAndType(path, mimeType);
                    intent.AddFlags(ActivityFlags.GrantReadUriPermission);

                    var choserActivity = Intent.CreateChooser(intent, "Choose App");
                    choserActivity.SetFlags(ActivityFlags.NewTask);
                    choserActivity.AddFlags(ActivityFlags.GrantReadUriPermission);

                    context.StartActivity(choserActivity);
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
