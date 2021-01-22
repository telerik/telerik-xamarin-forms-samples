using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using tagit.Services;
using tagit.UWP.Services;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageService))]
namespace tagit.UWP.Services
{
    /// Contains methods for accessing local images on the file system
    public class ImageService : IImageService
    {
        public async Task<List<LocalFileInformation>> GetImagesAsync(IEnumerable<string> existingFileNames)
        {
            var images = new List<LocalFileInformation>();

            var imagesFolder = KnownFolders.PicturesLibrary;
            var cameraRollFolder = await imagesFolder.GetFolderAsync("Camera Roll");

            var files = await cameraRollFolder.GetFilesAsync();
            
            var filteredFiles = files.Where(w => !existingFileNames.Contains(w.Name.Split("/\\d".ToCharArray()).LastOrDefault())).OrderByDescending(p => p.DateCreated);
            
            foreach (var file in filteredFiles.Take(tagit.Common.CoreConstants.ImageCountLimit))
            {
                BasicProperties properties = await file.GetBasicPropertiesAsync();

                if (properties.Size < tagit.Common.CoreConstants.ImageSizeLimit)
                {
                    try
                    {
                        var bytes = await file.ReadFileAsync();

                        images.Add(new LocalFileInformation
                        {
                            FileName = file.Name,
                            Url = file.Path,
                            File = bytes,
                            CreatedDate = properties.DateModified.Date
                        });
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"ImageService.GetImagesAsync Exception: {ex}");
                    }
                }
            }

            return images;
        }

        public async Task SaveImageAsync(string fileName, string url)
        {
            try
            {
                var imageData = await GetImageFromUriAsync(url);

                var imagesFolder = KnownFolders.PicturesLibrary;

                var cameraRollFolder = await imagesFolder.GetFolderAsync("Camera Roll");

                var file = await cameraRollFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                await FileIO.WriteBytesAsync(file, imageData);
            }
            catch
            {
            }
        }

        private async Task<byte[]> GetImageFromUriAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                return await client.GetByteArrayAsync(new Uri(uri));
            }
        }
    }
}