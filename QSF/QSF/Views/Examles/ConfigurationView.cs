using QSF.Services;
using QSF.ViewModels;
using System;
using Xamarin.Forms;

namespace QSF.Views
{
    public class ConfigurationView : ContentPage
    {
        public ConfigurationView()
        {
            this.SetAppThemeColor(ContentPage.BackgroundColorProperty, (Color)App.Current.Resources["LightBackgroundColorLight"], (Color)App.Current.Resources["LightBackgroundColorDark"]);

            var controlTemplate = (ControlTemplate)App.Current.Resources["ConfigurationView"];

            this.ControlTemplate = controlTemplate;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            this.UpdateContent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as PageViewModelBase;

            if (viewModel != null)
            {
                viewModel.OnAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = this.BindingContext as PageViewModelBase;

            if (viewModel != null)
            {
                viewModel.OnDisappearing();
            }

            base.OnDisappearing();
        }

        private void UpdateContent()
        {
            if (this.BindingContext != null)
            {
                var content = this.GetViewFromExampleInfo();

                this.Content = content;
            }
        }


        private View GetViewFromExampleInfo()
        {
            var navigationService = DependencyService.Get<INavigationService>();
            var viewType = navigationService.GetViewTypeForViewModel(this.BindingContext.GetType());

            return (View)Activator.CreateInstance(viewType);
        }
    }
}
