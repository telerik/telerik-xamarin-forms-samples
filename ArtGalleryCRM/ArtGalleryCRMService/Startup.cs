using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ArtGalleryCRMService.Startup))]
namespace ArtGalleryCRMService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}