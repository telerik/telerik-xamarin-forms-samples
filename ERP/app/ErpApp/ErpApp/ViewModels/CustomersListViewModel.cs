using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ErpApp.Models;
using ErpApp.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.ViewModels
{
    public class CustomersListViewModel : MvxViewModel
    {
        public CustomersListViewModel(Services.IErpService service, IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            this.service = service;
            this.navigationService = navigationService;
            this.messenger = messenger;
            this.customerUpdatedMessageToken = messenger.SubscribeOnThreadPoolThread<CustomerUpdatedMessage>(OnCustomerUpdated);
            this.customerDeletedMessageToken = messenger.SubscribeOnMainThread<CustomerDeletedMessage>(OnCustomerDeleted);
            this.customerSearchMessageToken = messenger.SubscribeOnMainThread<CustomerSearchMessage>(OnCustomerSearch);
            this.CreateCustomerCommand = new MvxCommand(OnCreateCustomer);
            this.SearchCommand = new MvxAsyncCommand(OnSearch);
            this.AboutCommand = new MvxCommand(ShowAboutPage);
            this.listDescription = "All Customers";
        }

        private Services.IErpService service;
        private IMvxMessenger messenger;
        private IMvxNavigationService navigationService;
        private Customer selectedCustomer, selectedCustomerDetail;
        private ObservableCollection<Customer> customers;
        private string listDescription;
        private bool isBusy;
        private readonly MvxSubscriptionToken customerUpdatedMessageToken, customerDeletedMessageToken, customerSearchMessageToken;

        public async override void ViewAppearing()
        {
            base.ViewAppearing();

            this.IsBusy = true;
            await FetchData();
            var firstCustomer = this.Customers?.FirstOrDefault();
            if (Device.Idiom != TargetIdiom.Phone && firstCustomer != null)
            {
                this.SelectedCustomer = firstCustomer;
                this.DisplayCustomerDetail(firstCustomer);
            }

            if (await this.service.SyncIfNeededAsync())
            {
                await FetchData();
            }

            this.IsBusy = false;
            if (Device.Idiom != TargetIdiom.Phone && this.customers?.Contains(this.selectedCustomerDetail) == false)
            {
                this.DisplayCustomerDetail(this.Customers?.FirstOrDefault());
            }
        }

        public ObservableCollection<Customer> Customers
        {
            get => customers;
            private set => SetProperty(ref customers, value);
        }

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                if (SetProperty(ref selectedCustomer, value) && value != null)
                {
                    OnSelectedCustomerChanged(value);
                }
            }
        }

        public string ListDescription
        {
            get => listDescription;
            private set => SetProperty(ref listDescription, value);
        }

        public IMvxCommand CreateCustomerCommand { get; }
        public IMvxCommand SearchCommand { get; }
        public IMvxCommand AboutCommand { get; }
        public bool IsBusy { get => isBusy; private set => SetProperty(ref isBusy, value); }

        public async Task Refresh()
        {
            await this.service.RefreshCustomers();
            await this.FetchData();
        }

        private async Task FetchData()
        {
            var newCustomers = (await this.service.GetCustomersAsync());
            SyncCustomers(newCustomers);
        }

        private void SyncCustomers(ObservableCollection<Customer> newCustomers)
        {
            if (this.customers == null || this.customers?.Count == 0 ||
                newCustomers == null || newCustomers?.Count == 0)
            {
                this.Customers = newCustomers;
            }
            else
            {
                foreach (var customerToAdd in newCustomers.Except(this.customers))
                {
                    this.customers.Add(customerToAdd);
                }

                var customersToRemove = this.customers.Except(newCustomers).ToArray();
                foreach (var customerToRemove in customersToRemove)
                {
                    this.customers.Remove(customerToRemove);
                }
            }
        }

        private void OnSelectedCustomerChanged(Customer newCustomer)
        {
            this.DisplayCustomerDetail(newCustomer);
            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.SelectedCustomer = null;
            }
        }

        private void DisplayCustomerDetail(Customer customer)
        {
            this.selectedCustomerDetail = customer;
            this.navigationService.Navigate<CustomerDetailViewModel, PresentationContext<string>>(new PresentationContext<string>(customer?.Id, PresentationMode.Read));
        }

        private async void OnCustomerUpdated(CustomerUpdatedMessage message)
        {
            var updatedCustomers = (await this.service.GetCustomersAsync());
            Device.BeginInvokeOnMainThread(() => this.Customers = updatedCustomers);
        }

        private void OnCustomerDeleted(CustomerDeletedMessage message)
        {
            var foundCustomer = this.customers.SingleOrDefault(c => c.Id == message.Customer.Id);
            if (foundCustomer != null)
            {
                this.customers.Remove(foundCustomer);
                if (Device.Idiom != TargetIdiom.Phone)
                {
                    this.DisplayCustomerDetail(this.customers.FirstOrDefault());
                }
            }
        }

        private async void OnCustomerSearch(CustomerSearchMessage message)
        {
            ObservableCollection<Customer> newCustomers;
            if (string.IsNullOrEmpty(message.SearchTerm))
                newCustomers = await this.service.GetCustomersAsync();
            else
                newCustomers = (await this.service.GetCustomersAsync(message.SearchTerm));
            SyncCustomers(newCustomers);
            ListDescription = string.IsNullOrEmpty(message.SearchTerm) ? "All Customers" : message.SearchTerm;
            this.messenger.Publish(new CustomerSearchPerformedMessage(this, newCustomers == null || !newCustomers.Any()));
        }

        private void ShowAboutPage()
        {
            this.navigationService.Navigate<AboutPageViewModel>();
        }

        private void OnCreateCustomer()
        {
            this.navigationService.Navigate<CustomerDetailViewModel, PresentationContext<string>>(new PresentationContext<string>(null, PresentationMode.Create));
        }

        private async Task OnSearch()
        {
            if (Device.Idiom != TargetIdiom.Phone)
                return;

            await this.navigationService.Navigate<SearchResultsViewModel, SearchRequest>(new SearchRequest(SearchResultsViewModel.CustomersContext, this.GetType()));
        }
    }
}
