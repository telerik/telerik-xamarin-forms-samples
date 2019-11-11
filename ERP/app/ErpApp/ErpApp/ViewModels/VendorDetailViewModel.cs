using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpApp.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.ViewModels
{
    public class VendorDetailViewModel : MvxViewModel<PresentationContext<string>>
    {
        public VendorDetailViewModel(Services.IErpService erpService, IMvxNavigationService navigationService)
        {
            this.erpService = erpService;
            this.navigationService = navigationService;
            this.BeginEditCommand = new Command(OnBeginEditVendor);
            this.CommitCommand = new MvxAsyncCommand(OnCommitEditOrder);
            this.DeleteCommand = new MvxAsyncCommand(OnDeleteVendor);
            this.CancelCommand = new MvxAsyncCommand(OnCancel);
        }

        private Services.IErpService erpService;
        private IMvxNavigationService navigationService;
        private Vendor targetVendor, draftVendor;
        private string targetVendorId;
        private PresentationMode mode;
        private string title;

        public Vendor Vendor
        {
            get => this.targetVendor;
            private set
            {
                if (SetProperty(ref this.targetVendor, value))
                {
                    RaisePropertyChanged(() => IsEditing);
                    RaisePropertyChanged(() => IsReading);
                }
            }
        }

        public Vendor DraftVendor
        {
            get => this.draftVendor;
            private set
            {
                if (SetProperty(ref this.draftVendor, value))
                {
                    RaisePropertyChanged(() => IsEditing);
                    RaisePropertyChanged(() => IsReading);
                }
            }
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

        public bool IsReading => this.targetVendor != null && this.mode == PresentationMode.Read;
        public bool IsEditing => this.draftVendor != null &&
            (this.mode == PresentationMode.Edit || this.mode == PresentationMode.Create);

        public string Title
        {
            get => this.title;
            private set => SetProperty(ref this.title, value);
        }

        public Command BeginEditCommand { get; }
        public IMvxCommand CommitCommand { get; }
        public IMvxCommand CancelCommand { get; }
        public IMvxCommand DeleteCommand { get; }

        public override void Prepare(PresentationContext<string> parameter)
        {
            this.targetVendorId = parameter.Model;
            this.Mode = parameter.Mode;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            Vendor vendor = null;
            if (this.mode == PresentationMode.Create)
            {
                vendor = new Vendor();
                vendor.ImageURL = Constants.EmptyVendorImage;
                vendor.LastOrderDate = DateTime.Today;
                vendor.SalesAmount = 500;
                this.DraftVendor = vendor;
                this.UpdateTitle();
                this.InitializeEditData(vendor);
                return;
            }

            if (!string.IsNullOrEmpty(this.targetVendorId))
            {
                vendor = await this.erpService.GetVendorAsync(this.targetVendorId);
            }

            if (vendor == null)
                return;

            if (this.mode == PresentationMode.Edit)
            {
                this.targetVendor = vendor;
                var copy = vendor.Copy();
                this.DraftVendor = copy;
                this.InitializeEditData(copy);
            }
            else
            {
                this.Vendor = vendor;
            }
            this.UpdateTitle();
        }

        private void UpdateTitle()
        {
            switch (this.mode)
            {
                case PresentationMode.Read:
                    this.Title = this.targetVendor.Name;
                    break;
                case PresentationMode.Edit:
                    this.Title = $"Edit Vendor";
                    break;
                case PresentationMode.Create:
                    this.Title = "Add New Vendor";
                    break;
            }
        }

        private void OnBeginEditVendor()
        {
            if (!this.IsReading)
                return;

            var vendor = this.targetVendor.Copy();
            this.Mode = PresentationMode.Edit;
            UpdateTitle();
            this.InitializeEditData(vendor);
            this.DraftVendor = vendor;
        }

        private async Task OnDeleteVendor()
        {
            bool result = await App.Current.MainPage.DisplayAlert("Delete vendor", $"Are you sure you want to delete vendor {this.targetVendor.Name}?", "Yes", "No");
            if (!result)
                return;

            await this.erpService.RemoveVendorAsync(this.targetVendor);
            if (Device.Idiom == TargetIdiom.Phone)
            {
                await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(VendorsViewModel)));
            }
        }

        private async Task OnCancel()
        {
            if (this.mode == PresentationMode.Read)
                return;

            this.DraftVendor = null;
            this.UpdateTitle();

            if (this.mode == PresentationMode.Edit)
            {
                this.Mode = PresentationMode.Read;
            }

            await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(VendorsViewModel)));
        }

        private async Task OnCommitEditOrder()
        {
            if (this.Mode == PresentationMode.Read)
                return;

            if (!this.draftVendor.Validate(out IList<string> errors))
            {
                await App.Current.MainPage.DisplayAlert("Validation failed", "Please check your data and try again" + Environment.NewLine + String.Join(Environment.NewLine, errors), "OK");
                return;
            }

            var updatedVendor = await this.erpService.SaveVendorAsync(this.draftVendor);

            this.DraftVendor = null;
            this.targetVendor = null;
            this.Vendor = updatedVendor;
            this.Mode = PresentationMode.Read;

            this.UpdateTitle();

            if (Device.Idiom != TargetIdiom.Phone)
                await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(VendorsViewModel)));
        }

        private void InitializeEditData(Vendor vendor)
        {
        }
    }
}
