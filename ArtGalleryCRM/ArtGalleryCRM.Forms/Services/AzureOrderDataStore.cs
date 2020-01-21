using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArtGalleryCRM.Forms.Models;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Essentials;

namespace ArtGalleryCRM.Forms.Services
{
    public class AzureOrderDataStore : IDataStore<Order>
    {
        private readonly IMobileServiceTable<Order> _ordersTable;

        public AzureOrderDataStore()
        {
            this._ordersTable = App.MobileService.GetTable<Order>();
        }
        
        public async Task<IReadOnlyList<Order>> GetItemsAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return new List<Order>();
            }
            
            try
            {
                return await this._ordersTable.ToListAsync();
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return new List<Order>();
            }
        }

        public async Task<Order> GetItemAsync(string id)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return null;
            }

            try
            {
                var orders = await this._ordersTable.Where(s => s.Id == id).ToListAsync();

                if (orders == null || orders.Count == 0)
                {
                    return null;
                }

                return orders[0];
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return null;
            }
        }

        public async Task<string> GetIdAsync(string notes)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return null;
            }

            try
            {
                var orders = await this._ordersTable.Where(s => s.Notes == notes).ToListAsync();

                if (orders == null || orders.Count == 0)
                {
                    return null;
                }

                return orders[0].Id;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return null;
            }
        }

        public async Task<bool> AddItemAsync(Order order)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._ordersTable.InsertAsync(order);

                return true;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(Order order)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._ordersTable.UpdateAsync(order);
                return true;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(Order order)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._ordersTable.DeleteAsync(order);
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