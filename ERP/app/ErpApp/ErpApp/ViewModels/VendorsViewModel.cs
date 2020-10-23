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
    public class VendorsViewModel : MvxViewModel
    {
        public VendorsViewModel(IMvxNavigationService navigationService, IErpService service, IMvxMessenger messenger, IAuthenticationService authenticationService)
        {
            this.navigationService = navigationService;
            this.service = service;
            this.CurrentUserName = authenticationService.UserName;
            this.vendorUpdatedMessageToken = messenger.SubscribeOnThreadPoolThread<VendorUpdatedMessage>(OnVendorUpdated);
            this.vendorDeletedMessageToken = messenger.SubscribeOnMainThread<VendorDeletedMessage>(OnVendorDeleted);

            this.currentLayoutMode = LayoutMode.Grid;
            this.ToggleLayoutModeCommand = new Command<LayoutMode?>(ChangeLayoutMode);
            this.SearchCommand = new MvxAsyncCommand(OnSearch);
            this.AboutCommand = new MvxCommand(ShowAboutPage);
            this.CreateVendorCommand = new MvxCommand(OnCreateVendor);
            this.EditVendorCommand = new MvxCommand<Vendor>(OnEditVendor);
            this.DeleteVendorCommand = new MvxAsyncCommand<Vendor>(OnDeleteVendor);
            this.listDescription = "All Vendors";
        }

        private IMvxNavigationService navigationService;
        private IErpService service;
        private Vendor selectedVendor;
        private ObservableCollection<Vendor> vendors;
        private LayoutMode currentLayoutMode;
        private bool isSearchEmpty, isBusy;
        private string draftSearchTerm, listDescription, currentUserName;
        private readonly MvxSubscriptionToken vendorUpdatedMessageToken, vendorDeletedMessageToken;

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

        public ObservableCollection<Vendor> Vendors
        {
            get => vendors;
            private set => SetProperty(ref vendors, value);
        }

        public Vendor SelectedVendor
        {
            get => selectedVendor;
            set
            {
                if (SetProperty(ref selectedVendor, value) && value != null)
                {
                    OnSelectedVendorChanged(value);
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

        public string CurrentUserName
        {
            get => this.currentUserName;
            private set => SetProperty(ref this.currentUserName, value);
        }

        public string ListDescription
        {
            get => listDescription;
            private set => SetProperty(ref listDescription, value);
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

        public ICommand ToggleLayoutModeCommand { get; }
        public ICommand CreateVendorCommand { get; }
        public ICommand EditVendorCommand { get; }
        public ICommand DeleteVendorCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand AboutCommand { get; }

        public async Task Refresh()
        {
            await this.service.RefreshCustomers();
            await this.FetchData();
        }

        private async Task FetchData()
        {
            var target = await this.service.GetVendorsAsync();
            ApplyVendorIndexing(target);
            this.Vendors = target;
        }

        private void OnSelectedVendorChanged(Vendor vendor)
        {
            if (Device.Idiom != TargetIdiom.Phone)
                return;

            this.navigationService.Navigate<VendorDetailViewModel, PresentationContext<string>>(new PresentationContext<string>(vendor.Id, PresentationMode.Read));
            this.SelectedVendor = null;
        }

        private void ChangeLayoutMode(LayoutMode? mode)
        {
            this.CurrentLayoutMode = mode ?? (this.CurrentLayoutMode == LayoutMode.Linear ?
                LayoutMode.Grid : LayoutMode.Linear);
        }

        private async Task DoSeach(string term)
        {
            ObservableCollection<Vendor> newVendors;
            if (string.IsNullOrEmpty(term))
                newVendors = await this.service.GetVendorsAsync();
            else
                newVendors = (await this.service.GetVendorsAsync(term));
            ApplyVendorIndexing(newVendors);
            this.Vendors = newVendors;
            ListDescription = string.IsNullOrEmpty(term) ? "All Vendors" : term;
            this.IsSearchEmpty = newVendors == null || !newVendors.Any();
        }

        private async void OnVendorUpdated(VendorUpdatedMessage message)
        {
            var updatedVendors = (await this.service.GetVendorsAsync());
            ApplyVendorIndexing(updatedVendors);
            Device.BeginInvokeOnMainThread(() => this.Vendors = updatedVendors);
        }

        private void OnVendorDeleted(VendorDeletedMessage message)
        {
            var found = this.vendors.SingleOrDefault(c => c.Id == message.Vendor.Id);
            if (found != null)
            {
                this.vendors.Remove(found);
                this.IsSearchEmpty = !this.vendors.Any();
                ApplyVendorIndexing(this.vendors);
            }
        }

        private void OnCreateVendor()
        {
            this.navigationService.Navigate<VendorDetailViewModel, PresentationContext<string>>(new PresentationContext<string>(null, PresentationMode.Create));
        }

        private void OnEditVendor(Vendor vendor)
        {
            if (vendor == null)
                return;

            this.navigationService.Navigate<VendorDetailViewModel, PresentationContext<string>>(new PresentationContext<string>(vendor.Id, PresentationMode.Edit));
        }

        private void ShowAboutPage()
        {
            this.navigationService.Navigate<AboutPageViewModel>();
        }

        private async Task OnSearch()
        {
            if (Device.Idiom != TargetIdiom.Phone)
                return;
                
            await this.navigationService.Navigate<SearchResultsViewModel, SearchRequest>(new SearchRequest(SearchResultsViewModel.VendorsContext, this.GetType()));
        }

        private async Task OnDeleteVendor(Vendor vendor)
        {
            if (vendor == null)
                return;

            bool result = await App.Current.MainPage.DisplayAlert("Delete product", $"Are you sure you want to delete vendor {vendor.Name}?", "Yes", "No");
            if (!result)
                return;

            await this.service.RemoveVendorAsync(vendor);
        }

        private static void ApplyVendorIndexing(IEnumerable<Vendor> vendors)
        {
            int index = 0;
            foreach (var item in vendors)
            {
                item.Index = index;
                index++;
            }
        }
    }
}
