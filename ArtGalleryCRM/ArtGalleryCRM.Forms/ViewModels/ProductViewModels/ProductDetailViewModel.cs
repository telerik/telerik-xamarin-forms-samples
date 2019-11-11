using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Services;
using ArtGalleryCRM.Forms.Views.OrderPages;
using ArtGalleryCRM.Forms.Views.ProductPages;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.ProductViewModels
{
    public class ProductDetailViewModel : PageViewModelBase
    {
        private Product _selectedProduct;

        public ProductDetailViewModel() { }
        
        public ProductDetailViewModel(Product item = null)
        {
            this.Title = item?.Title;
            this.SelectedProduct = item;
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
        }

        public Product SelectedProduct
        {
            get => this._selectedProduct;
            set => SetProperty(ref this._selectedProduct, value);
        }

        public ICommand ToolbarCommand { get; set; }

        public override async void OnAppearing()
        {
            this.IsBusy = true;

            try
            {
                this.IsBusyMessage = "loading product details...";

                this.SelectedProduct = await DependencyService.Get<IDataStore<Product>>().GetItemAsync(this.SelectedProduct.Id);
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem getting the product details, check your network connection and try again. Details: \r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
            finally
            {
                this.IsBusyMessage = "";
                this.IsBusy = false;
            }
        }

        private async void ToolbarItemTapped(object obj)
        {
            if (this.SelectedProduct == null)
            {
                return;
            }

            switch (obj.ToString())
            {
                case "order":
                    await this.NavigateForwardAsync(new OrderEditPage(this.SelectedProduct));
                    break;
                case "edit":
                    await this.NavigateForwardAsync(new ProductEditPage(this.SelectedProduct));
                    break;
                case "delete":
                    await this.DeleteProductAsync();
                    break;
            }
        }

        private async Task DeleteProductAsync()
        {
            await App.DisplayReadOnlyDemoAlert();
            return;

            // ****** Deletion Logic ****** //

            //if (this.SelectedProduct == null)
            //{
            //    return;
            //}

            //var result = await Application.Current.MainPage.DisplayAlert("Delete?", "Do you really want to delete this item?", "Yes", "No");

            //if (!result)
            //{
            //    return;
            //}

            //try
            //{
            //    this.IsBusy = true;

            //    // Do Referential Integrity Check
            //    var orders = await DependencyService.Get<IDataStore<Order>>().GetItemsAsync();

            //    var matchingOrders = orders.Where(o => o.ProductId == this.SelectedProduct.Id).ToList();

            //    if (matchingOrders.Count > 0)
            //    {
            //        var deleteAll = await Application.Current.MainPage.DisplayAlert("!!! WARNING !!!", $"There are orders in the database for {this.SelectedProduct.Title}. If you delete this product, you'll also delete all of the associated orders.\r\n\nDo you wish to delete everything?", "Delete All", "Cancel");

            //        // Back out if user declines to delete associated orders
            //        if (!deleteAll)
            //        {
            //            return;
            //        }

            //        // Delete each associated Order
            //        foreach (var order in matchingOrders)
            //        {
            //            await DependencyService.Get<IDataStore<Order>>().DeleteItemAsync(order);
            //        }
            //    }

            //    // Lastly, delete the product
            //    await DependencyService.Get<IDataStore<Product>>().DeleteItemAsync(this.SelectedProduct);
            //}
            //catch (Exception ex)
            //{
            //    // inform user
            //}
            //finally
            //{
            //    this.IsBusy = false;
            //}

            //await this.NavigateBackAsync();
        }
    }
}