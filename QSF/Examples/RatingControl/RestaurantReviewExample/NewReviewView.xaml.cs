using System;
using QSF.Services;
using Xamarin.Forms;

namespace QSF.Examples.RatingControl.RestaurantReviewExample
{
    public partial class NewReviewView : ContentPage
    {
        public NewReviewView()
        {
            this.InitializeComponent();
        }

        private void OnTapped(object sender, EventArgs eventArgs)
        {
            var navigationService = DependencyService.Get<INavigationService>();

            navigationService.NavigateBackAsync();
        }
    }
}