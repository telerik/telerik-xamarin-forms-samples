using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using ArtGalleryCRMService.Helpers;
using ArtGalleryCRMService.Models;
using Owin;
using Hangfire;
using Hangfire.Dashboard;
using Swashbuckle.Application;

namespace ArtGalleryCRMService
{
    public partial class Startup
    {
        // Entry method
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.EnableSystemDiagnosticsTracing();
            
            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new ArtGalleryCRMInitializer());

            var settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
            
#if DEBUG
            // Hangfire initialization is skipped while debugging locally.
#else
            // Initializes Hangfire if running in production (Hangfire maintains the sample database's integrity)
            InitializeHangFire(app);
#endif

            // This is to enable a Swagger UI, a nice browser-based API documentation portal
            var version = typeof(Startup).Assembly.GetName().Version;
            config.EnableSwagger(c =>
                {
                    c.SingleApiVersion($"v{version.Major}.{version.Minor}.{version.Build}.{version.Revision}", "Art Gallery CRM - Mobile Services API");
                    c.PrettyPrint();
                    c.DescribeAllEnumsAsStrings();
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("Art Gallery CRM Swagger");
                });
        }
        
        // This is to enable Hangfire for background server-based jobs to help maintain the database
        // It is not related to, or required by, Azure Mobile Service
        private static void InitializeHangFire(IAppBuilder app)
        {
            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("MS_TableConnectionString");
            
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new List<IDashboardAuthorizationFilter>
                {
                    new BasicDashboardAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                    {
                        RequireSsl = true,
                        LoginCaseSensitive = false,
                        Users = new[]
                        {
                            // Todo IMPORTANT!!! Remove Hangfire code before publishing source. Keep it intact so we can maintain database for demo.
                            new BasicAuthAuthorizationUser
                            {
                                Login = "hangfire_admin",
                                PasswordClear = "hangfire_admin"
                            }
                        }
                    })
                }
            });

            app.UseHangfireServer();

            // This updates a record every 10 minutes with a timestamp in the Notes property for a specific employee
            RecurringJob.AddOrUpdate(() => new SampleDatabaseHelpers().HeartbeatTestJob(), Cron.MinuteInterval(10));

            // Recurring job to move each order's OrderDate up one day so that the data in the demo app is always in view on the ShippingCalendar
            RecurringJob.AddOrUpdate(() => new SampleDatabaseHelpers().UpdateOrderDatesJob(), Cron.Daily);

            // This recurring job resets the database records to keep them recent and clean once a week
            RecurringJob.AddOrUpdate(() => new SampleDatabaseHelpers().ResetDatabaseJob(), Cron.Weekly);
        }
    }
}