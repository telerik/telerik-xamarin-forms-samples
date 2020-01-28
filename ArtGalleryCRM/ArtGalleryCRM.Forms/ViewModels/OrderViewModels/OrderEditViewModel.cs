using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Services;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.OrderViewModels
{
    public class OrderEditViewModel : PageViewModelBase
    {
        private bool _isNewOrder = true;
        private bool _isProductPreviewVisible;
        private Order _selectedOrder;
        private Product _selectedProduct;
        private Customer _selectedCustomer;
        private Employee _selectedEmployee;
        private string _selectedDeliveryService;

        public OrderEditViewModel()
        {
            this.Title = "Edit Order";
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
            this.QuantityChangedCommand = new Command(this.QuantityChanged);
            this.ProductImageTappedCommand = new Command(this.ProductImageTapped);
        }

        #region Properties

        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();

        public ObservableCollection<Customer> Customers { get; } = new ObservableCollection<Customer>();

        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();

        public ObservableCollection<string> DeliveryServices { get; } = new ObservableCollection<string>();

        public bool IsReadyForSave { get; set; }

        public bool IsNewOrder
        {
            get => this._isNewOrder;
            set => SetProperty(ref this._isNewOrder, value);
        }

        public bool IsProductPreviewVisible
        {
            get => this._isProductPreviewVisible;
            set => SetProperty(ref this._isProductPreviewVisible, value);
        }

        public Order SelectedOrder
        {
            get => this._selectedOrder;
            set => SetProperty(ref this._selectedOrder, value);
        }

        public Product SelectedProduct
        {
            get => this._selectedProduct;
            set
            {
                SetProperty(ref this._selectedProduct, value);

                if (value != null)
                {
                    this.SelectedOrder.ProductId = value?.Id;
                }
            }
        }

        public Customer SelectedCustomer
        {
            get => this._selectedCustomer;
            set
            {
                SetProperty(ref this._selectedCustomer, value);

                if (value != null)
                {
                    this.SelectedOrder.CustomerId = value?.Id;
                }
            }
        }

        public Employee SelectedEmployee
        {
            get => this._selectedEmployee;
            set
            {
                SetProperty(ref this._selectedEmployee, value);

                if (value != null)
                {
                    this.SelectedOrder.EmployeeId = value?.Id;
                }
            }
        }

        public string SelectedDeliveryService
        {
            get => this._selectedDeliveryService;
            set
            {
                SetProperty(ref this._selectedDeliveryService, value);

                if (value != null)
                {
                    this.SelectedOrder.DeliveryService = value;
                }
            }
        }

        public ICommand ToolbarCommand { get; set; }

        public ICommand QuantityChangedCommand { get; set; }

        public ICommand ProductImageTappedCommand { get; set; }

        public IDataFormView DataFormView { get; set; }

        #endregion

        #region Interaction Methods

        private async void ToolbarItemTapped(object obj)
        {
            switch (obj.ToString())
            {
                case "save":
                    if (this.SelectedProduct.IsDiscontinued)
                    {
                        await Application.Current.MainPage.DisplayAlert("Discontinued", "This product has been discontinued, choose a different product.", "OK");
                        return;
                    }

                    // Validate the editors
                    this.DataFormView.CommitChanges();

                    // If validation was successful we can now save
                    if (this.IsReadyForSave)
                    {
                        if (await this.UpdateDatabaseAsync())
                        {
                            await this.NavigateBackAsync();
                        }
                    }
                    else
                    {
                        // If validation failed, the different entries will have an error message in red
                        await Application.Current.MainPage.DisplayAlert("Enter Correct Address", "One or more of the address fields are not valid. Check and try again.", "OK");
                    }
                    break;
            }
        }

        private async void QuantityChanged(object obj)
        {
            if(obj is Telerik.XamarinForms.Input.NumericInput.ValueChangedEventArgs e)
            {
                try
                {
                    var quantity = Convert.ToInt32(e.NewValue);

                    // In order to calculate total price, a product needs to be selected.
                    if (this.SelectedProduct == null && this.IsNewOrder && quantity > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Select Product", "You need to select a product before entering a quantity.", "OK");
                        return;
                    }

                    // Fallback for no product selection
                    if (this.SelectedProduct == null)
                    {
                        return;
                    }

                    // Make sure there are enough items in inventory for the order
                    if (this.SelectedOrder.Quantity > this.SelectedProduct.InventoryCount)
                    {
                        await Application.Current.MainPage.DisplayAlert("Lower Quantity", $"There are only {this.SelectedProduct.InventoryCount} in stock, choose a lower value and try again.", "ok");
                        return;
                    }

                    // Calculate total price
                    this.SelectedOrder.TotalPrice = this.SelectedProduct.Price * this.SelectedOrder.Quantity;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void ProductImageTapped()
        {
            this.IsProductPreviewVisible = !this.IsProductPreviewVisible;
        }

        #endregion

        #region Data Methods

        public override async void OnAppearing()
        {
            this.IsBusy = true;

            this.IsBusyMessage = "loading products...";
            await this.LoadProductDataAsync();

            this.IsBusyMessage = "loading customers...";
            await this.LoadCustomerDataAsync();

            this.IsBusyMessage = "loading employees...";
            await this.LoadEmployeeDataAsync();

            this.IsBusyMessage = "loading shippers...";
            await this.LoadShippingDataAsync();

            this.IsBusy = false;
        }

        public async Task<bool> UpdateDatabaseAsync()
        {
            await App.DisplayReadOnlyDemoAlert();
            return false;

            // ****** Update Record Logic ****** //

            //try
            //{
            //    this.IsBusy = true;

            //    // Make sure ids are set
            //    if (string.IsNullOrEmpty(this.SelectedOrder.ProductId))
            //        this.SelectedOrder.ProductId = SelectedProduct.Id;

            //    if (string.IsNullOrEmpty(this.SelectedOrder.EmployeeId))
            //        this.SelectedOrder.EmployeeId = SelectedEmployee.Id;

            //    if (string.IsNullOrEmpty(this.SelectedOrder.CustomerId))
            //        this.SelectedOrder.CustomerId = SelectedCustomer.Id;

            //    SelectedOrder.DeliveryService = SelectedDeliveryService;

            //    if (this.IsNewOrder)
            //    {
            //        await DependencyService.Get<IDataStore<Order>>().AddItemAsync(this.SelectedOrder);
            //    }
            //    else
            //    {
            //        await DependencyService.Get<IDataStore<Order>>().UpdateItemAsync(this.SelectedOrder);
            //    }

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    MessagingCenter.Send(new MessagingCenterAlert
            //    {
            //        Title = "Error",
            //        Message = $"There was a problem updating the database. Details:\r\n\n{ex.Message}",
            //        Cancel = "OK"
            //    }, "Alert");

            //    return false;
            //}
            //finally
            //{
            //    this.IsBusy = false;
            //}
        }
        
        private async Task LoadProductDataAsync()
        {
            try
            {
                var products = await DependencyService.Get<IDataStore<Product>>().GetItemsAsync();

                foreach (var product in products)
                {
                    this.Products.Add(product);
                }
                
                if (string.IsNullOrEmpty(this.SelectedOrder?.ProductId))
                {
                    this.SelectedProduct = this.Products?.FirstOrDefault();
                }
                else
                {
                    var matchingProduct = this.Products?.FirstOrDefault(p => p.Id == this.SelectedOrder?.ProductId);

                    this.SelectedProduct = matchingProduct ?? this.Products?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading products. Details:\r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
        }

        private async Task LoadCustomerDataAsync()
        {
            try
            {
                var customers = await DependencyService.Get<IDataStore<Customer>>().GetItemsAsync();

                foreach (var customer in customers)
                {
                    this.Customers.Add(customer);
                }
            
                if (string.IsNullOrEmpty(this.SelectedOrder?.CustomerId))
                {
                    this.SelectedCustomer = this.Customers?.FirstOrDefault();
                }
                else
                {
                    var matchingCustomer = this.Customers?.FirstOrDefault(c => c.Id == this.SelectedOrder?.CustomerId);

                    this.SelectedCustomer = matchingCustomer ?? this.Customers?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading customers. Details:\r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
        }

        private async Task LoadEmployeeDataAsync()
        {
            try
            {
                var employees = await DependencyService.Get<IDataStore<Employee>>().GetItemsAsync();

                foreach (var employee in employees)
                {
                    this.Employees.Add(employee);
                }
            
                if (string.IsNullOrEmpty(this.SelectedOrder?.EmployeeId))
                {
                    this.SelectedEmployee = this.Employees?.FirstOrDefault();
                }
                else
                {
                    var matchingEmployee = this.Employees?.FirstOrDefault(e => e.Id == this.SelectedOrder?.EmployeeId);

                    this.SelectedEmployee = matchingEmployee ?? this.Employees?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading employees. Details:\r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
        }
        
        private Task LoadShippingDataAsync()
        {
            try
            {
                var deliveryServices = new[] {"UPS", "FedEx", "USPS", "DHL", "OnTrac", "YRC Freight", "Bolt Express", "Transit Systems"};

                foreach (var deliveryService in deliveryServices)
                {
                    this.DeliveryServices.Add(deliveryService);
                }
                
                if (string.IsNullOrEmpty(this.SelectedOrder?.DeliveryService))
                {
                    this.SelectedDeliveryService = this.DeliveryServices?.FirstOrDefault();
                }
                else
                {
                    var matchingService = this.DeliveryServices?.FirstOrDefault(o => o == this.SelectedOrder?.DeliveryService);

                    this.SelectedDeliveryService = matchingService ?? this.DeliveryServices?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading shipping companies. Details:\r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }

            return Task.CompletedTask;
        }
        
        #endregion
    }
}