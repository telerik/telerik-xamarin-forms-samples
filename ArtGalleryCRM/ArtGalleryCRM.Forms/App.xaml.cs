using System;
using System.Net.Http;
using System.Threading.Tasks;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Services;
using ArtGalleryCRM.Forms.Views;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms
{
    public partial class App : Application
    {
        public static MobileServiceClient MobileService { get; set; }

        internal static MasterDetailPage RootPage { get; set; }

        public App()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(ServiceConstants.AzureMobileAppUrl))
            {
                throw new NotImplementedException("You need to update ArtGalleryCRM (Portable)/Helpers/ServiceConstants.cs with your Azure Mobile Service URL.");
            }

            DependencyService.Register<AzureEmployeeDataStore>();
            DependencyService.Register<AzureCustomerDataStore>();
            DependencyService.Register<AzureProductDataStore>();
            DependencyService.Register<AzureOrderDataStore>();

            MobileService = new MobileServiceClient(ServiceConstants.AzureMobileAppUrl, new HttpClientHandler())
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings
                {
                    CamelCasePropertyNames = true
                }
            };

            RootPage = new RootPage();

            Current.MainPage = RootPage;
            
            if (Settings.IsFirstRun)
            {
                Current.MainPage.Navigation.PushModalAsync(new WelcomePage());
            }
        }

        public static async Task DisplayReadOnlyDemoAlert()
        {
            await RootPage.DisplayAlert(
                "Read Only", 
                "The app is currently read-only to prevent data corruption, deploy the app to your Azure Mobile Services to enable editing of your company's data.\n\n",
                "CLOSE");
        }
    }
}