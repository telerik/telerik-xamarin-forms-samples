using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArtGalleryCRM.Forms.Models;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Essentials;

namespace ArtGalleryCRM.Forms.Services
{
    public class AzureEmployeeDataStore : IDataStore<Employee>
    {
        private readonly IMobileServiceTable<Employee> _employeesTable;

        public AzureEmployeeDataStore()
        {
            this._employeesTable = App.MobileService.GetTable<Employee>();
        }
        
        public async Task<IReadOnlyList<Employee>> GetItemsAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return null;
            }

            try
            {
                return await this._employeesTable.ToListAsync();
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return new List<Employee>();
            }
        }

        public async Task<Employee> GetItemAsync(string id)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return null;
            }

            try
            {
                var employees = await this._employeesTable.Where(s => s.Id == id).ToListAsync();

                if (employees == null || employees.Count == 0)
                {
                    return null;
                }

                return employees[0];
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
                var employees = await this._employeesTable.Where(s => s.Name == name).ToListAsync();

                if (employees == null || employees.Count == 0)
                {
                    return null;
                }

                return employees[0].Id;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return null;
            }
        }

        public async Task<bool> AddItemAsync(Employee employee)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._employeesTable.InsertAsync(employee);
                return true;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(Employee employee)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._employeesTable.UpdateAsync(employee);
                return true;
            }
            catch(Exception ex)
            {
                await this.ShowErrorMessageAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(Employee employee)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await this.ShowErrorMessageAsync();
                return false;
            }

            try
            {
                await this._employeesTable.DeleteAsync(employee);
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
