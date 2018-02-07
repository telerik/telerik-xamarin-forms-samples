using System.Threading.Tasks;
using tagit.Analysis;
using tagit.Services;
using Xamarin.Forms;

namespace tagit.Helpers
{
    /// <summary>
    ///     Used to access the platform-specific ComputerVisionService methods
    /// </summary>
    internal static class AnalysisHelper
    {
        internal static async Task<ImageAnalysisResult> AnalyzeImageAsync(byte[] bytes)
        {
            var service = DependencyService.Get<IComputerVisionService>();
            var result = await service.AnalyzeImageAsync(bytes);

            return result;
        }

        internal static async Task<ImageAnalysisResult> AnalyzeImageAsync(string url)
        {
            var service = DependencyService.Get<IComputerVisionService>();
            var result = await service.AnalyzeImageAsync(url);

            return result;
        }
    }
}