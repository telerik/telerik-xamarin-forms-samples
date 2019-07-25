using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FFImageLoading;
using Newtonsoft.Json;
using tagit.Analysis;
using tagit.Common;
using tagit.Droid.Services;
using tagit.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ComputerVisionService))]
namespace tagit.Droid.Services
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

            return await GetImageAnalysisAsync(bytes);
        }

        private async Task<byte[]> GetImageFromUriAsync(string uri)
        {
            using(var client = new HttpClient())
            {
                return await client.GetByteArrayAsync(new Uri(uri));
            }
        }

        private async Task<ImageAnalysisResult> GetImageAnalysisAsync(byte[] bytes)
        {
            ImageAnalysisResult result = null;

            try
            {
                using(var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", CoreConstants.ComputerVisionApiSubscriptionKey);

                    var payload = new ByteArrayContent(bytes);

                    payload.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    var analysisFeatures = "Color,ImageType,Tags,Categories,Description,Adult,Faces";

                    var uri = new Uri($"{CoreConstants.CognitiveServicesBaseUrl}/analyze?visualFeatures={analysisFeatures}");

                    using (var results = await Retry.DoAsync(() => client.PostAsync(uri, payload), new TimeSpan(0, 0, 3), 10))
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
                Console.WriteLine($"ComputerVisionService.GetImageAnalysisAsync Exception: {ex}");
            }

            return result;
        }
    }
}