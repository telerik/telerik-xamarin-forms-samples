using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.ViewModels;

namespace ArtGalleryCRM.Forms.Views
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for ContentPage that supports and invokes IViewModel OnAppearing
    /// </summary>
    public class BasePage : Xamarin.Forms.ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(BindingContext != null && BindingContext is PageViewModelBase viewModel)
            {
                viewModel.OnAppearing();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext != null && BindingContext is PageViewModelBase viewModel)
            {
                return viewModel.OnBackButtonRequested();
            }

            return base.OnBackButtonPressed();
        }
    }
}