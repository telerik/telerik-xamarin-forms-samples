using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryCRM.SupportBot.Models;
using Newtonsoft.Json;

namespace ArtGalleryCRM.SupportBot.Services
{
    public class TextTranslationServiceClient : IDisposable
    {
        private readonly HttpClient client;

        public TextTranslationServiceClient(string subscriptionKey)
        {
            this.client = new HttpClient();

            if(string.IsNullOrEmpty(SubscriptionKeys.TextTranslationServiceKey))
            {
                throw new NullReferenceException("You need to add your Cognitive Services Subscription Key. Visit https://azure.microsoft.com/en-us/try/cognitive-services/ to get started.");
            }

            this.client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
        }

        public async Task<TranslationResponse> TranslateAsync(string message, string languageIsoCode)
        {
            var urlString = $"https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&to={languageIsoCode}";

            var requestBody = JsonConvert.SerializeObject(new object[] { new { Text = message } });

            try
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(urlString);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                    using (var response = await this.client.SendAsync(request))
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<TranslationResponse>>(responseBody);
                        return result.FirstOrDefault();
                    }
                }
            }
            catch
            {
                return new TranslationResponse
                {
                    DetectedLanguage = new DetectedLanguage
                    {
                        Language = string.Empty,
                        Score = 0
                    },
                    Translations = new List<Translation>
                    {
                        new Translation
                        {
                            Text = string.Empty
                        }
                    }
                };
            }
        }

        public void Dispose()
        {
            this.client?.Dispose();
        }
    }
}