using ErpApp.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ErpApp.ViewModels
{
    public class OrderScheduleViewModel : MvxViewModel
    {
        public OrderScheduleViewModel(Services.IErpService service, IMvxNavigationService navigationService)
        {
            this.service = service;
            this.navigationService = navigationService;
            this.CompleteOrderCommand = new MvxAsyncCommand<Order>(CompleteOrder, CanCompleteOrder);
        }

        private Services.IErpService service;
        private IMvxNavigationService navigationService;
        private IReadOnlyCollection<Order> todayOrders;
        private bool isBusy;

        public ICommand CompleteOrderCommand { get; }

        public IReadOnlyCollection<Order> Orders
        {
            get => this.todayOrders;
            private set => SetProperty(ref this.todayOrders, value);
        }
        public bool IsBusy
        {
            get => this.isBusy;
            private set => SetProperty(ref this.isBusy, value);
        }

        public async override void Prepare()
        {
            base.Prepare();

            this.IsBusy = true;
            await this.FetchData();
            if (await this.service.SyncIfNeededAsync())
            {
                await FetchData();
            }
            this.IsBusy = false;
        }

        private async Task FetchData()
        {
            var ordersToTake = (await this.service.GetTodayOrdersAsync()).Select(c => c.Id).ToArray();
            var orders = new ObservableCollection<Order>();
            foreach (var item in ordersToTake)
            {
                var order = await this.service.GetOrderAsync(item);
                orders.Add(order);
            }
            this.Orders = orders;
        }

        private bool CanCompleteOrder(Order order)
        {
            return order == null ? false : !order.IsCompleted;
        }

        private async Task CompleteOrder(Order order)
        {
            if (order.IsCompleted)
                return;

            var result = await this.navigationService.Navigate<OrderCompletionViewModel, string, OrderCompletionViewModel.SigningResult>(order.Id);
            if (result?.IsSigned == true)
            {
                order.CompleteOrder();
                await this.service.SaveOrderAsync(order);
            }
        }
    }
}
