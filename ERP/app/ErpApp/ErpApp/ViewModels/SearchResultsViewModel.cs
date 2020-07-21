using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ErpApp.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.ViewModels
{
    public class SearchResultsViewModel : MvxViewModel<SearchRequest>
    {
        public SearchResultsViewModel(IMvxNavigationService navigationService, Services.IErpService erpService)
        {
            this.erpService = erpService;
            this.navigationService = navigationService;
            this.CancelCommand = new MvxAsyncCommand(OnCancel);
        }

        private string searchContext, searchTerm;
        private bool isSearchEmpty;
        private Type senderType;
        private Services.IErpService erpService;
        private IMvxNavigationService navigationService;
        private IEnumerable source;
        private object selectedItem;
        private Func<string, Task> searchAction;

        public const string CustomersContext = "search_customers";
        public const string OrdersContext = "search_orders";
        public const string ProductsContext = "search_products";
        public const string VendorsContext = "search_vendors";

        public ICommand CancelCommand { get; }

        public IEnumerable Source
        {
            get => this.source;
            private set
            {
                if (SetProperty(ref this.source, value))
                {
                    this.IsSearchEmpty = value != null && !value.OfType<object>().Any();
                }
            }
        }

        public object SelectedItem
        {
            get => this.selectedItem;
            set
            {
                if (SetProperty(ref this.selectedItem, value) && value != null)
                {
                    MvxNotifyTask.Create(async () => await OnSelect(value));
                    this.selectedItem = null;
                }
            }
        }

        public string SearchTerm
        {
            get => this.searchTerm;
            set
            {
                if (SetProperty(ref this.searchTerm, value))
                {
                    DoSearch(value);
                }
            }
        }

        public bool IsSearchEmpty
        {
            get => isSearchEmpty;
            set => SetProperty(ref isSearchEmpty, value);
        }

        public override void Prepare(SearchRequest parameter)
        {
            this.searchContext = parameter.Context;
            this.senderType = parameter.Sender;

            switch (this.searchContext)
            {
                case CustomersContext:
                    this.searchAction = (term) => this.erpService.GetCustomersAsync(term)
                        .ContinueWith(antc => this.Source = antc.Result, TaskScheduler.FromCurrentSynchronizationContext());
                    break;
                case OrdersContext:
                    this.searchAction = (term) => this.erpService.GetOrdersAsync(term)
                        .ContinueWith(antc => this.Source = antc.Result, TaskScheduler.FromCurrentSynchronizationContext());
                    break;
                case ProductsContext:
                    this.searchAction = (term) => this.erpService.GetProductsAsync(term)
                        .ContinueWith(antc => this.Source = antc.Result, TaskScheduler.FromCurrentSynchronizationContext());
                    break;
                case VendorsContext:
                    this.searchAction = (term) => this.erpService.GetVendorsAsync(term)
                        .ContinueWith(antc => this.Source = antc.Result, TaskScheduler.FromCurrentSynchronizationContext());
                    break;
            }
        }

        private void DoSearch(string term)
        {
            this.searchAction(term);
        }

        private async Task OnCancel()
        {
            await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(this.senderType));
        }

        private async Task OnSelect(object item)
        {
            PresentationContext<string> ctx = new PresentationContext<string>((item as IEntity).Id, PresentationMode.Read);
            await this.navigationService.Navigate<PresentationContext<string>>(GetDetailViewModelForItem(item), ctx);
        }

        private Type GetDetailViewModelForItem(object item)
        {
            switch (item)
            {
                case Customer c:
                    return typeof(CustomerDetailViewModel);
                case Order c:
                    return typeof(OrderDetailViewModel);
                case Product c:
                    return typeof(ProductDetailViewModel);
                case Vendor c:
                    return typeof(VendorDetailViewModel);
            }
            return null;
        }
    }

    public class SearchRequest
    {
        public SearchRequest(string context, Type sender)
        {
            Context = context;
            Sender = sender;
        }

        public string Context { get; }
        public Type Sender { get; }
    }
}
