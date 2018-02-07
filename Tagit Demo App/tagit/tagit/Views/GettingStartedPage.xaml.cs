using tagit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tagit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GettingStartedPage : ContentPage
    {
        public GettingStartedPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.ViewModel.Settings.IsFirstRun = false;
            StorageHelper.SaveIsFirstRun(false);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Send<object>(this, "IsSetup");
        }
    }
}