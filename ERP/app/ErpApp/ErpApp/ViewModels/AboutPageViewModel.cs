using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ErpApp.ViewModels
{
    public class AboutPageViewModel : MvxViewModel
    {
        public string HyperlinkText { get; }
        public string TrailingContent { get; }
        public string Header { get; }
        public string LogoImage { get; }
        public ICommand LinkTapped { get; }

        public AboutPageViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.LogoImage = "TelerikXamarin_Logo.png";
            this.Header = "Overview";
            this.HyperlinkText = @"Telerik® UI for Xamarin";
            this.TrailingContent = @"This is a suite of professionally designed UI components for building modern, feature rich Xamarin.Forms apps from a single C# code base targeting the most popular mobile platforms such as Android and iOS, as well as UWP.";
            this.LinkTapped = new Command(OnLinkTapped);
        }

        private IMvxNavigationService navigationService;

        private void OnLinkTapped(object obj)
        {
            Device.OpenUri(new Uri(obj.ToString()));
        }
    }
}