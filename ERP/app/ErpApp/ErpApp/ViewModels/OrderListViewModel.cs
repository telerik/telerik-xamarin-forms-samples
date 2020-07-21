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
    public class OrderListViewModel : MvxViewModel
    {
        public OrderListViewModel(Services.IErpService service, IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            this.service = service;
            this.navigationService = navigationService;
            this.messenger = messenger;
            this.orderUpdatedMessageToken = messenger.SubscribeOnThreadPoolThread<OrderUpdatedMessage>(OnOrderUpdated);
            this.orderDeletedMessageToken = messenger.SubscribeOnMainThread<OrderDeletedMessage>(OnOrderDeleted);
            this.orderSearchMessageToken = messenger.SubscribeOnMainThread<OrderSearchMessage>(OnOrderSearch);
            this.ShowOrderScheduleCommand = new MvxCommand(ShowOrderSchedule);
            this.CreateOrderCommand = new MvxCommand(CreateOrder);
            this.SearchCommand = new MvxAsyncCommand(OnSearch);
            this.AboutCommand = new MvxCommand(ShowAboutPage);
            this.listDescription = "All Orders";
        }

        private Services.IErpService service;
        private IMvxNavigationService navigationService;
        private IMvxMessenger messenger;
        private Order selectedOrder, selectedOrderDetail;
        private ObservableCollection<Order> orders;
        private string listDescription;
        private readonly MvxSubscriptionToken orderUpdatedMessageToken, orderDeletedMessageToken, orderSearchMessageToken;
        private bool isBusy;

        public async override void ViewAppearing()
        {
            base.ViewAppearing();

            this.IsBusy = true;
            await FetchData();
            var firstOrder = this.Orders?.FirstOrDefault();
            if (Device.Idiom != TargetIdiom.Phone && firstOrder != null)
            {
                this.SelectedOrder = firstOrder;
                this.DisplayOrderDetail(firstOrder);
            }

            if (await this.service.SyncIfNeededAsync())
            {
                await FetchData();
            }
            this.IsBusy = false;

            if (Device.Idiom != TargetIdiom.Phone && this.orders?.Contains(this.selectedOrderDetail) == false)
            {
                this.DisplayOrderDetail(this.Orders?.FirstOrDefault());
            }
        }

        public ObservableCollection<Order> Orders
        {
            get => orders;
            private set => SetProperty(ref orders, value);
        }

        public string ListDescription
        {
            get => listDescription;
            private set => SetProperty(ref listDescription, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            private set => SetProperty(ref isBusy, value);
        }

        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                if (SetProperty(ref selectedOrder, value) && value != null)
                {
                    OnSelectedOrderChanged(value);
                }
            }
        }

        public IMvxCommand ShowOrderScheduleCommand { get; }
        public IMvxCommand CreateOrderCommand { get; }
        public IMvxCommand SearchCommand { get; }
        public IMvxCommand AboutCommand { get; }

        public async Task Refresh()
        {
            await this.service.RefreshOrders();
            await this.FetchData();
        }

        private async Task FetchData()
        {
            var newOrders = (await this.service.GetOrdersAsync());
            SyncOrders(newOrders);
        }

        private void SyncOrders(ObservableCollection<Order> newOrders)
        {
            if (this.orders == null || this.orders?.Count == 0 ||
                newOrders == null || newOrders?.Count == 0)
            {
                this.Orders = newOrders;
            }
            else
            {
                foreach (var customerToAdd in newOrders.Except(this.orders))
                {
                    this.orders.Add(customerToAdd);
                }

                var ordersToRemove = this.orders.Except(newOrders).ToArray();
                foreach (var orderToRemove in ordersToRemove)
                {
                    this.orders.Remove(orderToRemove);
                }
            }
        }

        private void DisplayOrderDetail(Order order)
        {
            this.selectedOrderDetail = order;
            this.navigationService.Navigate<OrderDetailViewModel, PresentationContext<string>>(new PresentationContext<string>(order?.Id, PresentationMode.Read));
        }

        private void OnSelectedOrderChanged(Order newOrder)
        {
            this.DisplayOrderDetail(newOrder);
            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.SelectedOrder = null;
            }
        }

        private async void OnOrderUpdated(OrderUpdatedMessage message)
        {
            var updatedOrders = await this.service.GetOrdersAsync();
            Device.BeginInvokeOnMainThread(() => this.Orders = updatedOrders);
        }

        private void OnOrderDeleted(OrderDeletedMessage message)
        {
            var found = this.orders.SingleOrDefault(c => c.Id == message.Order.Id);
            if (found != null)
            {
                this.orders.Remove(found);
                if (Device.Idiom != TargetIdiom.Phone)
                {
                    this.DisplayOrderDetail(this.orders.FirstOrDefault());
                }
            }
        }

        private async void OnOrderSearch(OrderSearchMessage message)
        {
            ObservableCollection<Order> newOrders;
            if (string.IsNullOrEmpty(message.SearchTerm))
                newOrders = await this.service.GetOrdersAsync();
            else
                newOrders = (await this.service.GetOrdersAsync(message.SearchTerm));
            SyncOrders(newOrders);
            ListDescription = string.IsNullOrEmpty(message.SearchTerm) ? "All Orders" : message.SearchTerm;
            this.messenger.Publish(new OrderSearchPerformedMessage(this, newOrders == null || !newOrders.Any()));
        }

        private void ShowAboutPage()
        {
            this.navigationService.Navigate<AboutPageViewModel>();
        }

        private void ShowOrderSchedule()
        {
            this.navigationService.Navigate<OrderScheduleViewModel>();
        }

        private void CreateOrder()
        {
            this.navigationService.Navigate<OrderDetailViewModel, PresentationContext<string>>(new PresentationContext<string>(null, PresentationMode.Create));
        }

        private async Task OnSearch()
        {
            if (Device.Idiom != TargetIdiom.Phone)
                return;

            await this.navigationService.Navigate<SearchResultsViewModel, SearchRequest>(new SearchRequest(SearchResultsViewModel.OrdersContext, this.GetType()));
        }
    }
}
