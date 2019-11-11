using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.Content;
using Newtonsoft.Json;
using tagit.Common;
using tagit.Droid.Services;
using tagit.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageService))]
namespace tagit.Droid.Services
{
    /// Contains methods for accessing local images on the file system
    public class ImageService : IImageService
    {
        public async Task<string> SaveTaggedImageAsync(string fileName, FileTaggingInformation taggingInformation, byte[] image)
        {
            if (!await tagit.Droid.Permissions.PermissionsHelper.RequestStorageAccess())
            {
                return string.Empty;
            }

            var imagesFolder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);

            var imagesPath = imagesFolder.AbsolutePath;

            var taggedFolder = Path.Combine(imagesPath, "Tagged");

            var filePath = Path.Combine(taggedFolder, fileName);

            try
            {
                var imageData = await GetTaggedImageFromUriAsync(taggingInformation, image);

                if (!Directory.Exists(taggedFolder))
                {
                    Directory.CreateDirectory(taggedFolder);
                }

                File.WriteAllBytes(filePath, imageData);

                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);

                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(filePath)));

                Forms.Context.SendBroadcast(mediaScanIntent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ImageService.SaveTaggedImageAsync Exception: {ex}");
            }

            return string.Empty;
        }

        public async Task<List<string>> GetImageFileNamesAsync(IEnumerable<string> existingFileNames)
        {
            var tcs = new TaskCompletionSource<List<string>>();

            if (!await tagit.Droid.Permissions.PermissionsHelper.RequestStorageAccess())
            {
                tcs.SetResult(new List<string>());

                return await tcs.Task;
            }

            var imagesFolder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);

            var imagesPath = Path.Combine(imagesFolder.AbsolutePath, "Camera");

            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            var files = Directory.GetFiles(imagesPath).ToList();

            List<string> fileNames = new List<string>();

            foreach (var file in files)
            {
                string fileName = file.Split("/\\".ToCharArray()).LastOrDefault();

                if (!existingFileNames.ToList().Contains(fileName)) fileNames.Add(fileName);
            }

            tcs.SetResult(fileNames);

            return await tcs.Task;
        }
        
        public async Task SaveImageAsync(string fileName, string url)
        {
            var imagesFolder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);

            var imagesPath = imagesFolder.AbsolutePath;

            var filePath = Path.Combine(imagesPath, "Camera", fileName);

            try
            {
                var imageData = await GetImageFromUriAsync(url);

                File.WriteAllBytes(filePath, imageData);

                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);

                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(filePath)));

                Forms.Context.SendBroadcast(mediaScanIntent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ImageService.SaveImageAsync Exception: {ex}");
            }
        }


        public async Task<List<LocalFileInformation>> GetImagesAsync(IEnumerable<string> existingFileNames)
        {
            var tcs = new TaskCompletionSource<List<LocalFileInformation>>();

            var images = new List<LocalFileInformation>();

            if (!await tagit.Droid.Permissions.PermissionsHelper.RequestStorageAccess())
            {
                tcs.SetResult(images);

                return await tcs.Task;
            }

            var imagesFolder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);

            var imagesPath = Path.Combine(imagesFolder.AbsolutePath, "Camera");

            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            var files = Directory.GetFiles(imagesPath).ToList();

            var filteredFiles = files.Where(w => !existingFileNames.Contains(w.Split("/\\d".ToCharArray()).LastOrDefault())).OrderByDescending(p => File.GetLastWriteTime(p));

            foreach (var file in filteredFiles.Take(Common.CoreConstants.ImageCountLimit))
            {
                var fileSize = new FileInfo(file).Length;

                try
                {
                    byte[] imageBytes = null;

                    imageBytes = File.ReadAllBytes(file);

                    if (imageBytes.Length < (int)Common.CoreConstants.ImageSizeLimit)
                    {
                        images.Add(new LocalFileInformation
                        {
                            FileName = file.Split('\\').LastOrDefault(),
                            Url = file,
                            File = imageBytes,
                            CreatedDate = File.GetLastWriteTime(file)
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ImageService.SaveImageAsync Exception: {ex}");
                }
            }

            tcs.SetResult(images);

            return await tcs.Task;
        }

        private async Task<byte[]> GetTaggedImageFromUriAsync(FileTaggingInformation taggingInformation, byte[] image)
        {
            var client = new HttpClient();

            var url = $"{CoreConstants.TaggingServiceUrl}";

            var content = new MultipartContent();

            var payload = JsonConvert.SerializeObject(taggingInformation);

            var stringContent = new StringContent(payload);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var byteContent = new ByteArrayContent(image);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            content.Add(stringContent);
            content.Add(byteContent);

            var response = await client.PostAsync(url, content);

            var result = await response.Content.ReadAsByteArrayAsync();

            return result;
        }

        private async Task<byte[]> GetImageFromUriAsync(string uri)
        {
            var client = new HttpClient();

            return await client.GetByteArrayAsync(new System.Uri(uri));
        }
    }
}