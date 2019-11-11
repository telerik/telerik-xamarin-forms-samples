using Hangfire.Dashboard;
using Microsoft.Owin;

namespace ArtGalleryCRMService.Models
{
    // This is to enable Hangfire for background server-based jobs to help maintain the database
    // It is not related to, or required by, Azure Mobile Service
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var owinContext = new OwinContext(context.GetOwinEnvironment());

            return owinContext.Authentication.User.Identity.IsAuthenticated;
        }
    }
}