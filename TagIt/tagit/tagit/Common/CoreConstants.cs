namespace tagit.Common
{
    public static class CoreConstants
    {
        // TODO: Set this to the region that the Azure Cognitive Services Computer Vision API key was generated for
        public static string CognitiveServicesRegion = "Your CognitiveServicesRegion here";
        public static string CognitiveServicesBaseUrl = $"https://{CognitiveServicesRegion}.api.cognitive.microsoft.com/vision/v1.0";

        // TODO: Replace with your own generated Computer Vision API Key
        // See https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/
        public static string ComputerVisionApiSubscriptionKey = "Your API Key here";

        // External training lab APIs for tagging and saving images locally
        public static string TaggingServiceUrl = "http://tagit-webapi.azurewebsites.net/api/tagit";
        public static string SampleImagesUrl = "https://xamarintagitstorage.blob.core.windows.net/tagit-sample-images/";

        // THIS NUMBER IS THE COMPUTER VISION UPPER LIMIT
        public static ulong ImageSizeLimit = 4194304;
        // THIS NUMBER IS AN ARBITRARY LIMIT TO PREVENT MASSIVE IMAGE COUNTS
        public static int ImageCountLimit = 100;
    }

    public static class UiConstants
    {
        public const string UploadImageLightFileName = "tagit.Images.ic_upload_light.png";
        public const string UploadImageDarkFileName = "tagit.Images.ic_upload_dark.png";
        public const string PaddedCalendarDisplayValue = "           (1)";
    }
}
