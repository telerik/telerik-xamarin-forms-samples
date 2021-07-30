using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Foundation;
using Photos;
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
                        var fileURL = info?[(NSString)@"PHImageFileURLKey"] as NSUrl;
                        name = fileURL?.FilePathUrl.LastPathComponent ?? asset.LocalIdentifier;
                        path = fileURL?.Path;
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

                uiImage.SaveToPhotosAlbum((image, error) =>
                {
                    if (error != null)
                    {
                        Console.WriteLine("ImageService.SaveImageAsync SaveToPhotosAlbum for {0} failed with error: {1}", fileName, error);
                    }
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ImageService.SaveImageAsync Exception: {ex}");
            }
        }

        private async Task<byte[]> GetImageFromUriAsync(string uri)
        {
            var client = new HttpClient();
            return await client.GetByteArrayAsync(new Uri(uri));
        }

        private Task GetImagesAssets()
        {
            this.imagesResult.Clear();
            var assets = PHAsset.FetchAssets(
                PHAssetMediaType.Image,
                new PHFetchOptions()
                {
                    IncludeAssetSourceTypes = PHAssetSourceType.UserLibrary,
                    IncludeHiddenAssets = true,
                    SortDescriptors = new NSSortDescriptor[] { new NSSortDescriptor("creationDate", false) }
                });
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