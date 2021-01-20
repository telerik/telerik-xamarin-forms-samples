using QSF.Services;
using Xamarin.Forms;

namespace QSF.Views
{
    public partial class ThemesView : ContentPage
    {
        public ThemesView()
        {
            this.InitializeComponent();

            // The PageRenderer by default sets BackgroundColor for the NavigationPage to White: https://github.com/xamarin/Xamarin.Forms/blob/ec380e4e24b213500a01992b3e10c5e8a1af3b3a/Xamarin.Forms.Platform.iOS/Renderers/PageRenderer.cs#L537
            // Because of that a Background should be set in order to make the StatusBar in iOS be visible when the mode is either dark or light.
            this.SetAppThemeColor(ContentPage.BackgroundColorProperty, (Color)App.Current.Resources["DarkBackgroundColorLight"], (Color)App.Current.Resources["DarkBackgroundColorDark"]);
        }

        protected override void OnAppearing()
        {
            if (Device.RuntimePlatform == Device.iOS && Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                var navigationService = DependencyService.Get<INavigationService>();
                navigationService.NavigateBackAsync();
            }

            base.OnAppearing();
        }
    }
}