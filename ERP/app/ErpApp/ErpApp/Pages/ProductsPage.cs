using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.Pages
{
    public class ProductsPage : MvxContentPage<ViewModels.ProductsViewModel>, IMvxOverridePresentationAttribute
    {
		public ProductsPage()
		{
            Title = "Products";
            this.IconImageSource = new FileImageSource() { File = "Products" };
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

            if (Device.Idiom == TargetIdiom.Phone)
            {
                var content = new Products.ProductsPhoneView();
                this.Content = content;

                var searchToolbarItem = new ToolbarItem();
                searchToolbarItem.Text = "Search";
                searchToolbarItem.IconImageSource = new FileImageSource() { File = "search" };
                searchToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("SearchCommand"));
                this.ToolbarItems.Add(searchToolbarItem);

                var aboutItem = new ToolbarItem();
                aboutItem.Text = "About";
                aboutItem.IconImageSource = new FileImageSource() { File = "About" };
                aboutItem.SetBinding(ToolbarItem.CommandProperty, new Binding("AboutCommand"));
                this.ToolbarItems.Add(aboutItem);

                ToolbarItem item = new ToolbarItem();
                item.Text = "Layout";
                item.IconImageSource = new FileImageSource() { File = "ellipsis" };
                item.Clicked += (s, e) => content.OpenPopup();
                this.ToolbarItems.Add(item);
            }
            else
            {
                Content = new Products.ProductsTabletView();
                BackgroundColor = Color.FromHex("#f1f3f7");
                NavigationPage.SetHasNavigationBar(this, false);
            }
		}

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return new MvxTabbedPagePresentationAttribute(TabbedPosition.Tab) { WrapInNavigationPage = true };
        }
    }
}