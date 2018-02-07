namespace tagit.Common
{
    public static class CoreConstants
    {
        //This can be changed to point to you own instance
        //of the Azure Cognitive Services Computer Vision API 
        //endpoints and values
        public static string CognitiveServicesRegion = "westus";
        public static string CognitiveServicesBaseUrl = $"https://{CognitiveServicesRegion}.api.cognitive.microsoft.com";

        //The Computer Vision API subscription key may be used during evaluation
        //Be sure to change this to your own instance of Computer Vision API
        //at https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/
        //as this key will be disabled after evaluation
        public static string ComputerVisionApiSubscriptionKey = "9369e75ef5974d339e939ac0520a805d";

        //External training lab APIs for tagging and saving images locally
        public static string TaggingServiceUrl = "https://traininglabservices.azurewebsites.net/api/tagit";
        public static string SampleImagesUrl = "https://tagit.blob.core.windows.net/sample-images/";
        
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
