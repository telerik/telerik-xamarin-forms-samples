using Newtonsoft.Json;
using QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample
{
    public class DialogFlowBotService
    {
        private HttpClient httpClient;
        private JsonSerializerSettings jsonSettings;
        private Request request;
        private static string BotBaseAddress = "https://api.dialogflow.com/v1";
        private static string ConversationURL = DialogFlowBotService.BotBaseAddress + "/query?v=20150910";

        public DialogFlowBotService(string tokenKey)
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(DialogFlowBotService.BotBaseAddress);
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenKey);

            this.request = new Request();
            this.request.Lang = "en";
            this.request.SessionId = Guid.NewGuid().ToString();

            this.jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public async Task<Result> SendMessageAsync(string messageText)
        {
            this.request.Query = messageText;
            string jsonRequest = JsonConvert.SerializeObject(this.request, Formatting.None, this.jsonSettings);
            StringContent contentPost = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage responseMessage = await this.httpClient.PostAsync(DialogFlowBotService.ConversationURL, contentPost);
                Result result = null;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResult = await responseMessage.Content.ReadAsStringAsync();
                    Response response = JsonConvert.DeserializeObject<Response>(jsonResult);
                    if (response != null)
                    {
                        result = response.Result;
                    }
                }

                return result;
            }
            catch
            {
            }

            return null;
        }
    }
}
