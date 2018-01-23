using Plugin.Settings;
using System;
using Xamarin.Forms;

namespace QSF.Views
{
    public partial class HomeView : ContentPage
    {
        private const string ShouldHideOnBoardingPageSettingsKey = "ShouldHideOnBoardingPage";

        public HomeView()
        {
            this.InitializeComponent();

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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            this.examplesList.UpdateListViewLayoutDefinition(160, 6, 10, 12);
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
    }
}