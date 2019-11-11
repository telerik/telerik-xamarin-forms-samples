using System.Collections.ObjectModel;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Views;
using ArtGalleryCRM.Forms.Views.CustomerPages;
using ArtGalleryCRM.Forms.Views.EmployeePages;
using ArtGalleryCRM.Forms.Views.OrderPages;
using ArtGalleryCRM.Forms.Views.ProductPages;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels
{
    public class RootMasterViewModel : PageViewModelBase
    {
        public RootMasterViewModel()
        {
            this.MenuItems = new ObservableCollection<NavigationMenuItem>
            {
                new NavigationMenuItem { Id = 0, Title = "Employees", TargetType = typeof(EmployeesPage), IconPath = "ic_employees.png"},
                new NavigationMenuItem { Id = 1, Title = "Customers", TargetType = typeof(CustomersPage), IconPath = "ic_customers.png"},
                new NavigationMenuItem { Id = 2, Title = "Products", TargetType = typeof(ProductsPage), IconPath = "ic_products.png"},
                new NavigationMenuItem { Id = 3, Title = "Orders", TargetType = typeof(OrdersPage), IconPath = "ic_orders.png"},
                new NavigationMenuItem { Id = 4, Title = "Shipping", TargetType = typeof(ShippingPage), IconPath = "ic_shipping.png"},
                new NavigationMenuItem { Id = 5, Title = "Support", TargetType = typeof(SupportPage), IconPath = "ic_help.png"},
                new NavigationMenuItem { Id = 6, Title = "About", TargetType = typeof(AboutPage), IconPath = "ic_about.png"}
            };

            // Set an alternating row background color
            for (var i = 0; i < this.MenuItems.Count - 1; i++)
            {
                this.MenuItems[i].RowBackgroundColor = i % 2 == 0 
                    ? Color.FromHex("#66FFFFFF") 
                    : Color.Transparent;
            }
        }

        public ObservableCollection<NavigationMenuItem> MenuItems { get; }
    }
}