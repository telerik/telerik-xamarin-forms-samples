using System.Threading.Tasks;
using ArtGalleryCRM.Forms.Interfaces;
using CommonHelpers.Common;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels
{
    public class PageViewModelBase : ViewModelBase, IViewModel
    {
        public virtual async Task NavigateForwardAsync(Page page)
        {
            await App.RootPage.Detail.Navigation.PushAsync(page);
        }

        public virtual async Task NavigateBackAsync()
        {
            await App.RootPage.Detail.Navigation.PopAsync();
        }

        public virtual async Task ShowModalAsync(Page page)
        {
            await App.RootPage.Detail.Navigation.PushModalAsync(page, true);
        }

        public virtual async Task HideModalAsync(Page page)
        {
            await App.RootPage.Detail.Navigation.PopModalAsync(true);
        }

        // Overridden in discrete view model instances to load relevant data when the page is loaded.
        public virtual void OnAppearing() {}

        public virtual bool OnBackButtonRequested() => false;
    }
}