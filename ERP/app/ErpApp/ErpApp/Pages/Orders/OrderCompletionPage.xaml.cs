using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.Pages.Orders
{
    public partial class OrderCompletionPage : MvxContentPage<ViewModels.OrderCompletionViewModel>, IMvxOverridePresentationAttribute
    {
        public OrderCompletionPage()
        {
            InitializeComponent();

            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.SetBinding(ContentPage.TitleProperty, new Binding("Order.OrderNumber"));
            }
            else
            {
                NavigationPage.SetHasNavigationBar(this, false);
                this.BackgroundColor = Color.FromHex("#f1f3f7");
            }
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return new MvxContentPagePresentationAttribute() { WrapInNavigationPage = true };
        }
    }
}
