using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tagit.Helpers;
using tagit.ViewModels;
using Xamarin.Forms;

namespace tagit
{
    public partial class App : Application
    {
        internal static MainViewModel ViewModel { get; set; }
        internal static NavigationService NavigationService { get; set; }

        public App()
        {
            InitializeComponent();
            //DEGUG TO CLEAR OUT STATE
            //Helpers.StorageHelper.SaveIsFirstRun(true);
            //Helpers.StorageHelper.SaveFavoritesAsync(new List<Models.ImageInformation>());
            //Helpers.StorageHelper.SaveTaggedImagesAsync(new List<Models.ImageInformation>());

            ////Instantiate the app view model
            ViewModel = new MainViewModel();

 
            //Easy implementation of navigation bar styling
            MainPage = new NavigationPage(new Views.HomePage())
            {
                BarBackgroundColor = (Color)Resources["AppAccentColor"],
                BarTextColor = Color.White,
            };

            NavigationService = new NavigationService(MainPage);
        }

        protected override void OnStart()
        {
             
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
