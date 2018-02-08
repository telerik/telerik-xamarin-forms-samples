using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using tagit.Common;
using tagit.Services;
using tagit.UWP.Services;
using Xamarin.Forms;
using Windows.Storage.FileProperties;
using Xamarin.Forms.Xaml;


[assembly: Dependency(typeof(ImageService))]

namespace tagit.UWP.Services
{
    /// Contains methods for accessing
    /// local images on the file system
    public class ImageService : IImageService
    {
        public async Task<List<LocalFileInformation>> GetImagesAsync(IEnumerable<string> existingFileNames)
        {
            var images = new List<LocalFileInformation>();

            var imagesFolder = KnownFolders.PicturesLibrary;
            var cameraRollFolder = await imagesFolder.GetFolderAsync("Camera Roll");

            var files = await cameraRollFolder.GetFilesAsync();
            
            var filteredFiles = files.Where(w => !existingFileNames.Contains(w.Name.Split("/\\d".ToCharArray()).LastOrDefault()));
            
            foreach (var file in filteredFiles.Take(Common.CoreConstants.ImageCountLimit))
            {
                BasicProperties properties = await file.GetBasicPropertiesAsync();

                if (properties.Size < Common.CoreConstants.ImageSizeLimit)
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
                    catch { }
                    
                }
                
            }

            return images;
        }

        public async Task<List<string>> GetImageFileNamesAsync(IEnumerable<string> existingFileNames)
        {
            return new List<string>();
        }
        
        public async Task<string> SaveTaggedImageAsync(string fileName, FileTaggingInformation taggingInformation,
            byte[] image)
        {
            var filePath = string.Empty;

            try
            {
                var imageData = await GetTaggedImageFromUriAsync(taggingInformation, image);

                var imagesFolder = KnownFolders.PicturesLibrary;
                var taggedFolder = await imagesFolder.CreateFolderAsync("Tagged", CreationCollisionOption.OpenIfExists);

                var file = await taggedFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                await FileIO.WriteBytesAsync(file, imageData);

                filePath = file.Path;
            }
            catch
            {
            }

            return filePath;
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

            return await client.GetByteArrayAsync(new Uri(uri));
        }
    }
}