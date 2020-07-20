using System;
using System.Collections.Generic;
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
using PresentationMode = ErpApp.Models.PresentationMode;

namespace ErpApp.ViewModels
{
    public class OrderDetailViewModel : MvxViewModel<PresentationContext<string>>
    {
        public OrderDetailViewModel(Services.IErpService erpService, Services.IContextService contextService, IMvxNavigationService navigationService, IMvxMessenger messenger, IAuthenticationService authenticationService)
        {
            this.erpService = erpService;
            this.contextService = contextService;
            this.navigationService = navigationService;
            this.messenger = messenger;
            this.CurrentUserName = authenticationService.UserName;
            this.BeginEditCommand = new MvxAsyncCommand(OnBeginEditOrder);
            this.DeleteCommand = new MvxAsyncCommand(OnDeleteOrder);
            this.CommitCommand = new MvxAsyncCommand(OnCommitEditOrder);
            this.CancelCommand = new MvxAsyncCommand(OnCancel);
            this.AllProducts = new Telerik.XamarinForms.Common.ObservableItemCollection<OrderDetail>();
            this.AllProducts.ItemPropertyChanged += OnAllProductsItemPropertyChanged;
            this.orderSearchPerformedMessageToken = messenger
                .SubscribeOnMainThread<OrderSearchPerformedMessage>((obj) => IsSearchEmpty = obj.IsEmpty);
            this.emptyImageName = "Item_add.png";
        }

        private Order targetOrder, draftOrder;
        private string targetOrderId, title, draftSearchTerm, currentUserName, emptyImageName;
        private bool isSearchEmpty, isEmpty;
        private Services.IErpService erpService;
        private Services.IContextService contextService;
        private IMvxMessenger messenger;
        private IMvxNavigationService navigationService;
        private PresentationMode mode;
        private ObservableCollection<Customer> customers;
        private readonly MvxSubscriptionToken orderSearchPerformedMessageToken;
        private const string TermContextKey = "OrderDetail_Term";

        public Order Order
        {
            get => this.targetOrder;
            private set
            {
                if (SetProperty(ref this.targetOrder, value))
                {
                    RaisePropertyChanged(() => OrdersList);
                    RaisePropertyChanged(() => IsEditing);
                    RaisePropertyChanged(() => IsReading);
                    RaisePropertyChanged(() => IsEmpty);
                }
            }
        }

        public Order DraftOrder
        {
            get => this.draftOrder;
            private set
            {
                if (SetProperty(ref this.draftOrder, value))
                {
                    RaisePropertyChanged(() => IsEditing);
                    RaisePropertyChanged(() => IsReading);
                }
            }
        }

        public IEnumerable<Order> OrdersList => GetOrdersList();

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

        public bool IsSearchEmpty
        {
            get => isSearchEmpty;
            set => SetProperty(ref isSearchEmpty, value);
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

        public string Title
        {
            get => this.title;
            private set => SetProperty(ref this.title, value);
        }

        public Telerik.XamarinForms.Common.ObservableItemCollection<OrderDetail> AllProducts { get; }

        public ObservableCollection<Customer> AllCustomers
        {
            get => this.customers;
            private set => SetProperty(ref this.customers, value);
        }

        public Customer SelectedCustomer
        {
            get => this.draftOrder?.Customer;
            set
            {
                if (this.draftOrder == null)
                    return;

                this.draftOrder.Customer = value;
                RaisePropertyChanged(() => SelectedCustomer);
                RaisePropertyChanged(() => CustomerAddresses);
                Device.StartTimer(TimeSpan.MinValue, () =>
                {
                    this.SelectedShippingAddress = CustomerAddresses?.FirstOrDefault();
                    return false;
                });
            }
        }

        public ICollection<CustomerAddress> CustomerAddresses => this.draftOrder?.CustomerAddresses;

        public CustomerAddress SelectedShippingAddress
        {
            get => this.draftOrder?.ShippingAddress;
            set
            {
                if (this.draftOrder == null)
                    return;

                this.draftOrder.ShippingAddress = value;
                RaisePropertyChanged(() => SelectedShippingAddress);
            }
        }

        public bool IsReading => this.targetOrder != null && this.mode == PresentationMode.Read;

        public bool IsEmpty => this.isEmpty;

        public bool IsEditing => this.draftOrder != null &&
            (this.mode == PresentationMode.Edit || this.mode == PresentationMode.Create);

        public IMvxCommand BeginEditCommand { get; }
        public IMvxCommand CommitCommand { get; }
        public IMvxCommand CancelCommand { get; }
        public IMvxCommand DeleteCommand { get; }

        public override void Prepare(PresentationContext<string> parameter)
        {
            this.targetOrderId = parameter.Model;
            this.Mode = parameter.Mode;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            Order order = null;
            this.isEmpty = false;
            if (this.mode == PresentationMode.Create)
            {
                order = new Order();
                order.OrderDetails = new ObservableCollection<OrderDetail>();
                order.OrderDate = DateTime.Now;
                order.DueDate = DateTime.Today.AddDays(7);
                order.ShipMethod = Order.AvailableShipMethods[0];
                order.ShippingAddressModifiedDate = DateTime.Now;
                order.Status = "In Progress";
                this.DraftOrder = order;
                this.UpdateTitle();
                await this.InitializeEditData();
                return;
            }

            if (!string.IsNullOrEmpty(this.targetOrderId))
            {
                order = await this.erpService.GetOrderAsync(this.targetOrderId);
            }

            if (order == null)
            {
                // We set the Order to new instance to visualize the order detail view for tablet
                this.isEmpty = true;
                this.Order = new Order();
                return;
            }

            this.Order = order;
            this.UpdateTitle();
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

        private async Task InitializeEditData()
        {
            var products = (await this.erpService.GetProductsAsync()).Select(OrderDetail.Create);
            this.AllProducts.Clear();
            foreach (var item in products)
            {
                this.AllProducts.Add(item);
            }

            this.AllCustomers = await this.erpService.GetCustomersAsync();
        }

        private void UpdateTitle()
        {
            switch (this.mode)
            {
                case PresentationMode.Read:
                    this.Title = $"Order ID: #{this.targetOrder.OrderNumber}";
                    break;
                case PresentationMode.Edit:
                    this.Title = $"Edit Order #{this.targetOrder.OrderNumber}";
                    break;
                case PresentationMode.Create:
                    this.Title = "Add New Order";
                    break;
            }
        }

        private async Task OnBeginEditOrder()
        {
            if (!this.IsReading || this.targetOrder.IsCompleted)
                return;

            var order = this.targetOrder.Copy();
            this.Mode = PresentationMode.Edit;
            UpdateTitle();
            await this.InitializeEditData();
            order.OrderDetails = UpdateOrderDetails(order);
            this.DraftOrder = order;
        }

        private async Task OnDeleteOrder()
        {
            bool result = await App.Current.MainPage.DisplayAlert("Delete order", $"Are you sure you want to delete order #{this.targetOrder.OrderNumber}?", "Yes", "No");
            if (!result)
                return;

            await this.erpService.RemoveOrderAsync(this.Order);
            if (Device.Idiom == TargetIdiom.Phone)
            {
                await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(OrderListViewModel)));
            }
        }

        private async Task OnCancel()
        {
            if (this.mode == PresentationMode.Read)
                return;

            this.DraftOrder = null;
            this.ClearAllProductsQuantity();
            this.UpdateTitle();

            if (this.mode == PresentationMode.Edit)
            {
                this.Mode = PresentationMode.Read;
                return;
            }

            if (Device.Idiom == TargetIdiom.Phone)
                await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(CustomersListViewModel)));
        }

        private async Task OnCommitEditOrder()
        {
            if (this.Mode == PresentationMode.Read)
                return;

            if (!this.draftOrder.Validate(out IList<string> errors))
            {
                await App.Current.MainPage.DisplayAlert("Validation failed", "Please check your data and try again" + Environment.NewLine + String.Join(Environment.NewLine, errors), "OK");
                return;
            }

            this.draftOrder.OrderDetails = this.draftOrder.OrderDetails.Select(c => c.Copy()).ToArray();

            var updatedOrder = await this.erpService.SaveOrderAsync(this.draftOrder);

            this.DraftOrder = null;
            this.ClearAllProductsQuantity();
            this.targetOrder = null;
            this.Order = updatedOrder;
            this.Mode = PresentationMode.Read;

            this.UpdateTitle();
        }

        private IEnumerable<Order> GetOrdersList()
        {
            yield return targetOrder;
        }

        private void OnAllProductsItemPropertyChanged(object sender, Telerik.XamarinForms.Common.ItemPropertyChangedEventArgs<OrderDetail> e)
        {
            if (this.draftOrder == null || e.PropertyName != nameof(OrderDetail.Count))
                return;

            var target = e.Item;
            if (target.Count == 0)
            {
                // remove
                this.draftOrder.OrderDetails.Remove(target);
            }
            else
            {
                // add
                if (!this.draftOrder.OrderDetails.Contains(target, new OrderDetailIDEqualityComparer()))
                    this.draftOrder.OrderDetails.Add(target);
            }
            this.draftOrder.InvalidateAmmount();
        }

        private void ClearAllProductsQuantity()
        {
            foreach (var item in this.AllProducts)
            {
                item.Quantity = 0;
            }
        }

        private IList<OrderDetail> UpdateOrderDetails(Order order)
        {
            var orderDetails = new ObservableCollection<OrderDetail>(order.OrderDetails.Select(c => AllProducts.SingleOrDefault(cw => cw.ProductId == c.ProductId)));

            foreach (var detail in order.OrderDetails)
            {
                var item = orderDetails.SingleOrDefault(c => c.ProductId == detail.ProductId);
                if (item == null)
                    continue;

                item.Count = detail.Count;
            }

            return orderDetails;
        }

        private void DoSeach(string term)
        {
            this.messenger.Publish<OrderSearchMessage>(new OrderSearchMessage(this, term));
        }
    }

    public class OrderSearchMessage : MvxMessage
    {
        public OrderSearchMessage(object sender, string term)
            : base(sender)
        {
            SearchTerm = term;
        }

        public string SearchTerm { get; }
    }

    public class OrderSearchPerformedMessage : MvxMessage
    {
        public OrderSearchPerformedMessage(object sender, bool isEmpty) : base(sender)
        {
            this.IsEmpty = isEmpty;
        }

        public bool IsEmpty { get; }
    }
}

