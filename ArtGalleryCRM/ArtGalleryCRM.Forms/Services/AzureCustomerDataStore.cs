using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArtGalleryCRM.Forms.Models;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Essentials;

namespace ArtGalleryCRM.Forms.Services
{
    public class AzureCustomerDataStore : IDataStore<Customer>
    {
        private readonly IMobileServiceTable<Customer> _customersTable;

        public AzureCustomerDataStore()
        {
            this._customersTable = App.MobileService.GetTable<Customer>();
        }
        
        public async Task<IReadOnlyList<Customer>> GetItemsAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return null;
            }

            try
            {
                return await this._customersTable.ToListAsync();
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return new List<Customer>();
            }
        }

        public async Task<Customer> GetItemAsync(string id)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return null;
            }

            try
            {
                var customers = await this._customersTable.Where(s => s.Id == id).ToListAsync();

                if (customers == null || customers.Count == 0)
                {
                    return null;
                }

                return customers[0];
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return null;
            }
        }

        public async Task<string> GetIdAsync(string name)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return null;
            }

            try
            {
                var customers = await this._customersTable.Where(s => s.Name == name).ToListAsync();

                if (customers == null || customers.Count == 0)
                {
                    return null;
                }

                return customers[0].Id;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return null;
            }
        }

        public async Task<bool> AddItemAsync(Customer customer)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._customersTable.InsertAsync(customer);
                return true;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(Customer customer)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._customersTable.UpdateAsync(customer);
                return true;
            }
            catch(Exception ex)
            {
                await ShowErrorMessageAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(Customer customer)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._customersTable.DeleteAsync(customer);
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