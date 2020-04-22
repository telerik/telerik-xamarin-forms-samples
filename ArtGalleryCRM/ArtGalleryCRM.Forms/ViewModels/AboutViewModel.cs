using System;
using System.Reflection;
using System.Windows.Input;
using Telerik.XamarinForms.Primitives;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels
{
    public class AboutViewModel : PageViewModelBase
    {
        private static string ApplicationDescriptionMessage = "This is a suite of professionally designed UI components for building modern, feature rich Xamarin.Forms apps from a single C# code base targeting the most popular mobile platforms such as Android and iOS, as well as UWP.";
        private static string ApplicationDescriptionMessageIOS = "This is a suite of professionally designed UI components for building modern, feature rich Xamarin.Forms apps from a single C# code base targeting the most popular mobile platforms.";
        public AboutViewModel()
        {
            this.Title = "About";

            OpenWebCommand = new Command(async (obj) =>
            {
                if (obj is string target)
                {
                    string url = string.Empty;

                    switch (target)
                    {
                        case "ProductPage":
                            url = "https://www.telerik.com/xamarin-ui";
                            break;
                        case "ReleaseHistoryPage":
                            url = "https://www.telerik.com/support/whats-new/xamarin-ui";
                            break;
                        case "DocumentationPage":
                            url = "https://docs.telerik.com/devtools/xamarin/introduction";
                            break;
                        case "GitHubDemosPage":
                            url = "https://github.com/telerik/telerik-xamarin-forms-samples";
                            break;
                        default:
                            url = "https://www.telerik.com";
                            break;
                    }

                    await Launcher.OpenAsync(new Uri(url));
                }
            });
        }

        public string TelerikDescription
        {
            get
            {
                var versionNumber = typeof(RadPath).GetTypeInfo().Assembly.FullName.Split(',')[1]?.Split('=')[1];
                return $"Progress© Telerik© UI for Xamarin, v {versionNumber}";
            }
        }

        public string AppName => "Art Gallery CRM";

        public string AppDescription => Device.RuntimePlatform == Device.iOS ? ApplicationDescriptionMessageIOS : ApplicationDescriptionMessage;

        public ICommand OpenWebCommand { get; }
    }
}