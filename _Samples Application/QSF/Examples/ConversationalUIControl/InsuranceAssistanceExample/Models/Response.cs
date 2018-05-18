using Newtonsoft.Json;
using System;

namespace QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample.Models
{
    public class Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

    }
}