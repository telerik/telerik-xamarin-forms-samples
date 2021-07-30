using System.Collections.Generic;
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
    }
}