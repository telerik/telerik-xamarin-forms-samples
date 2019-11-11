using ErpApp.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.Pages
{
    public class CustomersPage : MvxMasterDetailPage<CustomersRootViewModel>, IMvxOverridePresentationAttribute
    {
        public CustomersPage()
        {
            this.MasterBehavior = MasterBehavior.Split;
            this.Title = "Customers";
            this.IconImageSource = new FileImageSource() { File = "Users" };
            NavigationPage.SetHasNavigationBar(this, false);

            this.Master = new Customers.CustomersListPage();
            this.Detail = new Customers.CustomerDetailPage();
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return new MvxTabbedPagePresentationAttribute(TabbedPosition.Tab) { WrapInNavigationPage = true };
        }
    }
}
