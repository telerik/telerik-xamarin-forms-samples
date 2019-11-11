using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArtGalleryCRMSupportBot.Models
{
    public class TranslationResponse
    {
        [JsonProperty("detectedLanguage")]
        public DetectedLanguage DetectedLanguage { get; set; }

        [JsonProperty("translations")]
        public List<Translation> Translations { get; set; }
    }
}