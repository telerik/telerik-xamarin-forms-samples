using System;
using System.Threading.Tasks;
using tagit.Common;
using tagit.Localization;
using tagit.Models;
using tagit.ViewModels;
using tagit.Views;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace tagit.Helpers
{
    internal class NavigationService
    {
        internal NavigationService(Page mainPage)
        {
            this.mainPage = mainPage;
        }

        internal void Initialize(RadSideDrawer sideDrawer)
        {
            this.drawer = sideDrawer;
        }

        private Page mainPage { get; }
        private RadSideDrawer drawer { get; set; }

        internal async Task PopModalAsync()
        {
            await mainPage.Navigation.PopModalAsync(true);
        }

        internal async Task PushModalAsync(Page page)
        {
            await mainPage.Navigation.PushModalAsync(page, true);
        }

        internal async Task PopAsync(string title)
        {
            App.ViewModel.Home.PageTitle = title;

            await mainPage.Navigation.PopAsync(true);
        }

        internal async Task PopAsync()
        {
            await mainPage.Navigation.PopAsync(true);
        }

        internal async Task PushAsync(Page page, string title)
        {
            App.ViewModel.Home.PageTitle = title;
            await mainPage.Navigation.PushAsync(page, true);
        }

        internal void NavigateToProcessing(RadSideDrawer drawer)
        {
            App.ViewModel.Home.PageTitle = "Tagging";
            drawer.MainContent = new ProcessingView();
        }

        internal async Task PushAsync(Page page, ImageInformation image)
        {
            App.ViewModel.SelectedImage = image;

            await mainPage.Navigation.PushAsync(page, true);
        }

        internal void ToggleDrawer()
        {
            drawer.IsOpen = !drawer.IsOpen;
        }

        internal async Task PushAsync(Page page)
        {
            await mainPage.Navigation.PushAsync(page, true);
        }

        internal void NavigateToPage(HomeViewModel homeViewModel, PageType pageType)
        {
            if (pageType == PageType.Home && homeViewModel.IsProcessing)
            {
                homeViewModel.PageTitle = AppResources.ProcessingTitle;
                drawer.MainContent = new ProcessingView();
            }
            else
            {
                homeViewModel.PageTitle = pageType.ToString();
                drawer.MainContent = pageType.AsView();
            }

            drawer.IsOpen = false;
        }

        internal void NavigateToPage(HomeViewModel homeViewModel, RadSideDrawer drawer, PageType pageType)
        {
            if (pageType == PageType.Home && homeViewModel.IsProcessing)
            {
                homeViewModel.PageTitle = AppResources.ProcessingTitle;
                drawer.MainContent = new ProcessingView();
            }
            else
            {
                homeViewModel.PageTitle = pageType.ToString();
                drawer.MainContent = pageType.AsView();
            }
        }
    }
}