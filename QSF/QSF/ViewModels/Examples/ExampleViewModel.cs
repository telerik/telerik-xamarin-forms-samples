using System;
using System.Threading.Tasks;
using QSF.Services;
using QSF.Services.Configuration;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public class ExampleViewModel : ExamplesViewModelBase
    {
        private Example example;

        public ExampleInfo ExampleInfo { get; private set; }

        private Example Example
        {
            get
            {
                if (this.example == null)
                {
                    var controlsService = DependencyService.Get<IControlsService>();
                    this.example = controlsService.GetControlExample(this.ExampleInfo.ControlName, this.ExampleInfo.ExampleName);
                }

                return this.example;
            }
        }

        internal override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.CanChangeTheme = this.Example.IsThemable && Xamarin.Forms.Application.Current.RequestedTheme != OSAppTheme.Dark;
            }
        }

        protected override Task InitializeAsyncOverride(object parameter)
        {
            this.ExampleInfo = (ExampleInfo)parameter;

            if (this.ExampleInfo != null)
            {
                this.Title = this.Example.DisplayName;
                this.CanChangeTheme = this.Example.IsThemable && Application.Current.RequestedTheme != OSAppTheme.Dark;
            }

            return Task.FromResult(false);
        }

        protected override Task NavigateToCodeOverride()
        {
            AnalyticsHelper.TraceNavigateToCode(this.ExampleInfo.ControlName, this.ExampleInfo.ExampleName);

            Launcher.OpenAsync(this.Example.CodeURL);

            return Task.FromResult(false);
        }

        protected override Task NavigateToInfoOverride()
        {
            InfoViewSettings settings = new InfoViewSettings(InfoType.ExampleInfo, this.Example.DescriptionHeader, this.Example.Description);

            return this.NavigationService.NavigateToAsync<InfoViewModel>(settings);
        }

        protected override Task NavigateToDocumentationOverride()
        {
            throw new InvalidOperationException("This view is not expected to have documentation to show.");
        }
    }
}
