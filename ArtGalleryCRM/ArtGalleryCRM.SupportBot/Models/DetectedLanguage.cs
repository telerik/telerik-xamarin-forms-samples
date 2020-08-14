using Newtonsoft.Json;

namespace ArtGalleryCRM.SupportBot.Models
{
    public class DetectedLanguage
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }
}