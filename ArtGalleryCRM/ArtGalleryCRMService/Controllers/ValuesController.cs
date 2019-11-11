using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;

namespace ArtGalleryCRMService.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use from your mobile clients 
    [MobileAppController]
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            var settings = Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
            var traceWriter = Configuration.Services.GetTraceWriter();
            
            var version = typeof(ValuesController).Assembly.GetName().Version;
            var host = settings.HostName ?? "localhost";

            var greeting = $"You are using Art Gallery CRM Data Services from {host}, version {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            
            traceWriter.Info(greeting);

            return greeting;
        }
    }
}
