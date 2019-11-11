using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace ErpApp.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class SearchResultsPage : MvxContentPage<ViewModels.SearchResultsViewModel>
    {
        public SearchResultsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(100);
            searchBox.Focus();
        }
    }
}
