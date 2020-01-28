using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Services;
using ArtGalleryCRM.Forms.Views.ProductPages;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.ProductViewModels
{
    public class ProductsViewModel : PageViewModelBase
    {
        private IReadOnlyList<Product> _allProducts;
        private string _searchText = string.Empty;

        public ProductsViewModel()
        {
            this.Title = "Products";
            this.ItemTapCommand = new Command<ItemTapCommandContext>(this.ItemTapped);
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
        }
        
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                this.ApplyFilter(value);
            }
        }

        public ICommand ItemTapCommand { get; set; }

        public ICommand ToolbarCommand { get; set; }

        private async Task LoadProductsAsync()
        {
            if (this.IsBusy)
            {
                return;
            }
            
            try
            {
                if (this.Products.Count == 0)
                {
                    this.IsBusy = true;
                    this.IsBusyMessage = "loading products...";

                    this._allProducts = await DependencyService.Get<IDataStore<Product>>().GetItemsAsync();
                    
                    foreach (var product in this._allProducts)
                    {
                        this.Products.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading products, check network connection and try again. Details: \r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
            finally
            {
                this.IsBusyMessage = "";
                this.IsBusy = false;
            }
        }

        public override async void OnAppearing()
        {
            await this.LoadProductsAsync();
        }

        private void ApplyFilter(string textToSearch)
        {
            if (this._allProducts == null)
            {
                return;
            }

            var filteredProducts = string.IsNullOrEmpty(textToSearch)
                ? this._allProducts
                : this._allProducts.Where(p => p.Title.ToLower().Contains(textToSearch.ToLower()));

            this.Products.Clear();

            foreach (var product in filteredProducts)
            {
                this.Products.Add(product);
            }
        }

        private async void ItemTapped(ItemTapCommandContext context)
        {
            if (context.Item is Product product)
            {
               await  this.NavigateForwardAsync(new ProductDetailPage(new ProductDetailViewModel(product)));
            }
        }

        private async void ToolbarItemTapped(object obj)
        {
            switch (obj.ToString())
            {
                case "sync":
                    this.Products.Clear();
                    await this.LoadProductsAsync();
                    break;
                case "add":
                    await this.NavigateForwardAsync(new ProductEditPage());
                    break;
            }
        }
    }
}