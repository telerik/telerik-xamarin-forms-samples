using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ErpApp.Models;
using ErpApp.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using PresentationMode = ErpApp.Models.PresentationMode;

namespace ErpApp.ViewModels
{
    public class CustomerDetailViewModel : MvxViewModel<PresentationContext<string>>
    {
        public CustomerDetailViewModel(Services.IErpService erpService, Services.IContextService contextService, IMvxNavigationService navigationService, IMvxMessenger messenger, IAuthenticationService authenticationService)
        {
            this.erpService = erpService;
            this.contextService = contextService;
            this.navigationService = navigationService;
            this.messenger = messenger;
            this.CurrentUserName = authenticationService.UserName;
            this.BeginEditCommand = new Command(OnBeginEditCustomer);
            this.CommitCommand = new MvxAsyncCommand(OnCommitEditCustomer);
            this.DeleteCommand = new MvxAsyncCommand(OnDeleteCustomer);
            this.CancelCommand = new MvxAsyncCommand(OnCancel);
            this.DeleteAddressCommand = new MvxCommand<CustomerAddress>(OnDeleteCustomerAddress);
            this.EditAddressCommand = new MvxCommand<CustomerAddress>(OnEditCustomerAddress);
            this.CreateAddressCommand = new MvxCommand(OnCreateCustomerAddress);
            this.CommitAddressCommand = new MvxCommand(OnCommitCustomerAddress);

            this.customerSearchPerformedMessageToken = messenger
                .SubscribeOnMainThread<CustomerSearchPerformedMessage>((obj) => IsSearchEmpty = obj.IsEmpty);
            this.emptyImageName = "Item_add.png";
        }

        private Customer targetCustomer, draftCustomer;
        private CustomerAddress draftCustomerAddress;
        private MvxObservableCollection<CustomerAddress> draftCustomerAddresses;
        private string targetCustomerId, title, draftSearchTerm, currentUserName, emptyImageName;
        private bool editingAddress, isSearchEmpty, isEmpty;
        private Services.IErpService erpService;
        private Services.IContextService contextService;
        private IMvxMessenger messenger;
        private IMvxNavigationService navigationService;
        private IReadOnlyCollection<NameValuePair> satisfaction;
        private IReadOnlyCollection<NameValuePair> activity;
        private ImageSource channelImageSource;
        private PresentationMode mode;
        private readonly MvxSubscriptionToken customerSearchPerformedMessageToken;
        private const string TermContextKey = "CustomerDetail_Term";

        public Customer Customer
        {
            get => this.targetCustomer;
            private set
            {
                if (SetProperty(ref this.targetCustomer, value))
                {
                    RaisePropertyChanged(() => IsEditing);
                    RaisePropertyChanged(() => IsReading);
                    RaisePropertyChanged(() => IsEmpty);
                }
            }
        }

        public Customer DraftCustomer
        {
            get => this.draftCustomer;
            private set
            {
                if (SetProperty(ref this.draftCustomer, value))
                {
                    RaisePropertyChanged(() => IsEditing);
                    RaisePropertyChanged(() => IsReading);
                }
            }
        }

        public CustomerAddress DraftCustomerAddress
        {
            get => this.draftCustomerAddress;
            private set => SetProperty(ref this.draftCustomerAddress, value);
        }

        public MvxObservableCollection<CustomerAddress> DraftCustomerAddresses
        {
            get => this.draftCustomerAddresses;
            private set => SetProperty(ref this.draftCustomerAddresses, value);
        }

        public IReadOnlyCollection<NameValuePair> CustomerSatisfaction
        {
            get => this.satisfaction;
            private set => SetProperty(ref this.satisfaction, value);
        }

        public IReadOnlyCollection<NameValuePair> CustomerActivity
        {
            get => this.activity;
            private set => SetProperty(ref this.activity, value);
        }

        public string DraftSearchTerm
        {
            get => this.draftSearchTerm;
            set
            {
                if (SetProperty(ref this.draftSearchTerm, value))
                {
                    DoSeach(value);
                }
            }
        }

        public string CurrentUserName
        {
            get => this.currentUserName;
            set => SetProperty(ref this.currentUserName, value);
        }

        public string EmptyImageName
        {
            get => this.emptyImageName;
            set => SetProperty(ref this.emptyImageName, value);
        }

        public bool IsSearchEmpty
        {
            get => isSearchEmpty;
            set => SetProperty(ref isSearchEmpty, value);
        }

        public bool IsEmpty
        {
            get => isEmpty;
            set => SetProperty(ref isEmpty, value);
        }

        public PresentationMode Mode
        {
            get => this.mode;
            private set
            {
                if (SetProperty(ref this.mode, value))
                {
                    RaisePropertyChanged(() => IsEditing);
                    RaisePropertyChanged(() => IsReading);
                }
            }
        }

        public bool IsReading => this.targetCustomer != null && this.mode == PresentationMode.Read;
        public bool IsEditing => this.draftCustomer != null &&
            (this.mode == PresentationMode.Edit || this.mode == PresentationMode.Create);

        public bool IsEditingAddress
        {
            get => this.editingAddress;
            private set => SetProperty(ref this.editingAddress, value);
        }

        public ImageSource CustomerPreferredCommunicationChannelImageSource
        {
            get => this.channelImageSource;
            private set => SetProperty(ref this.channelImageSource, value);
        }

        public string Title
        {
            get => this.title;
            private set => SetProperty(ref this.title, value);
        }

        public Command BeginEditCommand { get; }
        public IMvxCommand CommitCommand { get; }
        public IMvxCommand CancelCommand { get; }
        public IMvxCommand DeleteCommand { get; }
        public IMvxCommand CreateAddressCommand { get; }
        public IMvxCommand CommitAddressCommand { get; }
        public IMvxCommand<CustomerAddress> DeleteAddressCommand { get; }
        public IMvxCommand<CustomerAddress> EditAddressCommand { get; }

        public override void Prepare(PresentationContext<string> parameter)
        {
            this.targetCustomerId = parameter.Model;
            this.Mode = parameter.Mode;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            Customer customer = null;
            this.isEmpty = false;
            if (this.mode == PresentationMode.Create)
            {
                customer = new Customer();
                customer.CustomerSatisfaction = 0.5;
                customer.PreferredCommunicationChannel = "email";
                customer.ImageURL = Constants.EmptyCustomerImage;
                this.DraftCustomer = customer;
                this.DraftCustomerAddresses = new MvxObservableCollection<CustomerAddress>();
                this.UpdateTitle();
                return;
            }

            if (!string.IsNullOrEmpty(this.targetCustomerId))
            {
                customer = await this.erpService.GetCustomerAsync(this.targetCustomerId);
            }

            if (customer == null)
            {
                // We set the Customer to new instance to visualize the customer detail view for tablet
                this.isEmpty = true;
                this.Customer = new Customer();
                return;
            }

            this.Customer = customer;
            UpdateCustomerAttributes(customer);
            UpdateTitle();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            if (this.contextService.TryGet(TermContextKey, out object term))
                this.DraftSearchTerm = (string)term;
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();

            if (!string.IsNullOrEmpty(this.draftSearchTerm))
                this.contextService.Store(TermContextKey, this.draftSearchTerm);
            else
                this.contextService.Remove(TermContextKey);
        }

        private void UpdateTitle()
        {
            switch (this.mode)
            {
                case PresentationMode.Read:
                case PresentationMode.Edit:
                    this.Title = this.Customer?.Name;
                    break;
                case PresentationMode.Create:
                    this.Title = "Create customer";
                    break;
            }
        }

        private void UpdateCustomerAttributes(Customer customer)
        {
            if (this.mode == PresentationMode.Read)
            {
                this.CustomerSatisfaction = new NameValuePair[]
                {
                    new NameValuePair("Positive", System.Convert.ToDouble(customer.CustomerSatisfaction)),
                    new NameValuePair("Negative", System.Convert.ToDouble(1 - customer.CustomerSatisfaction))
                };

                CustomerActivity = new NameValuePair[]
                {
                    new NameValuePair("1", 8),
                    new NameValuePair("2", 7),
                    new NameValuePair("3", 8),
                    new NameValuePair("4", 6),
                };

                this.UpdateCustomerPreferredCommunicationChannelImageSource(customer);
            }
        }

        private void UpdateCustomerPreferredCommunicationChannelImageSource(Customer target)
        {
            this.CustomerPreferredCommunicationChannelImageSource = string.Equals(target?.PreferredCommunicationChannel, "email", System.StringComparison.InvariantCultureIgnoreCase) ?
                EmbeddedImages.MailImage : EmbeddedImages.PhoneImage;
        }

        private void OnBeginEditCustomer()
        {
            if (!this.IsReading)
                return;

            this.DraftCustomer = this.targetCustomer.Copy();
            this.DraftCustomerAddresses = new MvxObservableCollection<CustomerAddress>(this.draftCustomer.Addresses);
            this.Mode = PresentationMode.Edit;
            this.IsEditingAddress = false;
            UpdateCustomerAttributes(this.draftCustomer);
            UpdateTitle();
        }

        private async Task OnCommitEditCustomer()
        {
            if (this.Mode == PresentationMode.Read)
                return;

            if (!this.draftCustomer.Validate(out IList<string> errors))
            {
                await App.Current.MainPage.DisplayAlert("Validation failed", "Please check your data and try again" + Environment.NewLine + String.Join(Environment.NewLine, errors), "OK");
                return;
            }

            this.draftCustomer.Addresses = this.draftCustomerAddresses.ToArray();

            var updatedCustomer = await this.erpService.SaveCustomerAsync(this.draftCustomer);

            this.DraftCustomer = null;
            this.DraftCustomerAddresses = null;
            this.IsEditingAddress = false;
            updatedCustomer.Orders = targetCustomer?.Orders;
            this.targetCustomer = updatedCustomer;
            await this.RaisePropertyChanged(() => Customer);
            this.Mode = PresentationMode.Read;

            this.UpdateTitle();
            this.UpdateCustomerAttributes(updatedCustomer);
        }

        private async Task OnDeleteCustomer()
        {
            bool result = await App.Current.MainPage.DisplayAlert("Delete customer", $"Are you sure you want to delete customer {this.Customer.Name}?", "Yes", "No");
            if (!result)
                return;

            await this.erpService.RemoveCustomerAsync(this.Customer);
            if (Device.Idiom == TargetIdiom.Phone)
            {
                await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxRemovePresentationHint(typeof(CustomerDetailViewModel)));
            }
        }

        private async Task OnCancel()
        {
            if (this.mode == PresentationMode.Read)
                return;

            this.DraftCustomer = null;
            this.DraftCustomerAddresses = null;
            this.IsEditingAddress = false;

            if (this.mode == PresentationMode.Edit)
            {
                this.Mode = PresentationMode.Read;
                return;
            }

            if (Device.Idiom == TargetIdiom.Phone)
                await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(CustomersListViewModel)));
        }

        private void OnCreateCustomerAddress()
        {
            this.DraftCustomerAddress = new CustomerAddress()
            {
                CustomerID = this.draftCustomer.Id,
                Index = this.draftCustomerAddresses.Count + 1
            };
            this.IsEditingAddress = true;
        }

        private void OnEditCustomerAddress(CustomerAddress address)
        {
            this.DraftCustomerAddress = address;
            this.IsEditingAddress = true;
        }

        private void OnDeleteCustomerAddress(CustomerAddress address)
        {
            this.DraftCustomerAddresses.Remove(address);
        }

        private void OnCommitCustomerAddress()
        {
            if (!this.IsEditingAddress)
                return;

            if (!this.draftCustomerAddresses.Contains(this.draftCustomerAddress))
                this.draftCustomerAddresses.Add(this.draftCustomerAddress);

            this.draftCustomerAddress.InvalidateFullAddress();

            this.IsEditingAddress = false;
        }

        private void DoSeach(string term)
        {
            this.messenger.Publish<CustomerSearchMessage>(new CustomerSearchMessage(this, term));
        }
    }

    public class CustomerSearchMessage : MvxMessage
    {
        public CustomerSearchMessage(object sender, string term)
            : base(sender)
        {
            SearchTerm = term;
        }

        public string SearchTerm { get; }
    }

    public class CustomerSearchPerformedMessage : MvxMessage
    {
        public CustomerSearchPerformedMessage(object sender, bool isEmpty) : base(sender)
        {
            this.IsEmpty = isEmpty;
        }

        public bool IsEmpty { get; }
    }
}

