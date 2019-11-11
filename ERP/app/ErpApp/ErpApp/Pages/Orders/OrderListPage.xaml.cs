using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Telerik.XamarinForms.DataControls;
using Xamarin.Forms;

namespace ErpApp.Pages.Orders
{
    public partial class OrderListPage : MvxContentPage<ViewModels.OrderListViewModel>, IMvxOverridePresentationAttribute
    {
        public OrderListPage()
        {
            InitializeComponent();

            if (Device.Idiom != TargetIdiom.Phone)
            {
                var service = MvvmCross.Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                var vm = service.LoadViewModel(new MvxViewModelInstanceRequest(typeof(ViewModels.OrderListViewModel)), null);
                ViewModel = vm as ViewModels.OrderListViewModel;

                NavigationPage.SetHasNavigationBar(this, false);
                BackgroundColor = Color.FromHex("#f1f3f7");
                Grid.SetRowSpan(this.busyIndicator, 2);
            }
            else
            {
                this.IconImageSource = new FileImageSource() { File = "Orders" };

                var searchToolbarItem = new ToolbarItem();
                searchToolbarItem.Text = "Search";
                searchToolbarItem.IconImageSource = new FileImageSource() { File = "search" };
                searchToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("SearchCommand"));
                this.ToolbarItems.Add(searchToolbarItem);

                ToolbarItem item = new ToolbarItem();
                item.Text = "Schedule";
                item.IconImageSource = new FileImageSource() { File = "Schedule" };
                item.SetBinding(ToolbarItem.CommandProperty, new Binding("ShowOrderScheduleCommand"));
                this.ToolbarItems.Add(item);

                var aboutItem = new ToolbarItem();
                aboutItem.Text = "About";
                aboutItem.IconImageSource = new FileImageSource() { File = "About" };
                aboutItem.SetBinding(ToolbarItem.CommandProperty, new Binding("AboutCommand"));
                this.ToolbarItems.Add(aboutItem);

                this.LayoutRoot.Children.Remove(this.searchBar);
                this.LayoutRoot.RowDefinitions.Clear();
                this.ContentRoot.ClearValue(Grid.RowProperty);
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
