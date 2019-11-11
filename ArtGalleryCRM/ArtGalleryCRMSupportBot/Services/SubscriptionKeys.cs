namespace ArtGalleryCRMSupportBot.Services
{
    public static class SubscriptionKeys
    {
        // TODO IMPORTANT!!! Update with your TextAnalysis subscription key
        public static string TextAnalysisServiceKey = "6eaec93a87d84f0dbb1590e85daf8336";

        // TODO IMPORTANT!!! Update with your TextAnalysis subscription key. See https://docs.microsoft.com/en-us/azure/cognitive-services/translator/translator-text-how-to-signup
        public static string TextTranslationServiceKey = "91e39e3a8f0b455c93a4f26c13801b3a";

        // TODO IMPORTANT!!! Update with your LUIS AppId (in the LUIS portal)
        public static string LuisAppId = "9717ec79-06a6-489b-bf1d-f82e480ab3fd";

        // TODO IMPORTANT!!! Update with your LUIS prediction key (in the LUIS portal). Note, this is not the "Authoring" key.
        public static string LuisPredictionKey = "2e2941785db543ba8ef03e335161b232";

        // TODO IMPORTANT!!! Set this to the region that the prediction key was generated for.
        private const string serviceRegion = "westus";
        public static string LuisEndpoint = $"https://{serviceRegion}.api.cognitive.microsoft.com";

        // TODO IMPORTANT!!! If you've enabled Bing spell check, put your Bing key here
        public static string BingSpellCheckKey = "f29cc4f5d1e74ce380befbb4ba6836f9";
    }
}