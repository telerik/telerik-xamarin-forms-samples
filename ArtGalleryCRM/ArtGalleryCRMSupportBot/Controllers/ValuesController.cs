using System.Web.Http;

namespace ArtGalleryCRMSupportBot.Controllers
{
    public class ValuesController : ApiController
    {
        // This controller is for confirming that the web app is published and running.
        // It does not need any BotAuthentication or special parameters, just make a GET request to your_base_url/api/values
        public string Get()
        {
            var version = typeof(ValuesController).Assembly.GetName().Version;

            return $"You are using Art Gallery CRM Support Bot version {version.Major}.{version.Minor}.{version.Build}.{version.Revision}.";
        }
    }
}