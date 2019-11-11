using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tagit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesView : ContentView
    {
        public FavoritesView()
        {
            InitializeComponent();

            BindingContext = App.ViewModel.Favorites;
        }
    }
}