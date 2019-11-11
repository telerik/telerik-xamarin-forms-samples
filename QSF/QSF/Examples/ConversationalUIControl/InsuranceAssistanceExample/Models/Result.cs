using Newtonsoft.Json;
using System;

namespace QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample.Models
{
    [JsonObject]
    public class Result
    {
        [JsonProperty("fulfillment")]
        public Fulfillment Fulfillment { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}