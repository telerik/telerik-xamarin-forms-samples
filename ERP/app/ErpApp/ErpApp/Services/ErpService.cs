using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ErpApp.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MvvmCross.Plugin.Messenger;
using Newtonsoft.Json.Linq;

namespace ErpApp.Services
{
    public class ErpService : IErpService
    {
        public ErpService(IMvxMessenger messenger)
        {
            this.messenger = messenger;

            //TODO: revert this code when this:https://github.com/Azure/azure-mobile-apps-net-client/issues/514 issue is fixed
            // old code was: this.client = new MobileServiceClient(Constants.ApplicationURL); 

            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.None;
            }
            this.client = new MobileServiceClient(Constants.ApplicationURL, handler);

            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<Vendor>();
            store.DefineTable<Product>();
            store.DefineTable<Order>();
            store.DefineTable<Customer>();

            this.client.SyncContext.InitializeAsync(store, new ErpMobileServiceSyncHandler());

            this.vendorTable = client.GetSyncTable<Vendor>();
            this.productTable = client.GetSyncTable<Product>();
            this.orderTable = client.GetSyncTable<Order>();
            this.customerTable = client.GetSyncTable<Customer>();
        }

        private MobileServiceClient client;
        private Task<bool> syncTask;
        private IMvxMessenger messenger;
        private IMobileServiceSyncTable<Vendor> vendorTable;
        private IMobileServiceSyncTable<Product> productTable;
        private IMobileServiceSyncTable<Order> orderTable;
        private IMobileServiceSyncTable<Customer> customerTable;

        const string offlineDbPath = @"localstore.db";

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public bool IsOfflineEnabled
        {
            get { return vendorTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<Vendor>; }
        }

        public async Task<ObservableCollection<Customer>> GetCustomersAsync()
        {
            try
            {
                IEnumerable<Customer> items = await customerTable
                    .ToEnumerableAsync();

                return new ObservableCollection<Customer>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<ObservableCollection<Customer>> GetCustomersAsync(string term)
        {
            try
            {
                IEnumerable<Customer> items = await customerTable.CreateQuery()
                    .Where(c => c.Name.Contains(term) || c.CustomerNumber.Contains(term) || c.Email.Contains(term) || c.Phone.Contains(term))
                    .ToEnumerableAsync();

                return new ObservableCollection<Customer>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<Customer> GetCustomerAsync(string id)
        {
            try
            {
                var customer = await customerTable.LookupAsync(id);
                if (customer == null)
                    return null;

                // Expand orders
                customer.Orders = await orderTable.CreateQuery().Where(c => c.CustomerId == customer.Id).ToCollectionAsync();
                if (customer.Orders != null)
                {
                    // Expand Product
                    foreach (var item in customer.Orders.SelectMany(c => c.OrderDetails))
                    {
                        item.Product = await productTable.LookupAsync(item.ProductId);
                    }
                }
                if (customer.Addresses != null)
                {
                    int index = 1;
                    foreach (var addr in customer.Addresses)
                    {
                        addr.Index = index;
                        index++;
                    }
                }
                return customer;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<ObservableCollection<Order>> GetOrdersAsync()
        {
            try
            {
                IEnumerable<Order> items = await orderTable
                    .ToEnumerableAsync();

                return new ObservableCollection<Order>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<ObservableCollection<Order>> GetOrdersAsync(string term)
        {
            try
            {
                var items = (await orderTable.ToEnumerableAsync()).Where(c => c.OrderNumber.Contains(term) ||
                    c.OrderDate.ToString("dd.MM.yyyy").Contains(term) || c.DueDate.ToString("dd.MM.yyyy").Contains(term)).ToList();

                foreach (var order in items)
                {
                    // Expand Product
                    foreach (var orderDetail in order.OrderDetails)
                    {
                        orderDetail.Product = await productTable.LookupAsync(orderDetail.ProductId);
                    }
                }

                return new ObservableCollection<Order>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            try
            {
                Order order = await orderTable.LookupAsync(id);

                // Expand Product
                foreach (var orderDetail in order.OrderDetails)
                {
                    orderDetail.Product = await productTable.LookupAsync(orderDetail.ProductId);
                }

                // Expand Customer
                order.Customer = await customerTable.LookupAsync(order.CustomerId);
                order.ShippingAddress = order?.Customer?.Addresses?.SingleOrDefault(c => c.Id == order.ShippingAddressId);

                return order;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<ObservableCollection<Models.Order>> GetTodayOrdersAsync()
        {
            try
            {
                IEnumerable<Order> items = await orderTable
                    .Where(c => c.OrderDate.Year == 2018 && c.OrderDate.Month == 12 && c.OrderDate.Day == 12)
                    .ToEnumerableAsync();

                return new ObservableCollection<Order>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<Product> GetProductAsync(string id)
        {
            try
            {
                Product product = await productTable.LookupAsync(id);
                product.Prepare();
                return product;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<ObservableCollection<Product>> GetProductsAsync()
        {
            try
            {
                IEnumerable<Product> items = (await productTable
                    .ToEnumerableAsync()).ToList();

                foreach (var item in items)
                {
                    item.Prepare();
                }

                return new ObservableCollection<Product>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<ObservableCollection<Product>> GetProductsAsync(string term)
        {
            try
            {
                IEnumerable<Product> items = (await productTable.CreateQuery()
                    .Where(c => c.Name.Contains(term) || c.ProductNumber.Contains(term))
                    .ToEnumerableAsync()).ToList();

                foreach (var item in items)
                {
                    item.Prepare();
                }

                return new ObservableCollection<Product>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<Vendor> GetVendorAsync(string id)
        {
            try
            {
                Vendor vendor = await vendorTable.LookupAsync(id);
                return vendor;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<ObservableCollection<Vendor>> GetVendorsAsync()
        {
            try
            {
                IEnumerable<Vendor> items = await vendorTable
                    .ToEnumerableAsync();

                return new ObservableCollection<Vendor>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<ObservableCollection<Vendor>> GetVendorsAsync(string term)
        {
            try
            {
                IEnumerable<Vendor> items = await vendorTable
                    .Where(c => c.Name.Contains(term) || c.VendorNumber.Contains(term))
                    .ToEnumerableAsync();

                return new ObservableCollection<Vendor>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<Models.Customer> SaveCustomerAsync(Models.Customer item)
        {
            try
            {
                if (item.Id == null)
                {
                    if ((await this.customerTable.ToEnumerableAsync()).Any())
                    {
                        int lastCustomerNumber = (await this.customerTable.Select(c => int.Parse(c.CustomerNumber.TrimStart('C'))).ToListAsync()).Max();
                        item.CustomerNumber = $"C{lastCustomerNumber + 1}";
                    }
                    else
                    {
                        item.CustomerNumber = "C000001";
                    }
                    await customerTable.InsertAsync(item);
                }
                else
                {
                    await customerTable.UpdateAsync(item);
                }

                if (item.Addresses != null && item.Addresses.Count > 0)
                {
                    foreach (var addr in item.Addresses)
                    {
                        if (string.IsNullOrEmpty(addr.Id))
                            addr.Id = Guid.NewGuid().ToString("N");
                        addr.CustomerID = item.Id;
                    }
                    await customerTable.UpdateAsync(item);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Save error: {0}", new[] { e.Message });
                return null;
            }

            this.messenger.Publish<CustomerUpdatedMessage>(new CustomerUpdatedMessage(this, item));
            return item;
        }

        public async Task RemoveCustomerAsync(Models.Customer item)
        {
            try
            {
                await customerTable.DeleteAsync(item);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Remove error: {0}", new[] { e.Message });
                return;
            }

            this.messenger.Publish<CustomerDeletedMessage>(new CustomerDeletedMessage(this, item));
        }

        public async Task<Models.Order> SaveOrderAsync(Models.Order item)
        {
            if (item.Customer != null)
                item.CustomerId = item.Customer.Id;
            if (item.ShippingAddress != null)
                item.ShippingAddressId = item.ShippingAddress.Id;

            try
            {
                if (item.Id == null)
                {
                    if ((await this.orderTable.ToEnumerableAsync()).Any())
                    {
                        int lastOrderNumber = (await this.orderTable.Select(c => int.Parse(c.OrderNumber.TrimStart('O'))).ToListAsync()).Max();
                        item.OrderNumber = $"O{lastOrderNumber + 1}";
                    }
                    else
                    {
                        item.OrderNumber = "O000001";
                    }

                    await orderTable.InsertAsync(item);
                }
                else
                {
                    await orderTable.UpdateAsync(item);
                }

                if (item.OrderDetails != null && item.OrderDetails.Count > 0)
                {
                    foreach (var detail in item.OrderDetails)
                    {
                        if (string.IsNullOrEmpty(detail.Id))
                            detail.Id = Guid.NewGuid().ToString("N");
                        detail.OrderId = item.Id;
                    }
                    await orderTable.UpdateAsync(item);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Save error: {0}", new[] { e.Message });
                return null;
            }

            if (item.ShippingAddress == null)
            {
                item.ShippingAddress = item.Customer?.Addresses.SingleOrDefault(c => c.Id == item.ShippingAddressId);
            }

            this.messenger.Publish<OrderUpdatedMessage>(new OrderUpdatedMessage(this, item));
            return item;
        }

        public async Task RemoveOrderAsync(Models.Order item)
        {
            try
            {
                await orderTable.DeleteAsync(item);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Remove error: {0}", new[] { e.Message });
                return;
            }

            this.messenger.Publish<OrderDeletedMessage>(new OrderDeletedMessage(this, item));
        }

        public async Task<Models.Product> SaveProductAsync(Models.Product item)
        {
            try
            {
                if (item.Id == null)
                {
                    if ((await this.productTable.ToEnumerableAsync()).Any())
                    {
                        int lastProductNumber = (await this.productTable.Select(c => int.Parse(c.ProductNumber.TrimStart('P'))).ToListAsync()).Max();
                        item.ProductNumber = $"P{lastProductNumber + 1}";
                    }
                    else
                    {
                        item.ProductNumber = "P0001";
                    }
                    await productTable.InsertAsync(item);
                }
                else
                {
                    await productTable.UpdateAsync(item);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Save error: {0}", new[] { e.Message });
                return null;
            }

            this.messenger.Publish<ProductUpdatedMessage>(new ProductUpdatedMessage(this, item));
            return item;
        }

        public async Task RemoveProductAsync(Models.Product item)
        {
            try
            {
                await productTable.DeleteAsync(item);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Remove error: {0}", new[] { e.Message });
                return;
            }

            this.messenger.Publish<ProductDeletedMessage>(new ProductDeletedMessage(this, item));
        }

        public async Task<Models.Vendor> SaveVendorAsync(Models.Vendor item)
        {
            try
            {
                if (item.Id == null)
                {
                    if ((await this.vendorTable.ToEnumerableAsync()).Any())
                    {
                        int lastVendorNumber = (await this.vendorTable.Select(c => int.Parse(c.VendorNumber.TrimStart('V'))).ToListAsync()).Max();
                        item.VendorNumber = $"V{lastVendorNumber + 1}";
                    }
                    else
                    {
                        item.VendorNumber = "V000001";
                    }
                    await vendorTable.InsertAsync(item);
                }
                else
                {
                    await vendorTable.UpdateAsync(item);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Save error: {0}", new[] { e.Message });
                return null;
            }

            this.messenger.Publish<VendorUpdatedMessage>(new VendorUpdatedMessage(this, item));
            return item;
        }

        public async Task RemoveVendorAsync(Models.Vendor item)
        {
            try
            {
                await vendorTable.DeleteAsync(item);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Remove error: {0}", new[] { e.Message });
                return;
            }

            this.messenger.Publish<VendorDeletedMessage>(new VendorDeletedMessage(this, item));
        }

        public Task<bool> SyncIfNeededAsync()
        {
            if (syncTask != null)
                return syncTask;

            syncTask = this.Sync();
            return syncTask;
        }

        public async Task<bool> Sync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.vendorTable.PullAsync("allVendors", this.vendorTable.CreateQuery());
                await this.productTable.PullAsync("allProducts", this.productTable.CreateQuery());
                await this.orderTable.PullAsync("allOrders", this.orderTable.CreateQuery());
                await this.customerTable.PullAsync("allCustomers", this.customerTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
                return false;
            }

            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }

            return syncErrors == null;
        }

        public async Task<bool> RefreshCustomers()
        {
            try
            {
                await this.customerTable.PurgeAsync("allCustomers", this.customerTable.CreateQuery(), true, System.Threading.CancellationToken.None);
                await this.customerTable.PullAsync("allCustomers", this.customerTable.CreateQuery());
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
                return false;
            }
        }

        public async Task<bool> RefreshOrders()
        {
            try
            {
                await this.orderTable.PurgeAsync("allOrders", this.orderTable.CreateQuery(), true, System.Threading.CancellationToken.None);
                await this.orderTable.PullAsync("allOrders", this.orderTable.CreateQuery());
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
                return false;
            }
        }
    }

    public class ErpMobileServiceSyncHandler : IMobileServiceSyncHandler
    {
        public Task<JObject> ExecuteTableOperationAsync(IMobileServiceTableOperation operation)
        {
            return Task.FromResult(operation.Item);
        }

        public Task OnPushCompleteAsync(MobileServicePushCompletionResult result)
        {
            return Task.CompletedTask;
        }
    }

    public class CustomerUpdatedMessage : MvxMessage
    {
        public CustomerUpdatedMessage(object sender, Models.Customer entity)
            : base(sender)
        {
            Customer = entity;
        }

        public Models.Customer Customer { get; }
    }

    public class CustomerDeletedMessage : MvxMessage
    {
        public CustomerDeletedMessage(object sender, Models.Customer entity)
            : base(sender)
        {
            Customer = entity;
        }

        public Models.Customer Customer { get; }
    }

    public class OrderUpdatedMessage : MvxMessage
    {
        public OrderUpdatedMessage(object sender, Models.Order entity)
            : base(sender)
        {
            Order = entity;
        }

        public Models.Order Order { get; }
    }

    public class OrderDeletedMessage : MvxMessage
    {
        public OrderDeletedMessage(object sender, Models.Order entity)
            : base(sender)
        {
            Order = entity;
        }

        public Models.Order Order { get; }
    }

    public class ProductUpdatedMessage : MvxMessage
    {
        public ProductUpdatedMessage(object sender, Models.Product entity)
            : base(sender)
        {
            Product = entity;
        }

        public Models.Product Product { get; }
    }

    public class ProductDeletedMessage : MvxMessage
    {
        public ProductDeletedMessage(object sender, Models.Product entity)
            : base(sender)
        {
            Product = entity;
        }

        public Models.Product Product { get; }
    }

    public class VendorUpdatedMessage : MvxMessage
    {
        public VendorUpdatedMessage(object sender, Models.Vendor entity)
            : base(sender)
        {
            Vendor = entity;
        }

        public Models.Vendor Vendor { get; }
    }

    public class VendorDeletedMessage : MvxMessage
    {
        public VendorDeletedMessage(object sender, Models.Vendor entity)
            : base(sender)
        {
            Vendor = entity;
        }

        public Models.Vendor Vendor { get; }
    }
}