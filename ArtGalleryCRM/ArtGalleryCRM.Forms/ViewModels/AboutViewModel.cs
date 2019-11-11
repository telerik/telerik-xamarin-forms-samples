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

        public string AppDescription => "This app uses Telerik UI for Xamarin, a collection of Xamarin.Forms controls and Xamarin bindings built on top of the native Telerik UI for Windows Universal Platform, Telerik UI for iOS and UI for Android suites, providing a common API that allows the developer to use the native Telerik components on all three mobile platforms (iOS / Android / Windows) using a single shared code base.\r\n\nIn addition you get access to the full set of controls in UI for Xamarin.IOS, Xamarin.Android and UI for UWP.";

        public ICommand OpenWebCommand { get; }
    }
}