using QSF.Services;
using QSF.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public class SampleAppsViewModel : PageViewModelBase
    {
        public SampleAppsViewModel()
        {
            this.AppBarLeftButtonCommand = new Command(async () => await this.NavigateBack());
            this.AppBarRightButtonCommand = new Command(this.NavigateToSearch);
            this.OpenInStoreCommand = new Command(this.OpenInStore);

            IConfigurationService configurationService = DependencyService.Get<IConfigurationService>();
            SampleApps sampleApps = configurationService.GetSampleAppsConfiguration();
            this.DescriptionHeader = sampleApps.DescriptionHeader;
            this.SampleApps = sampleApps.Apps;
            this.Title = sampleApps.SampleAppsPageTitle;
        }

        public string DescriptionHeader { get; }

        public IEnumerable<SampleApp> SampleApps { get; }

        public ICommand OpenInStoreCommand { get; }

        private void NavigateToSearch(object obj)
        {
            this.NavigationService.NavigateToAsync<SearchViewModel>();
        }

        private void OpenInStore(object obj)
        {
            SampleApp sampleApp = (SampleApp)obj;
            OpenAppInStore(sampleApp.AppStoreId);
        }

        private void OpenAppInStore(string appStoreId)
        {
            string platformUri = Device.RuntimePlatform == Device.Android ? "market://details?id=" :
                Device.RuntimePlatform == Device.iOS ? "itms-apps://apps.apple.com/app/" :
                Device.RuntimePlatform == Device.UWP ? "ms-windows-store://pdp/?ProductId=" : 
                null;

            string[] idsArray = appStoreId.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Func<int, string> getAppId = index => index < idsArray.Length ? idsArray[index] : idsArray[0];
            string platformAppId = Device.RuntimePlatform == Device.Android ? getAppId(0) :
                Device.RuntimePlatform == Device.iOS ? getAppId(1) :
                Device.RuntimePlatform == Device.UWP ? getAppId(2) :
                null;

            string uriString = string.Format("{0}{1}", platformUri, platformAppId);
            Uri uri = new Uri(uriString);
            Launcher.OpenAsync(uri);
        }
    }
}
