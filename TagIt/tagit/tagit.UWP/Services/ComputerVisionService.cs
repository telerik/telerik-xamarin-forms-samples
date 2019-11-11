using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Newtonsoft.Json;
using tagit.Analysis;
using tagit.Common;
using tagit.Services;
using tagit.UWP.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ComputerVisionService))]
namespace tagit.UWP.Services
{
    ///Contains methods for accessing Microsoft Cognitive Services Computer Vision APIs
    public class ComputerVisionService : IComputerVisionService
    {
        public async Task<ImageAnalysisResult> AnalyzeImageAsync(byte[] bytes)
        {
            return await GetImageAnalysisAsync(bytes);
        }

        public async Task<ImageAnalysisResult> AnalyzeImageAsync(string url)
        {
            var bytes = await GetImageFromUriAsync(url);

            var results = await GetImageAnalysisAsync(bytes);

            return results;
        }
        
        private async Task<byte[]> GetImageFromUriAsync(string uri)
        {
            using(var webClient = new HttpClient())
            {
                var result = await webClient.GetBufferAsync(new Uri(uri));

                return result.ToArray();
            }
        }

        private async Task<ImageAnalysisResult> GetImageAnalysisAsync(byte[] bytes)
        {
            ImageAnalysisResult result = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", CoreConstants.ComputerVisionApiSubscriptionKey);

                    var payload = new HttpBufferContent(bytes.AsBuffer());

                    payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/octet-stream");

                    var analysisFeatures = "Color,ImageType,Tags,Categories,Description,Adult,Faces";

                    var uri = new Uri($"{CoreConstants.CognitiveServicesBaseUrl}/analyze?visualFeatures={analysisFeatures}");

                    using (var results = await client.PostAsync(uri, payload))
                    {
                        var analysisResults = await results.Content.ReadAsStringAsync();

                        var imageAnalysisResult = JsonConvert.DeserializeObject<ImageAnalysisInfo>(analysisResults);

                        result = new ImageAnalysisResult
                        {
                            id = Guid.NewGuid().ToString(),
                            details = imageAnalysisResult,
                            caption = imageAnalysisResult.description.captions.FirstOrDefault()?.text,
                            tags = imageAnalysisResult.description.tags.ToList()
                        };

                        if (string.IsNullOrEmpty(result.caption))
                        {
                            result.caption = "No caption";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ComputerVisionService.GetImageAnalysisAsync Exception: {ex}");
            }

            return result;
        }
    }
}