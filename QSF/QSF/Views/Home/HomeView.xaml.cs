using System;
using Plugin.Settings;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace QSF.Views
{
    public partial class HomeView : ContentPage
    {
        private const string ShouldHideOnBoardingPageSettingsKey = "ShouldHideOnBoardingPage";

        public HomeView()
        {
            this.InitializeComponent();

            // The PageRenderer by default sets BackgroundColor for the NavigationPage to White: https://github.com/xamarin/Xamarin.Forms/blob/ec380e4e24b213500a01992b3e10c5e8a1af3b3a/Xamarin.Forms.Platform.iOS/Renderers/PageRenderer.cs#L537
            // Because of that a Background should be set in order to make the StatusBar in iOS be visible when the mode is either dark or light.
            this.SetAppThemeColor(ContentPage.BackgroundColorProperty, (Color)App.Current.Resources["DarkBackgroundColorLight"], (Color)App.Current.Resources["DarkBackgroundColorDark"]);

            var shouldHideOnBoardingPage = CrossSettings.Current.GetValueOrDefault(ShouldHideOnBoardingPageSettingsKey, false);
            if (shouldHideOnBoardingPage)
            {
                this.RemoveOnBoardingPageFromUI();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.onBoarding.StartAutoSliding();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.onBoarding.StopAutoSliding();
        }

        private void OnBoardingClosing(object sender, EventArgs e)
        {
            this.onBoarding.StopAutoSliding();

            this.RemoveOnBoardingPageFromUI();

            var shouldShowOnBoardingPage = CrossSettings.Current.AddOrUpdateValue(ShouldHideOnBoardingPageSettingsKey, true);
        }

        private void RemoveOnBoardingPageFromUI()
        {
            var layout = this.onBoarding.Parent as Layout<View>;

            if (layout != null)
            {
                layout.Children.Remove(this.onBoarding);
            }
        }

        private void FeaturedSlideViewSlidedToIndex(object sender, Telerik.XamarinForms.Primitives.SlideView.SlideViewSlidedToIndexEventArgs e)
        {
            // This is a workaround for a bug in UWP where the ListView cannot measure it's Header accurately when the Header contains RadSlideView.
            if (Device.RuntimePlatform == Device.UWP)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var examplesListWidth = this.examplesList.Width;
                    if (examplesListWidth >= 0)
                    {
                        var margin = (Thickness)this.Resources["UWPMargin"];
                        var headerMargin = (Thickness)this.Resources["UWPHeaderMargin"];
                        var totalHorizontalMargin = margin.HorizontalThickness + headerMargin.HorizontalThickness;

                        var presenter = (RadSlideViewPresenter)sender;
                        var headerWidth = presenter.WidthRequest + totalHorizontalMargin;
                        if (examplesListWidth != headerWidth)
                        {
                            presenter.WidthRequest = examplesListWidth - totalHorizontalMargin;
                        }
                    }
                });
            }
        }
    }
}