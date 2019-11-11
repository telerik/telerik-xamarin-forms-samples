using System;
using System.Collections.Specialized;
using tagit.Common;
using tagit.Localization;
using tagit.Models;
using tagit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tagit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            this.BindingContext = App.ViewModel.Home;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitializeContent();

            InitializeMessageListeners();

            ToggleSearchMenu();
        }

        private void InitializeContent()
        {
            App.NavigationService.Initialize(drawer);

            if (App.ViewModel.Processing.HomeTabs.ProcessingTab.Images.Count > 0)
            {
                //User has selected picker images, so display the
                //image processing view and then
                //start analyzing and tagging images
                if (!App.ViewModel.Home.IsProcessing) App.ViewModel.Processing.StartProcessingImages();

                App.NavigationService.NavigateToProcessing(drawer);
            }

            if (App.ViewModel.Settings.IsFirstRun)
            {
                this.ToolbarItems.RemoveAt(0);
                App.ViewModel.Home.ShowFirstRun();
            }
        }

        private void InitializeMessageListeners()
        {
            //Listen to see when tags are available to
            //only show Search menu when image tags have been populated
            MessagingCenter.Subscribe<object>(this, "TagsAvailable", sender => { ToggleSearchMenu(); });
            MessagingCenter.Subscribe<object>(this, "IsSetup", sender => { EnableMainMenu(); });
            MessagingCenter.Subscribe<object>(this, "NoServiceConnection", sender => { NoServiceConnection(); });
        }

        private void NoServiceConnection()
        {
            DisplayAlert("Error", "There is no connection to the computer vision service.", "OK");
        }

        //Add or remove the Search menu based
        //on image tag availability
        private void ToggleSearchMenu()
        {
            (this.BindingContext as HomeViewModel)?.ToggleSearchMenu(this.ToolbarItems, App.ViewModel.AllTags.Count > 0);
        }

        //Add or remove the Search menu based
        //on image tag availability
        private void EnableMainMenu()
        {
            (this.BindingContext as HomeViewModel)?.EnableMainMenu(this.ToolbarItems);
        }

    }
}