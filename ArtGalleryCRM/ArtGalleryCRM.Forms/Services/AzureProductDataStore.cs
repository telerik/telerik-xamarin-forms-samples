using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArtGalleryCRM.Forms.Models;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Essentials;

namespace ArtGalleryCRM.Forms.Services
{
    public class AzureProductDataStore : IDataStore<Product>
    {
        private readonly IMobileServiceTable<Product> _productsTable;

        public AzureProductDataStore()
        {
            this._productsTable = App.MobileService.GetTable<Product>();
        }
        
        public async Task<IReadOnlyList<Product>> GetItemsAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return null;
            }

            try
            {
                return await this._productsTable.ToListAsync();
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return new List<Product>();
            }
        }

        public async Task<Product> GetItemAsync(string id)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return null;
            }

            try
            {
                var products = await this._productsTable.Where(s => s.Id == id).ToListAsync();

                if (products == null || products.Count == 0)
                {
                    return null;
                }

                return products[0];
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return null;
            }
        }

        public async Task<string> GetIdAsync(string title)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return null;
            }

            try
            {
                var products = await this._productsTable.Where(s => s.Title == title).ToListAsync();

                if (products == null || products.Count == 0)
                {
                    return null;
                }

                return products[0].Id;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return null;
            }
        }

        public async Task<bool> AddItemAsync(Product product)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._productsTable.InsertAsync(product);
                return true;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(Product product)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._productsTable.UpdateAsync(product);
                return true;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(Product product)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._productsTable.DeleteAsync(product);
                return true;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return false;
            }
        }

        public Task ShowErrorMessageAsync(string errorMessage = null)
        {
            return string.IsNullOrEmpty(errorMessage) 
                ? App.RootPage.DisplayAlert("No Internet Connection", "Unable to perform requested action, your device is offline", "OK") 
                : App.RootPage.DisplayAlert("Error", $"There was a problem performing the requested operation. Try again.\r\n{errorMessage}", "OK");
        }
    }
}