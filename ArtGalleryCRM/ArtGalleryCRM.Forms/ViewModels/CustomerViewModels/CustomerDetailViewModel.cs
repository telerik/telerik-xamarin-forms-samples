using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Services;
using ArtGalleryCRM.Forms.ViewModels.OrderViewModels;
using ArtGalleryCRM.Forms.Views.CustomerPages;
using ArtGalleryCRM.Forms.Views.OrderPages;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.CustomerViewModels
{
    public class CustomerDetailViewModel : PageViewModelBase
    {
        private Customer _selectedCustomer;
        private bool _ordersLoaded;

        public CustomerDetailViewModel() { }
        
        public CustomerDetailViewModel(Customer item = null)
        {
            this.SelectedCustomer = item;
            this.RefreshRequestedCommand = new Command<PullToRefreshRequestedCommandContext>(this.RefreshRequested);
            this.ItemTapCommand = new Command<ItemTapCommandContext>(this.ItemTapped);
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
        }

        public Customer SelectedCustomer
        {
            get => this._selectedCustomer;
            set => SetProperty(ref this._selectedCustomer, value);
        }

        public ObservableCollection<Order> SelectedCustomerOrders { get; } = new ObservableCollection<Order>();

        public ICommand ItemTapCommand { get; set; }

        public ICommand RefreshRequestedCommand { get; set; }

        public ICommand ToolbarCommand { get; set; }

        public override async void OnAppearing()
        {
            await this.LoadSelectedCustomerOrdersAsync();
        }

        public async Task LoadSelectedCustomerOrdersAsync()
        {
            if (this.IsBusy || this.SelectedCustomer == null)
            {
                return;
            }
            
            try
            {
                this.IsBusy = true;
                this.IsBusyMessage = "loading orders...";

                if (this._ordersLoaded)
                {
                    return;
                }

                var allOrders = await DependencyService.Get<IDataStore<Order>>().GetItemsAsync();

                this.FilterCustomerOrders(allOrders);
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading {this.SelectedCustomer.Name}'s orders. Details:\r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
            finally
            {
                this.IsBusyMessage = "";
                this.IsBusy = false;
            }
        }

        private void FilterCustomerOrders(IReadOnlyList<Order> orders)
        {
            if (orders?.Count > 0)
            {
                var customerOrders = orders.Where(o => o.CustomerId == this.SelectedCustomer.Id);

                foreach (var customerOrder in customerOrders)
                {
                    this.SelectedCustomerOrders?.Add(customerOrder);
                }

                this._ordersLoaded = true;
            }
        }

        private async void RefreshRequested(PullToRefreshRequestedCommandContext context)
        {
            await this.LoadSelectedCustomerOrdersAsync();

            context.Cancel();
        }

        private async void ItemTapped(ItemTapCommandContext context)
        {
            if (context.Item is Order order)
            {
                await this.NavigateForwardAsync(new OrderDetailPage(new OrderDetailViewModel(order)));
            }
        }

        private async void ToolbarItemTapped(object obj)
        {
            if (this.SelectedCustomer == null)
            {
                return;
            }

            switch (obj.ToString())
            {
                case "order":
                    await this.NavigateForwardAsync(new OrderEditPage(this.SelectedCustomer));
                    break;
                case "edit":
                    await this.NavigateForwardAsync(new CustomerEditPage(this.SelectedCustomer));
                    break;
                case "delete":
                    await this.DeleteCustomerAsync();
                    break;
            }
        }

        private async Task DeleteCustomerAsync()
        {
            await App.DisplayReadOnlyDemoAlert();
            return;

            // ****** Deletion Logic ****** //

            //var result = await App.Current.MainPage.DisplayAlert("Delete?", "Do you really want to delete this item?", "Yes", "No");

            //if (!result)
            //{
            //    return;
            //}

            //var item = this.SelectedCustomer;

            //if (item == null)
            //{
            //    return;
            //}

            //try
            //{
            //    this.IsBusy = true;

            //    // Referential Integrity Check
            //    var orders = await DependencyService.Get<IDataStore<Order>>().GetItemsAsync();
            //    var matchingOrders = orders.Where(o => o.EmployeeId == this?.SelectedCustomer.Id).ToList();

            //    if (matchingOrders.Count > 0)
            //    {
            //        var deleteAll = await App.Current.MainPage.DisplayAlert("!!! WARNING !!!", $"There are orders in the database for {this.SelectedCustomer.Name}. If you delete this customer, you'll also delete all of the associated orders.\r\n\nDo you wish to delete everything?", "Delete All", "Cancel");

            //        // Back out if user declines to delete associated orders
            //        if (!deleteAll)
            //            return;

            //        // Delete each associated Order
            //        foreach (var order in matchingOrders)
            //        {
            //            await DependencyService.Get<IDataStore<Order>>().DeleteItemAsync(order);
            //        }
            //    }

            //    // Lastly, delete the employee
            //    await DependencyService.Get<IDataStore<Customer>>().DeleteItemAsync(this.SelectedCustomer);
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