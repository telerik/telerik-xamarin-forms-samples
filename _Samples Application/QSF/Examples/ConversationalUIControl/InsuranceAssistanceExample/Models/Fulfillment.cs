using Newtonsoft.Json;
using System.Collections.Generic;

namespace QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample.Models
{
    [JsonObject]
    public class Fulfillment
    {
        [JsonProperty("speech")]
        public string Speech { get; set; }

        [JsonProperty("displayText")]
        public string DisplayText { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("messages")]
        public List<object> Messages { get; set; }
    }
}