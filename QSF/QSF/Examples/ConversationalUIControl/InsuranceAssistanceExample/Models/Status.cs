using Newtonsoft.Json;

namespace QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample.Models
{
    [JsonObject]
    public class Status
    {
        [JsonProperty("code")]
        public int? Code { get; set; }

        [JsonProperty("errorType")]
        public string ErrorType { get; set; }

        [JsonProperty("errorDetails")]
        public string ErrorDetails { get; set; }

        [JsonProperty("errorID")]
        public string ErrorID { get; set; }
    }
}