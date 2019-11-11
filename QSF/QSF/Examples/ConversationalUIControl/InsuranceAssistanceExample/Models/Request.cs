using Newtonsoft.Json;

namespace QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample.Models
{
    public class Request
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
    }
}