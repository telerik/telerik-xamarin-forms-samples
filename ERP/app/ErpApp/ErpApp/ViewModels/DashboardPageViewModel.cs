using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ErpApp.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ErpApp.ViewModels
{
    public class DashboardPageViewModel : MvxViewModel
    {
        public DashboardPageViewModel(Services.IErpService service, IMvxNavigationService navigationService)
        {
            this.service = service;
            this.navigationService = navigationService;
            this.AboutCommand = new MvxCommand(ShowAboutPage);
        }

        private IReadOnlyCollection<NameValuePair> salesChannels, businessOverview, orderStatusOverview,
            newCustomers, expectedSalesQuantitues, actualSalesQuantitues;
        private Services.IErpService service;
        private IReadOnlyCollection<Vendor> bestVendors;
        private IReadOnlyCollection<Product> recentProducts;
        private IReadOnlyCollection<Order> latestOrders;
        private IMvxNavigationService navigationService;
        private bool busy;

        public async override void Prepare()
        {
            base.Prepare();

            IsBusy = true;
            await this.FetchData();
            if (await this.service.SyncIfNeededAsync())
            {
                await FetchData();
            }
            IsBusy = false;
        }

        public IReadOnlyCollection<NameValuePair> SalesChannels { get => salesChannels; private set => SetProperty(ref salesChannels, value); }
        public IReadOnlyCollection<NameValuePair> BusinessOverview { get => businessOverview; private set => SetProperty(ref businessOverview, value); }
        public IReadOnlyCollection<NameValuePair> OrderStatusOverview { get => orderStatusOverview; private set => SetProperty(ref orderStatusOverview, value); }
        public IReadOnlyCollection<NameValuePair> NewCustomers { get => newCustomers; private set => SetProperty(ref newCustomers, value); }
        public IReadOnlyCollection<NameValuePair> ExpectedSalesQuantitues { get => expectedSalesQuantitues; private set => SetProperty(ref expectedSalesQuantitues, value); }
        public IReadOnlyCollection<NameValuePair> ActualSalesQuantitues { get => actualSalesQuantitues; private set => SetProperty(ref actualSalesQuantitues, value); }
        public IReadOnlyCollection<Vendor> BestVendors { get => bestVendors; private set => SetProperty(ref bestVendors, value); }
        public IReadOnlyCollection<Product> RecentProducts { get => recentProducts; private set => SetProperty(ref recentProducts, value); }
        public IReadOnlyCollection<Order> LatestOrders { get => latestOrders; private set => SetProperty(ref latestOrders, value); }
        public bool IsBusy { get => busy; private set => SetProperty(ref busy, value); }
        public MvxCommand AboutCommand { get; }

        private void ShowAboutPage()
        {
            this.navigationService.Navigate<AboutPageViewModel>();
        }

        private async Task FetchData()
        {
            this.BestVendors = (await this.service.GetVendorsAsync()).Take(2).ToArray();
            this.RecentProducts = (await this.service.GetProductsAsync()).Take(3).ToArray();
            var ordersToTake = (await this.service.GetOrdersAsync()).Take(3).Select(c => c.Id).ToArray();
            var orders = new ObservableCollection<Order>();
            foreach (var item in ordersToTake)
            {
                var order = await this.service.GetOrderAsync(item);
                orders.Add(order);
            }
            this.LatestOrders = orders;

            if (orders.Any())
            {
                SalesChannels = new NameValuePair[]
                {
                    new NameValuePair("Online", 0.25),
                    new NameValuePair("Offline", 0.75)
                };

                BusinessOverview = new NameValuePair[]
                {
                    new NameValuePair("New York", 0.35),
                    new NameValuePair("Ohio", 0.30),
                    new NameValuePair("California", 0.35)
                };

                OrderStatusOverview = new NameValuePair[]
                {
                    new NameValuePair("Pending", 0.60),
                    new NameValuePair("Completed", 0.40)
                };

                NewCustomers = new NameValuePair[]
                {
                    new NameValuePair(DateTime.Today.AddMonths(-2).ToString("MMMM"), 63),
                    new NameValuePair(DateTime.Today.AddMonths(-1).ToString("MMMM"), 48),
                    new NameValuePair(DateTime.Today.ToString("MMMM"), 60)
                };

                ExpectedSalesQuantitues = new NameValuePair[]
                {
                    new NameValuePair(DateTime.Today.AddMonths(-2).ToString("MMMM"), 1500),
                    new NameValuePair(DateTime.Today.AddMonths(-1).ToString("MMMM"), 1400),
                    new NameValuePair(DateTime.Today.ToString("MMMM"), 1600)
                };

                ActualSalesQuantitues = new NameValuePair[]
                {
                    new NameValuePair(DateTime.Today.AddMonths(-2).ToString("MMMM"), 1723),
                    new NameValuePair(DateTime.Today.AddMonths(-1).ToString("MMMM"), 1413),
                    new NameValuePair(DateTime.Today.ToString("MMMM"), 2313)
                };
            }
        }
    }
}
