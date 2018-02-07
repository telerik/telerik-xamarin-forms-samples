using System.Threading.Tasks;
using tagit.Analysis;

namespace tagit.Services
{
    /// <summary>
    ///     Used to faciliate dependency injection for the
    ///     platform-specific ComputerVisionService
    /// </summary>
    public interface IComputerVisionService
    {
        Task<ImageAnalysisResult> AnalyzeImageAsync(byte[] bytes);
        Task<ImageAnalysisResult> AnalyzeImageAsync(string url);
    }
}