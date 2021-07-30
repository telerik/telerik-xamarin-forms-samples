using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Content;
using Android.Provider;
using tagit.Droid.Services;
using tagit.Services;
using Telerik.XamarinForms.Common.Android;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageService))]
namespace tagit.Droid.Services
{
    /// Contains methods for accessing local images on the file system
    public class ImageService : IImageService
    {
        private WeakReference<Context> context;
        private static string[] FoldersToExclude = new string[] { "viber", "viber images", "whatsapp", "whatsapp images", "messenger", "facebook", "twitter", "instagram" };

        public ImageService()
        {
            this.context = new WeakReference<Context>(Android.App.Application.Context);
        }

        public Context Context
        {
            get
            {
                if (this.context.TryGetTarget(out Context target))
                {
                    return target;
                }

                return null;
            }
        }

        public async Task SaveImageAsync(string fileName, string url)
        {
            ContentResolver resolver = this.Context?.ContentResolver;
            if (resolver != null)
            {
                Android.Net.Uri imagesCollection;
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Q)
                {
                    imagesCollection = MediaStore.Images.Media.GetContentUri(MediaStore.VolumeExternalPrimary);
                }
                else
                {
                    imagesCollection = MediaStore.Images.Media.ExternalContentUri;
                }

                try
                {
                    var imageData = await GetImageFromUriAsync(url);

                    ContentValues image = new ContentValues();
                    var imageName = fileName.Split('.').First();
                    image.Put(MediaStore.Images.ImageColumns.DisplayName, string.Join(string.Empty, imageName.Split('_')));
                    image.Put(MediaStore.Images.ImageColumns.MimeType, "image/jpeg");

                    Android.Net.Uri savedImageUri = resolver.Insert(imagesCollection, image);

                    using (Android.Graphics.Bitmap bitmap = Android.Graphics.BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length))
                    {
                        using (Stream stream = resolver.OpenOutputStream(savedImageUri))
                        {
                            bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, stream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ImageService.SaveImageAsync Exception: {ex}");
                }
            }
        }

        public async Task<List<LocalFileInformation>> GetImagesAsync(IEnumerable<string> existingFileNames)
        {
            var tcs = new TaskCompletionSource<List<LocalFileInformation>>();

            var images = new List<LocalFileInformation>();

            if (Android.OS.Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.LollipopMr1 && !await tagit.Droid.Permissions.PermissionsHelper.RequestStorageAccess())
            {
                tcs.SetResult(images);

                return await tcs.Task;
            }

            Android.Net.Uri collection;
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Q)
            {
                collection = MediaStore.Images.Media.GetContentUri(MediaStore.VolumeExternal);
            }
            else
            {
                collection = MediaStore.Images.Media.ExternalContentUri;
            }

            var projection = new string[]
            {
                MediaStore.Images.ImageColumns.Id,
                MediaStore.Images.ImageColumns.DisplayName,
                MediaStore.Images.ImageColumns.DateTaken,
                MediaStore.Images.ImageColumns.BucketDisplayName
            };

            var sortOrder = MediaStore.Images.ImageColumns.DateAdded + " DESC";

            var context = this.Context;
            if (context != null)
            {
                using (var cursor = context.ContentResolver.Query(collection, projection, null, null, sortOrder))
                {
                    int idColumn = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.Id);
                    int nameColumn = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.DisplayName);
                    int dateTakenColumn = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.DateTaken);
                    int folderNameColumn = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.BucketDisplayName);

                    var imageCountLimit = Common.CoreConstants.ImageCountLimit;
                    int currentCountLimit = 0;
                    while (cursor.MoveToNext())
                    {
                        string name = cursor.GetString(nameColumn);
                        string folderName = cursor.GetString(folderNameColumn);
                        if (existingFileNames.Contains(name) || FoldersToExclude.Contains(folderName.ToLower()))
                        {
                            continue;
                        }

                        long id = cursor.GetLong(idColumn);
                        Android.Net.Uri contentUri = ContentUris.WithAppendedId(MediaStore.Images.Media.ExternalContentUri, id);

                        byte[] imageBytes = ReadBytesFromUri(contentUri, context);
                        if (imageBytes != null)
                        {
                            images.Add(new LocalFileInformation
                            {
                                FileName = name,
                                Url = contentUri.Path,
                                File = imageBytes,
                                CreatedDate = cursor.GetLong(dateTakenColumn).ToDateTime().ToLocalTime(),
                            });

                            currentCountLimit++;
                        }

                        if (currentCountLimit >= imageCountLimit)
                        {
                            break;
                        }
                    }
                }
            }

            tcs.SetResult(images);

            return await tcs.Task;
        }

        private static byte[] ReadBytesFromUri(Android.Net.Uri uri, Context context)
        {
            var stream = context.ContentResolver.OpenInputStream(uri);
            var byteArrayStream = new Java.IO.ByteArrayOutputStream();
            byte[] buffer = new byte[1024];

            int i = Java.Lang.Integer.MaxValue;
            while ((i = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                byteArrayStream.Write(buffer, 0, i);
            }

            var bytes = byteArrayStream.ToByteArray();

            if (bytes.Length > (int)Common.CoreConstants.ImageSizeLimit)
            {
                return null;
            }

            return bytes;
        }

        private async Task<byte[]> GetImageFromUriAsync(string uri)
        {
            var client = new HttpClient();

            return await client.GetByteArrayAsync(new System.Uri(uri));
        }
    }
}