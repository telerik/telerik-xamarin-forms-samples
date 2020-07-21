using ErpApp.Models;
using ErpApp.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using PresentationMode = ErpApp.Models.PresentationMode;

namespace ErpApp.ViewModels
{
    public class ProductsViewModel : MvxViewModel
    {
        public ProductsViewModel(IMvxNavigationService navigationService, IErpService service, IMvxMessenger messenger, IAuthenticationService authenticationService)
        {
            this.navigationService = navigationService;
            this.service = service;
            this.CurrentUserName = authenticationService.UserName;
            this.productUpdatedMessageToken = messenger.SubscribeOnThreadPoolThread<ProductUpdatedMessage>(OnProductUpdated);
            this.productDeletedMessageToken = messenger.SubscribeOnMainThread<ProductDeletedMessage>(OnProductDeleted);

            this.ProductStockCount = 12800;

            ExpectedStockQuantitues = new NameValuePair[]
            {
                new NameValuePair(DateTime.Today.AddMonths(-2).ToString("MMMM"), 1500),
                new NameValuePair(DateTime.Today.AddMonths(-1).ToString("MMMM"), 1400),
                new NameValuePair(DateTime.Today.ToString("MMMM"), 1600),
            };

            ActualStockQuantitues = new NameValuePair[]
            {
                new NameValuePair(DateTime.Today.AddMonths(-2).ToString("MMMM"), 1723),
                new NameValuePair(DateTime.Today.AddMonths(-1).ToString("MMMM"), 1413),
                new NameValuePair(DateTime.Today.ToString("MMMM"), 2313),
            };

            TopStockProducts = new NameValuePair[]
            {
                new NameValuePair("A", 1423),
                new NameValuePair("B", 2621),
                new NameValuePair("C", 1724),
                new NameValuePair("D", 2223),
                new NameValuePair("E", 1383)
            };

            TopSoldProducts = new NameValuePair[]
            {
                new NameValuePair("A", 14100),
                new NameValuePair("B", 12200),
                new NameValuePair("C", 11300)
            };

            StorageLocations = new NameValuePair[]
            {
                new NameValuePair("New York", 0.35),
                new NameValuePair("Ohio", 0.30),
                new NameValuePair("California", 0.35),
            };

            currentLayoutMode = LayoutMode.Grid;
            ToggleLayoutModeCommand = new Command<LayoutMode?>(ChangeLayoutMode);
            this.CreateProductCommand = new MvxCommand(OnCreateProduct);
            this.EditProductCommand = new MvxCommand<Product>(OnEditProduct);
            this.DeleteProductCommand = new MvxAsyncCommand<Product>(OnDeleteProduct);
            this.SearchCommand = new MvxAsyncCommand(OnSearch);
            this.AboutCommand = new MvxCommand(ShowAboutPage);
            this.listDescription = "All Products";
        }

        private IMvxNavigationService navigationService;
        private IErpService service;
        private Product selectedProduct;
        private ObservableCollection<Product> products;
        private LayoutMode currentLayoutMode;
        private bool isSearchEmpty, isBusy;
        private string draftSearchTerm, listDescription, currentUserName;
        private readonly MvxSubscriptionToken productUpdatedMessageToken, productDeletedMessageToken;

        public async override void Prepare()
        {
            base.Prepare();

            this.IsBusy = true;
            await FetchData();
            if (await this.service.SyncIfNeededAsync())
            {
                await FetchData();
            }
            this.IsBusy = false;
        }

        public ObservableCollection<Product> Products
        {
            get => products;
            private set => SetProperty(ref products, value);
        }

        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                if (SetProperty(ref selectedProduct, value) && value != null)
                {
                    OnSelectedProductChanged(value);
                }
            }
        }

        public LayoutMode CurrentLayoutMode
        {
            get => currentLayoutMode;
            private set => SetProperty(ref currentLayoutMode, value);
        }

        public string DraftSearchTerm
        {
            get => this.draftSearchTerm;
            set
            {
                if (SetProperty(ref this.draftSearchTerm, value))
                {
                    MvxNotifyTask.Create(async () => await DoSeach(value));
                }
            }
        }

        public string ListDescription
        {
            get => listDescription;
            private set => SetProperty(ref listDescription, value);
        }

        public string CurrentUserName
        {
            get => this.currentUserName;
            private set => SetProperty(ref this.currentUserName, value);
        }

        public bool IsSearchEmpty
        {
            get => isSearchEmpty;
            set => SetProperty(ref isSearchEmpty, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public IMvxCommand CreateProductCommand { get; }
        public IMvxCommand EditProductCommand { get; }
        public IMvxCommand DeleteProductCommand { get; }

        public int ProductStockCount { get; }
        public IReadOnlyCollection<NameValuePair> ExpectedStockQuantitues { get; }
        public IReadOnlyCollection<NameValuePair> ActualStockQuantitues { get; }
        public IReadOnlyCollection<NameValuePair> TopStockProducts { get; }
        public IReadOnlyCollection<NameValuePair> TopSoldProducts { get; }
        public IReadOnlyCollection<NameValuePair> StorageLocations { get; }
        public ICommand ToggleLayoutModeCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand AboutCommand { get; }

        public async Task Refresh()
        {
            await this.service.RefreshCustomers();
            await this.FetchData();
        }

        private async Task FetchData()
        {
            var target = await this.service.GetProductsAsync();
            ApplyProductIndexing(target);
            this.Products = target;
        }

        private void OnSelectedProductChanged(Product product)
        {
            if (Device.Idiom != TargetIdiom.Phone)
                return;

            this.navigationService.Navigate<ProductDetailViewModel, PresentationContext<string>>(new PresentationContext<string>(product.Id, PresentationMode.Read));
            this.SelectedProduct = null;
        }

        private void ChangeLayoutMode(LayoutMode? mode)
        {
            this.CurrentLayoutMode = mode ?? (this.CurrentLayoutMode == LayoutMode.Linear ?
                LayoutMode.Grid : LayoutMode.Linear);
        }

        private async Task DoSeach(string term)
        {
            ObservableCollection<Product> newProducts;
            if (string.IsNullOrEmpty(term))
                newProducts = await this.service.GetProductsAsync();
            else
                newProducts = (await this.service.GetProductsAsync(term));
            ApplyProductIndexing(newProducts);
            this.Products = newProducts;
            ListDescription = string.IsNullOrEmpty(term) ? "All Products" : term;
            this.IsSearchEmpty = newProducts == null || !newProducts.Any();
        }

        private async void OnProductUpdated(ProductUpdatedMessage message)
        {
            var updatedProducts = (await this.service.GetProductsAsync());
            ApplyProductIndexing(updatedProducts);
            Device.BeginInvokeOnMainThread(() => this.Products = updatedProducts);
        }

        private void OnProductDeleted(ProductDeletedMessage message)
        {
            var found = this.products.SingleOrDefault(c => c.Id == message.Product.Id);
            if (found != null)
            {
                this.products.Remove(found);
                this.IsSearchEmpty = !this.products.Any();
                ApplyProductIndexing(this.products);
            }
        }

        private void OnCreateProduct()
        {
            this.navigationService.Navigate<ProductDetailViewModel, PresentationContext<string>>(new PresentationContext<string>(null, PresentationMode.Create));
        }

        private void OnEditProduct(Product product)
        {
            if (product == null)
                return;

            this.navigationService.Navigate<ProductDetailViewModel, PresentationContext<string>>(new PresentationContext<string>(product.Id, PresentationMode.Edit));
        }

        private void ShowAboutPage()
        {
            this.navigationService.Navigate<AboutPageViewModel>();
        }

        private async Task OnDeleteProduct(Product product)
        {
            if (product == null)
                return;

            bool result = await App.Current.MainPage.DisplayAlert("Delete product", $"Are you sure you want to delete product {product.Name}?", "Yes", "No");
            if (!result)
                return;

            await this.service.RemoveProductAsync(product);
        }

        private async Task OnSearch()
        {
            if (Device.Idiom != TargetIdiom.Phone)
                return;

            await this.navigationService.Navigate<SearchResultsViewModel, SearchRequest>(new SearchRequest(SearchResultsViewModel.ProductsContext, this.GetType()));
        }

        private static void ApplyProductIndexing(IEnumerable<Product> products)
        {
            int index = 0;
            foreach (var item in products)
            {
                item.Index = index;
                index++;
            }
        }
    }
}
