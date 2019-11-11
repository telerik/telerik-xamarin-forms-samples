using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using telerikerpService.Models;
using Owin;

namespace telerikerpService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<telerikerpContext, telerikerpService.Migrations.Configuration>());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    }
}

