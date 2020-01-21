using Newtonsoft.Json;

namespace ArtGalleryCRMSupportBot.Models
{
    public class Translation
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}