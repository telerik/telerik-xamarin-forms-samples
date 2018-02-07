using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tagit.Services;
using Xamarin.Forms;

namespace tagit.Helpers
{
    /// <summary>
    ///     Used to access the platform-specific ImageService methods
    /// </summary>
    internal static class ImageHelper
    {
        internal static async Task<List<string>> GetImageFileNamesAsync(IEnumerable<string> existingFileNames)
        {
            var service = DependencyService.Get<IImageService>();
            var fileNames = await service.GetImageFileNamesAsync(existingFileNames);

            return fileNames;
        }
        
        internal static async Task<List<LocalFileInformation>> GetImagesAsync(IEnumerable<string> existingFileNames)
        {
            var service = DependencyService.Get<IImageService>();
            
            var images = await service.GetImagesAsync(existingFileNames);
            
            return images;
        }

        internal static async Task SaveImageToDiskAsync(string fileName, string url)
        {
            var service = DependencyService.Get<IImageService>();
            await service.SaveImageAsync(fileName, url);
        }

        internal static async Task<string> SaveTaggedImageToDiskAsync(string fileName, byte[] image, string caption,
            List<string> tags, short rating)
        {
            var service = DependencyService.Get<IImageService>();
            var filePath = await service.SaveTaggedImageAsync(fileName.Split("/\\".ToCharArray()).LastOrDefault(),
                new FileTaggingInformation
                {
                    Caption = caption,
                    Tags = tags,
                    Rating = rating
                }, image);

            return filePath;
        }
    }
}