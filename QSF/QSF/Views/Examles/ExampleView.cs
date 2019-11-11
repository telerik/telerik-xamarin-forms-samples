using QSF.Services;
using QSF.ViewModels;
using System;
using Xamarin.Forms;

namespace QSF.Views
{
    public class ExampleView : ExamplesViewBase
    {
        private string currentThemeName;

        public ExampleView()
        {
            var themesService = DependencyService.Get<IThemesService>();
            this.currentThemeName = themesService.CurrentTheme.Name;
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

            var themesService = DependencyService.Get<IThemesService>();
            var themeName = themesService.CurrentTheme.Name;

            if (this.currentThemeName != themeName)
            {
                this.UpdateContent();
            }

            this.currentThemeName = themeName;
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
                var viewModel = (ExampleViewModel)this.BindingContext;
                var content = this.GetViewFromExampleInfo(viewModel.ExampleInfo);

                this.Content = content;
            }
        }


        private View GetViewFromExampleInfo(ExampleInfo exampleInfo)
        {
            var navigationService = DependencyService.Get<INavigationService>();
            var viewType = navigationService.GetExampleViewType(exampleInfo);

            return (View)Activator.CreateInstance(viewType);
        }
    }
}
