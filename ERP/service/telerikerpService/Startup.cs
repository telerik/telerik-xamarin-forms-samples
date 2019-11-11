using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(telerikerpService.Startup))]

namespace telerikerpService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}