using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class AboutPageModel : MainPageModelBase
    {
        private static string ApplicationDescriptionMessage = @"This is a suite of professionally designed UI components for building modern, feature rich Xamarin.Forms apps from a single C# code base targeting the most popular mobile platforms such as Android and iOS, as well as UWP.";
        private static string ApplicationDescriptionMessageIOS = @"This is a suite of professionally designed UI components for building modern, feature rich Xamarin.Forms apps from a single C# code base targeting the most popular mobile platforms.";
        public string HyperlinkText { get; }
        public string TrailingContent { get; }
        public string Header { get; }
        public string LogoImage { get; }
        public ICommand LinkTapped { get; }

        public AboutPageModel(Services.ITodoService todoService)
            : base(todoService)
        {
            this.LogoImage = "TelerikXamarin_Logo.png";
            this.Header = "Overview";
            this.HyperlinkText = @"Telerik® UI for Xamarin";
            this.TrailingContent = Device.RuntimePlatform == Device.iOS ? ApplicationDescriptionMessageIOS : ApplicationDescriptionMessage;
            this.LinkTapped = new Command(OnLinkTapped);
        }

        private void OnLinkTapped(object obj)
        {
            Launcher.OpenAsync(new Uri(obj.ToString()));
        }
    }
}