using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tagit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = App.ViewModel.SelectedImage;
        }
        
    }
}