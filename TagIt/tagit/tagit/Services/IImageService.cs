using System.Collections.Generic;
using System.Threading.Tasks;

namespace tagit.Services
{
    /// <summary>
    ///     Used to faciliate dependency injection for the
    ///     platform-specific ImageService
    /// </summary>
    public interface IImageService
    {
        Task SaveImageAsync(string fileName, string url);
        Task<List<LocalFileInformation>> GetImagesAsync(IEnumerable<string> existingFileNames);
    }
}