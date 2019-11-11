using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ErpApp.Services
{
    public interface IErpService
    {
        Task<bool> Sync();
        Task<bool> SyncIfNeededAsync();
        Task<ObservableCollection<Models.Customer>> GetCustomersAsync();
        Task<ObservableCollection<Models.Customer>> GetCustomersAsync(string term);
        Task<Models.Customer> GetCustomerAsync(string id);
        Task<ObservableCollection<Models.Order>> GetOrdersAsync();
        Task<ObservableCollection<Models.Order>> GetOrdersAsync(string term);
        Task<ObservableCollection<Models.Order>> GetTodayOrdersAsync();
        Task<Models.Order> GetOrderAsync(string id);
        Task<ObservableCollection<Models.Vendor>> GetVendorsAsync();
        Task<ObservableCollection<Models.Vendor>> GetVendorsAsync(string term);
        Task<Models.Vendor> GetVendorAsync(string id);
        Task<ObservableCollection<Models.Product>> GetProductsAsync();
        Task<ObservableCollection<Models.Product>> GetProductsAsync(string term);
        Task<Models.Product> GetProductAsync(string id);

        Task<Models.Customer> SaveCustomerAsync(Models.Customer customer);
        Task RemoveCustomerAsync(Models.Customer customer);
        Task<Models.Order> SaveOrderAsync(Models.Order order);
        Task RemoveOrderAsync(Models.Order order);
        Task<Models.Product> SaveProductAsync(Models.Product product);
        Task RemoveProductAsync(Models.Product product);
        Task<Models.Vendor> SaveVendorAsync(Models.Vendor vendor);
        Task RemoveVendorAsync(Models.Vendor vendor);

        Task<bool> RefreshCustomers();
        Task<bool> RefreshOrders();
    }
}
