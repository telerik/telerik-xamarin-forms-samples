using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Hangfire.Dashboard;
using Microsoft.Owin;

namespace ArtGalleryCRMService.Models
{
    // This is to enable Hangfire for background server-based jobs to help maintain the database
    // It is not related to, or required by, Azure Mobile Service
    public class BasicDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly BasicAuthAuthorizationFilterOptions options;

        public BasicDashboardAuthorizationFilter()
            : this(new BasicAuthAuthorizationFilterOptions())
        {
        }

        public BasicDashboardAuthorizationFilter(BasicAuthAuthorizationFilterOptions options)
        {
            this.options = options;
        }

        public bool Authorize(DashboardContext dashboardContext)
        {
            var context = new OwinContext(dashboardContext.GetOwinEnvironment());

            if (options.SslRedirect == true && context.Request.Uri.Scheme != "https")
            {
                context.Response.OnSendingHeaders(state =>
                {
                    var redirectUri = new UriBuilder("https", context.Request.Uri.Host, 443, context.Request.Uri.PathAndQuery).ToString();

                    context.Response.StatusCode = 301;
                    context.Response.Redirect(redirectUri);

                }, null);

                return false;
            }

            if (options.RequireSsl == true && context.Request.IsSecure == false)
            {
                context.Response.Write("Secure connection is required to access Hangfire Dashboard.");

                return false;
            }

            var header = context.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(header) == false)
            {
                var authValues = AuthenticationHeaderValue.Parse(header);

                if ("Basic".Equals(authValues.Scheme, StringComparison.InvariantCultureIgnoreCase))
                {
                    var parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter));

                    var parts = parameter.Split(':');

                    if (parts.Length > 1)
                    {
                        var login = parts[0];

                        var password = parts[1];

                        if (string.IsNullOrWhiteSpace(login) == false && string.IsNullOrWhiteSpace(password) == false)
                        {
                            return options.Users.Any(user => user.Validate(login, password, options.LoginCaseSensitive)) || Challenge(context);
                        }
                    }
                }
            }

            return Challenge(context);
        }

        private static bool Challenge(OwinContext context)
        {
            context.Response.StatusCode = 401;
            context.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
            context.Response.Write("Authentication is required.");

            return false;
        }
    }
}