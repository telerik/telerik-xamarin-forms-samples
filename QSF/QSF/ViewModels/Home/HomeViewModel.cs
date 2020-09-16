using QSF.Services;
using QSF.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public class HomeViewModel : PageViewModelBase
    {
        private ControlViewModel selectedControl;
        private bool isSideDrawerOpen;

        public HomeViewModel()
        {
            var configurationService = DependencyService.Get<IConfigurationService>();
            this.Title = configurationService.GetHomePageTitle();
            this.TitleIcon = configurationService.GetHomePageTitleIcon();
            this.SideDrawerHeaderTitle = configurationService.GetSideDrawerHeaderTitle();
            this.SideDrawerSubHeaderTitle = configurationService.GetSideDrawerSubHeaderTitle();
            this.SideDrawerHeaderIcon = configurationService.GetSideDrawerHeaderIcon();

            var controlsService = DependencyService.Get<IControlsService>();

            var featured = controlsService.GetFeatured();
            this.Featured = new ObservableCollection<ControlViewModel>(this.ToViewModels(featured));

            var allControls = controlsService.GetAllControls();
            this.AllControls = new ObservableCollection<ControlViewModel>(this.ToViewModels(allControls));

            this.AppBarLeftButtonCommand = new Command(this.ToggleIsSideDrawerOpen);
            this.AppBarRightButtonCommand = new Command(this.NavigateToSearch);
            this.TapCommand = new Command(this.SlideViewTap);
            this.NavigateToAboutCommand = new Command(this.NavigateToAbout);
            this.NavigateToSourceCommand = new Command(this.NavigateToSource);
            this.NavigateToDocumentationCommand = new Command(this.NavigateToDocumentation);
            this.NavigateToProductPageCommand = new Command(this.NavigateToProductPage);
            this.NavigateToWhatsNewPageCommand = new Command(this.NavigateToWhatsNewPage);
            this.NavigateToPrivacyPolicyPageCommand = new Command(this.NavigateToPrivacyPolicyPage);
            this.NavigateToSampleAppsPageCommand = new Command(this.NavigateToSampleAppsPage);
            this.NavigateToSampleAppsText = configurationService.GetSampleAppsConfiguration().NavigateToSampleAppsText;
        }

        public string SideDrawerHeaderIcon { get; }

        public string SideDrawerHeaderTitle { get; }

        public string SideDrawerSubHeaderTitle { get; }

        public ObservableCollection<ControlViewModel> Featured { get; }

        public ObservableCollection<ControlViewModel> AllControls { get; }

        public ICommand TapCommand { get; }

        public ControlViewModel SelectedControl
        {
            get
            {
                return this.selectedControl;
            }
            set
            {
                if (this.selectedControl != value)
                {
                    this.selectedControl = value;
                    this.OnSelectedControlChanged();
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsSideDrawerOpen
        {
            get
            {
                return this.isSideDrawerOpen;
            }
            set
            {
                if (this.isSideDrawerOpen != value)
                {
                    this.isSideDrawerOpen = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand NavigateToAboutCommand { get; }

        public ICommand NavigateToSourceCommand { get; }

        public ICommand NavigateToDocumentationCommand { get; }

        public ICommand NavigateToProductPageCommand { get; }

        public ICommand NavigateToWhatsNewPageCommand { get; }

        public ICommand NavigateToPrivacyPolicyPageCommand { get; }

        public ICommand NavigateToSampleAppsPageCommand { get; }

        public string NavigateToSampleAppsText { get; }

        private void ToggleIsSideDrawerOpen(object obj)
        {
            this.IsSideDrawerOpen = !this.IsSideDrawerOpen;
        }

        private void NavigateToSearch(object obj)
        {
            this.NavigationService.NavigateToAsync<SearchViewModel>();
        }

        private void NavigateToAbout(object obj)
        {
            var configurationService = DependencyService.Get<IConfigurationService>();
            var aboutContent = configurationService.GetQSFAboutContent();
            var aboutHeader = configurationService.GetQSFAboutHeader();
            var aboutImageName = configurationService.GetQSFAboutImageName();
            var aboutLinkText = configurationService.GetQSFAboutLinkText();

            InfoViewSettings settings = new InfoViewSettings(InfoType.About, aboutHeader, aboutContent, aboutLinkText, aboutImageName);
            this.NavigationService.NavigateToAsync<InfoViewModel>(settings);
        }

        private void NavigateToSource(object obj)
        {
            AnalyticsHelper.TraceNavigateToCode();
            var configurationService = DependencyService.Get<IConfigurationService>();
            Launcher.OpenAsync(configurationService.GetSourceURL());
        }

        private void NavigateToDocumentation(object obj)
        {
            AnalyticsHelper.TraceNavigateToDocumentation();
            var configurationService = DependencyService.Get<IConfigurationService>();
            Launcher.OpenAsync(configurationService.GetDocumentationURL());
        }

        private void NavigateToProductPage(object obj)
        {
            AnalyticsHelper.TraceNavigateToProductPage();
            var configurationService = DependencyService.Get<IConfigurationService>();
            Launcher.OpenAsync(configurationService.GetProductPageURL());
        }

        private void NavigateToWhatsNewPage(object obj)
        {
            AnalyticsHelper.TraceNavigateToWhatsNewPage();
            var configurationService = DependencyService.Get<IConfigurationService>();
            Launcher.OpenAsync(configurationService.GetWhatsNewPageURL());
        }

        private void NavigateToPrivacyPolicyPage(object obj)
        {
            AnalyticsHelper.TraceNavigateToWhatsNewPage();
            var configurationService = DependencyService.Get<IConfigurationService>();
            Launcher.OpenAsync(configurationService.GetPrivacyPolicyPageURL());
        }

        private void NavigateToSampleAppsPage(object obj)
        {
            this.NavigationService.NavigateToAsync<SampleAppsViewModel>();
        }

        private void SlideViewTap(object obj)
        {
            ControlViewModel controlViewModel = obj as ControlViewModel;

            this.OpenControlExamplesView(controlViewModel);
        }

        private void OnSelectedControlChanged()
        {
            this.OpenControlExamplesView(this.SelectedControl);

            this.SelectedControl = null;
        }

        private void OpenControlExamplesView(ControlViewModel controlViewModel)
        {
            if (controlViewModel != null)
            {
                this.NavigationService.NavigateToAsync<ControlExamplesViewModel>(controlViewModel.Name);
            }
        }

        private IEnumerable<ControlViewModel> ToViewModels(IEnumerable<Control> controls)
        {
            return controls.Select(p => new ControlViewModel(p));
        }
    }
}
