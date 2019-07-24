namespace tagit.Common
{
    public static class CoreConstants
    {
        //This can be changed to point to you own instance
        //of the Azure Cognitive Services Computer Vision API 
        //endpoints and values
        public static string CognitiveServicesRegion = "eastus2";
        public static string CognitiveServicesBaseUrl = $"https://{CognitiveServicesRegion}.api.cognitive.microsoft.com/vision.v1.0";

        //The Computer Vision API subscription key may be used during evaluation
        //Be sure to change this to your own instance of Computer Vision API
        //at https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/
        //as this key will be disabled after evaluation
        public static string ComputerVisionApiSubscriptionKey = "Your API Key here";

        //External training lab APIs for tagging and saving images locally
        public static string TaggingServiceUrl = "http://tagit-webapi.azurewebsites.net/api/tagit";
        public static string SampleImagesUrl = "https://tagitappstorage.blob.core.windows.net/tagit-sample-images/";
        
        public static ulong ImageSizeLimit = 4194304; //THIS NUMBER IS THE COMPUTER VISION UPPER LIMIT
        public static int ImageCountLimit = 100; //THIS NUMBER IS AN ARBITRARY LIMIT TO PREVENT MASSIVE IMAGE COUNTS
    }

    public static class UiConstants
    {
        public const string UploadImageLightFileName = "tagit.Images.ic_upload_light.png";
        public const string UploadImageDarkFileName = "tagit.Images.ic_upload_dark.png";
        public const string PaddedCalendarDisplayValue = "           (1)";
    }
}
