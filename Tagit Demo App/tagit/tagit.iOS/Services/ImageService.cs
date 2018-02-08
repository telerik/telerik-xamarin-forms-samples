using Foundation;
using Newtonsoft.Json;
using Photos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using tagit.Common;
using tagit.iOS.Services;
using tagit.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageService))]
namespace tagit.iOS.Services
{
    /// Contains methods for accessing
    /// local images on the file system
    public class ImageService : IImageService
    {
        private List<NSObject> imagesResult;
        private PHImageManager imageManager;
        private PHImageRequestOptions requestOptions;

        public ImageService()
        {
            this.imagesResult = new List<NSObject>();
            this.imageManager = PHImageManager.DefaultManager;
            this.requestOptions = new PHImageRequestOptions() { Synchronous = true };
        }

        public async Task<List<LocalFileInformation>> GetImagesAsync(IEnumerable<string> existingFileNames)
        {
            var tcs = new TaskCompletionSource<List<LocalFileInformation>>();
            var images = new List<LocalFileInformation>();
            await this.GetImagesAssets();

            foreach (PHAsset asset in this.imagesResult)
            {
                using (asset)
                {
                    string name = null;
                    string path = null;

                    imageManager.RequestImageData(asset, requestOptions, (data, dataUti, orientation, info) =>
                    {
                        name = (info?[(NSString)@"PHImageFileURLKey"] as NSUrl).FilePathUrl.LastPathComponent;
                        path = (info?[(NSString)@"PHImageFileURLKey"] as NSUrl).Path;
                        if (data != null)
                        {
                            Byte[] imageBytes = new Byte[data.Length];
                            System.Runtime.InteropServices.Marshal.Copy(data.Bytes, imageBytes, 0, Convert.ToInt32(data.Length));

                            images.Add(new LocalFileInformation
                            {
                                FileName = name,
                                Url = path,
                                File = imageBytes,
                                CreatedDate = ImageService.NSDateToDateTime(asset.CreationDate)
                            });
                        }

                    });
                }
            }
            
            tcs.SetResult(images);
            return await tcs.Task;
        }

        public async Task<List<string>> GetImageFileNamesAsync(IEnumerable<string> existingFileNames)
        {
            var tcs = new TaskCompletionSource<List<string>>();
            var images = new List<LocalFileInformation>();
            List<string> fileNames = new List<string>();
            await this.GetImagesAssets();
            
            foreach (PHAsset asset in this.imagesResult)
            {
                imageManager.RequestImageData(asset, requestOptions, (data, dataUti, orientation, info) =>
                {
                    var name = (info?[(NSString)@"PHImageFileURLKey"] as NSUrl).FilePathUrl.LastPathComponent;
                    fileNames.Add(name);
                });
            }

            tcs.SetResult(fileNames);
            return await tcs.Task;
        }
        
        public async Task<string> SaveTaggedImageAsync(string fileName, FileTaggingInformation taggingInformation,
            byte[] image)
        {
            var imagesFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var taggedFolder = Path.Combine(imagesFolder, "Tagged");
            var filePath = Path.Combine(taggedFolder, fileName);

            try
            {
                var imageData = await GetTaggedImageFromUriAsync(taggingInformation, image);

                if (!Directory.Exists(taggedFolder))
                {
                    Directory.CreateDirectory(taggedFolder);
                }

                var data = NSData.FromArray(imageData);
                var uiImage = UIImage.LoadFromData(data);

                NSData imgData = uiImage.AsJPEG();
                NSError err = null;
                imgData.Save(filePath, false, out err);
            }
            catch (Exception ex)
            {
            }

            return string.Empty;
        }

        public async Task SaveImageAsync(string fileName, string url)
        {
            var imagesFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var cameraFolder = Path.Combine(imagesFolder, "Camera");
            var filePath = Path.Combine(cameraFolder, fileName);

            try
            {
                var imageData = await GetImageFromUriAsync(url);

                if (!Directory.Exists(cameraFolder))
                {
                    Directory.CreateDirectory(cameraFolder);
                }

                var data = NSData.FromArray(imageData);
                var uiImage = UIImage.LoadFromData(data);

                uiImage.SaveToPhotosAlbum((image, error) => {
                    
                    Console.WriteLine("error:" + error);
                });
               
            }
            catch (Exception ex)
            {
            }
        }

        private async Task<byte[]> GetImageFromUriAsync(string uri)
        {
            var client = new HttpClient();
            return await client.GetByteArrayAsync(new Uri(uri));
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

        
        private Task GetImagesAssets()
        {
            this.imagesResult.Clear();
            var assets = PHAsset.FetchAssets(PHAssetMediaType.Image, new PHFetchOptions());
            assets.Enumerate(new PHFetchResultEnumerator(OnPHFetchResultEnumerator));

            return Task.FromResult(true);
        }

        private void OnPHFetchResultEnumerator(NSObject element, nuint elementIndex, out bool stop)
        {
            this.imagesResult.Add(element);
            stop = this.imagesResult.Count >= Common.CoreConstants.ImageCountLimit;
        }

        public static DateTime NSDateToDateTime(Foundation.NSDate date)
        {
            DateTime reference = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var utcDateTime = reference.AddSeconds(date.SecondsSinceReferenceDate);
            return utcDateTime.ToLocalTime();
        }
    }
}