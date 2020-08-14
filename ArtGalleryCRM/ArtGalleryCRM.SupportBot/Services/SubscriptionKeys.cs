namespace ArtGalleryCRM.SupportBot.Services
{
    public static class SubscriptionKeys
    {
        // TODO Update with your TextAnalysis subscription key
        // See https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/
        public static string TextAnalysisServiceKey = "";

        // TODO Update with your TextAnalysis translation subscription key.
        // See https://azure.microsoft.com/en-us/services/cognitive-services/translator-text-api/
        public static string TextTranslationServiceKey = "";

        // TODO If you've enabled Bing spell check, put your Bing key here
        // See https://azure.microsoft.com/en-us/services/cognitive-services/spell-check/
        public static string BingSpellCheckKey = "";

        // Language Understanding (LUIS) Keys
        // See https://azure.microsoft.com/en-us/services/cognitive-services/language-understanding-intelligent-service/

        // TODO Update with your LUIS AppId (in the LUIS portal)
        public static string LuisAppId = "";

        // TODO Update with your LUIS prediction key (in the LUIS portal). Note, this is not the "Authoring" key.
        public static string LuisPredictionKey = "";

        // TODO Set this to the region that the prediction key was generated for.
        private const string serviceRegion = "";
        public static string LuisEndpoint = "";
    }
}