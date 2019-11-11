using ErpApp.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.Pages
{
    public class OrdersPage : MvxMasterDetailPage<OrderRootViewModel>, IMvxOverridePresentationAttribute
    {
        public OrdersPage()
        {
            this.MasterBehavior = MasterBehavior.Split;
            this.Title = "Orders";
            this.IconImageSource = new FileImageSource() { File = "Orders" };
            NavigationPage.SetHasNavigationBar(this, false);

            this.Master = new Orders.OrderListPage();
            this.Detail = new Orders.OrderDetailPage();
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return new MvxTabbedPagePresentationAttribute(TabbedPosition.Tab) { WrapInNavigationPage = true };
        }
    }
}
