using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Telerik.XamarinForms.DataControls;
using Xamarin.Forms;

namespace ErpApp.Pages.Customers
{
    public partial class CustomersListPage : MvxContentPage<ViewModels.CustomersListViewModel>, IMvxOverridePresentationAttribute
    {
        public CustomersListPage()
        {
            InitializeComponent();

            if (Device.Idiom != TargetIdiom.Phone)
            {
                var service = MvvmCross.Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                var vm = service.LoadViewModel(new MvxViewModelInstanceRequest(typeof(ViewModels.CustomersListViewModel)), null);
                ViewModel = vm as ViewModels.CustomersListViewModel;

                NavigationPage.SetHasNavigationBar(this, false);
                Grid.SetRowSpan(this.busyIndicator, 2);
                BackgroundColor = Color.FromHex("#f1f3f7");
            }
            else
            {
                this.IconImageSource = new FileImageSource() { File = "Users" };
                this.LayoutRoot.Children.Remove(this.searchBar);
                this.LayoutRoot.RowDefinitions.Clear();
                this.ContentRoot.ClearValue(Grid.RowProperty);

                var searchToolbarItem = new ToolbarItem();
                searchToolbarItem.Text = "Search";
                searchToolbarItem.IconImageSource = new FileImageSource() { File = "search" };
                searchToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("SearchCommand"));

                var aboutItem = new ToolbarItem();
                aboutItem.Text = "About";
                aboutItem.IconImageSource = new FileImageSource() { File = "About" };
                aboutItem.SetBinding(ToolbarItem.CommandProperty, new Binding("AboutCommand"));

                this.ToolbarItems.Add(searchToolbarItem);
                this.ToolbarItems.Add(aboutItem);
            }
        }

        private async void Handle_RefreshRequested(object sender, Telerik.XamarinForms.DataControls.ListView.PullToRefreshRequestedEventArgs e)
        {
            await ViewModel.Refresh();
            (sender as RadListView).IsPullToRefreshActive = false;
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                return new MvxTabbedPagePresentationAttribute(TabbedPosition.Tab) { WrapInNavigationPage = true };
            }
            else
            {
                return new MvxMasterDetailPagePresentationAttribute(MasterDetailPosition.Master) { WrapInNavigationPage = false };
            }
        }
    }
}
