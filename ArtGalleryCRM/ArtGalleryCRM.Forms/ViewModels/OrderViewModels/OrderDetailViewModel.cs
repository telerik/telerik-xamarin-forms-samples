using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Services;
using ArtGalleryCRM.Forms.Views.OrderPages;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.OrderViewModels
{
    public class OrderDetailViewModel : PageViewModelBase
    {
        private Order _selectedOrder;
        private Customer _relatedCustomer;
        private Employee _relatedEmployee;
        private Product _relatedProduct;

        public OrderDetailViewModel() { }

        public OrderDetailViewModel(Order item = null)
        {
            this.Title = "Order Details";
            this.SelectedOrder = item;
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
        }

        public Order SelectedOrder
        {
            get => this._selectedOrder;
            set => SetProperty(ref this._selectedOrder, value);
        }

        public Customer RelatedCustomer
        {
            get => this._relatedCustomer;
            set => SetProperty(ref this._relatedCustomer, value);
        }

        public Employee RelatedEmployee
        {
            get => this._relatedEmployee;
            set => SetProperty(ref this._relatedEmployee, value);
        }

        public Product RelatedProduct
        {
            get => this._relatedProduct;
            set => SetProperty(ref this._relatedProduct, value);
        }

        public ICommand ToolbarCommand { get; set; }

        public override async void OnAppearing()
        {
            if (this.IsBusy || this.SelectedOrder == null)
            {
                return;
            }

            this.IsBusy = true;

            try
            {
                // Get the customer who requested the order
                try
                {
                    if (this.RelatedCustomer == null)
                    {
                        this.IsBusyMessage = "loading customer...";
                        this.RelatedCustomer = await DependencyService.Get<IDataStore<Customer>>().GetItemAsync(this.SelectedOrder.CustomerId);
                    }
                }
                catch (Exception ex)
                {
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Error",
                        Message = $"There was a problem loading the order's customer details:  \r\n\n{ex.Message}",
                        Cancel = "OK"
                    }, "Alert");
                }

                // Get the employee who placed the order
                try
                {
                    if (this.RelatedEmployee == null)
                    {
                        this.IsBusyMessage = "loading employee...";
                        this.RelatedEmployee = await DependencyService.Get<IDataStore<Employee>>().GetItemAsync(this.SelectedOrder.EmployeeId);
                    }
                }
                catch (Exception ex)
                {
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Error",
                        Message = $"There was a problem loading the order's employee details:  \r\n\n{ex.Message}",
                        Cancel = "OK"
                    }, "Alert");
                }

                // Get the product that was ordered
                try
                {
                    if (this.RelatedProduct == null)
                    {
                        this.IsBusyMessage = "loading product...";
                        this.RelatedProduct = await DependencyService.Get<IDataStore<Product>>().GetItemAsync(this.SelectedOrder.ProductId);
                    }
                }
                catch (Exception ex)
                {
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Error",
                        Message = $"There was a problem loading the order's product details:  \r\n\n{ex.Message}",
                        Cancel = "OK"
                    }, "Alert");
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading the order's related data. Details:\r\n\n{ex.Message}",
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
            if (this.SelectedOrder == null)
            {
                return;
            }

            switch (obj.ToString())
            {
                case "edit":
                    await this.NavigateForwardAsync(new OrderEditPage(this.SelectedOrder));
                    break;
                case "delete":
                    await this.DeleteOrderAsync();
                    break;
            }
        }

        private async Task DeleteOrderAsync()
        {
            await App.DisplayReadOnlyDemoAlert();
            return;

            // ****** Deletion Logic ****** //

            //var result = await Application.Current.MainPage.DisplayAlert("Delete?", "Do you really want to delete this item?", "Yes", "No");

            //if (!result)
            //{
            //    return;
            //}

            //try
            //{
            //    this.IsBusy = true;

            //    if (this.SelectedOrder == null)
            //    {
            //        return;
            //    }
                
            //    await DependencyService.Get<IDataStore<Order>>().DeleteItemAsync(this.SelectedOrder);
            //}
            //catch (Exception ex)
            //{
            //    // display error to user
            //}
            //finally
            //{
            //    this.IsBusy = false;
            //}
            
            //await this.NavigateBackAsync();
        }
    }
}