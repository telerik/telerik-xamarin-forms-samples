using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Services;
using ArtGalleryCRM.Forms.Views.CustomerPages;
using Telerik.XamarinForms.DataControls.ListView;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.CustomerViewModels
{
    public class CustomersViewModel : PageViewModelBase
    {
        private int _selectedSegmentIndex;

        public CustomersViewModel()
        {
            this.Title = "Customers";
            this.ItemTapCommand = new Command<ItemTapCommandContext>(this.ItemTapped);
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
        }

        public List<string> GroupingOptions { get; } = new List<string> {"None", "City", "State", "Country"};

        public ObservableCollection<Customer> Customers { get; } = new ObservableCollection<Customer>();

        public ObservableCollection<GroupDescriptorBase> GroupDescriptors { get; set; }

        public int SelectedSegmentIndex
        {
            get => this._selectedSegmentIndex;
            set
            {
                this.SetProperty(ref this._selectedSegmentIndex, value);
                SetGrouping(value);
            }
        }

        public ICommand ItemTapCommand { get; set; }

        public ICommand ToolbarCommand { get; set; }

        public override async void OnAppearing()
        {
            await this.LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            if (this.IsBusy)
            {
                return;
            }

            try
            {
                if (this.Customers.Count == 0)
                {
                    this.IsBusy = true;
                    this.IsBusyMessage = "loading customers...";

                    var customers = await DependencyService.Get<IDataStore<Customer>>().GetItemsAsync();

                    foreach (var customer in customers)
                    {
                        this.Customers.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading customers, check your network connection and try again. Details: \r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
            finally
            {
                this.IsBusyMessage = "";
                this.IsBusy = false;
            }
        }

        private void SetGrouping(int groupOptionIndex)
        {
            try
            {
                if (this.GroupDescriptors == null)
                {
                    return;
                }

                this.GroupDescriptors.Clear();

                var propertyToGroupBy = this.GroupingOptions[groupOptionIndex];

                if (string.IsNullOrEmpty(propertyToGroupBy))
                {
                    return;
                }

                switch (propertyToGroupBy)
                {
                    case "None":
                    case null:
                        this.GroupDescriptors.Clear();
                        break;
                    default:
                        this.GroupDescriptors.Add(new PropertyGroupDescriptor {PropertyName = propertyToGroupBy});
                        break;
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"A problem occurred while grouping the customers, try a different grouping option. Details: \r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
        }

        private async void ItemTapped(ItemTapCommandContext context)
        {
            if (context.Item is Customer customer)
            {
                await this.NavigateForwardAsync(new CustomerDetailPage(new CustomerDetailViewModel(customer)));
            }
        }
        
        private async void ToolbarItemTapped(object obj)
        {
            switch (obj.ToString())
            {
                case "sync":
                    this.Customers.Clear();
                    await this.LoadCustomersAsync();
                    break;
                case "add":
                    await this.NavigateForwardAsync(new CustomerEditPage());
                    break;
            }
        }
    }
}