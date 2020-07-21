using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using ErpApp.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace ErpApp.ViewModels
{
    public class ProductDetailViewModel : MvxViewModel<PresentationContext<string>>
    {
        public ProductDetailViewModel(Services.IErpService erpService, IMvxNavigationService navigationService)
        {
            this.erpService = erpService;
            this.navigationService = navigationService;
            this.BeginEditCommand = new Command(OnBeginEditProduct);
            this.CommitCommand = new MvxAsyncCommand(OnCommitEditOrder);
            this.DeleteCommand = new MvxAsyncCommand(OnDeleteProduct);
            this.CancelCommand = new MvxAsyncCommand(OnCancel);
            this.SelectProductColorCommand = new Command<RadBrush>(OnSelectProductColor);
        }

        private Services.IErpService erpService;
        private IMvxNavigationService navigationService;
        private Product targetProduct, draftProduct;
        private string targetProductId;
        private PresentationMode mode;
        private int selectedBrushIndex;
        private string title;
        private List<RadBrush> availableBrushes;

        public List<RadBrush> AvailableBrushes
        {
            get => availableBrushes;
            private set => SetProperty(ref availableBrushes, value);
        }

        public Product Product
        {
            get => this.targetProduct;
            private set
            {
                if (SetProperty(ref this.targetProduct, value))
                {
                    RaisePropertyChanged(() => IsEditing);
                    RaisePropertyChanged(() => IsReading);
                }
            }
        }

        public Product DraftProduct
        {
            get => this.draftProduct;
            private set
            {
                if (SetProperty(ref this.draftProduct, value))
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

        public bool IsReading => this.targetProduct != null && this.mode == PresentationMode.Read;
        public bool IsEditing => this.draftProduct != null &&
            (this.mode == PresentationMode.Edit || this.mode == PresentationMode.Create);

        public int SelectedBrushIndex
        {
            get => selectedBrushIndex;
            private set => SetProperty(ref this.selectedBrushIndex, value);
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
        public ICommand SelectProductColorCommand { get; }

        public override void Prepare(PresentationContext<string> parameter)
        {
            this.targetProductId = parameter.Model;
            this.Mode = parameter.Mode;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            Product product = null;
            if (this.mode == PresentationMode.Create)
            {
                product = new Product();
                product.ImageURL = Constants.EmptyProductImage;
                product.CreatedAt = System.DateTime.Now;
                product.ChangeBrush(new RadSolidColorBrush(Color.Accent));
                this.DraftProduct = product;
                this.UpdateTitle();
                this.InitializeEditData(product);
                return;
            }

            if (!string.IsNullOrEmpty(this.targetProductId))
            {
                product = await this.erpService.GetProductAsync(this.targetProductId);
            }

            if (product == null)
                return;
                
            if (this.mode == PresentationMode.Edit)
            {
                this.targetProduct = product;
                var copy = product.Copy();
                this.DraftProduct = copy;
                this.InitializeEditData(copy);
            }
            else
            {
                this.Product = product;
            }
            this.UpdateTitle();
        }

        private void UpdateTitle()
        {
            switch (this.mode)
            {
                case PresentationMode.Read:
                    this.Title = this.targetProduct.Name;
                    break;
                case PresentationMode.Edit:
                    this.Title = $"Edit Product";
                    break;
                case PresentationMode.Create:
                    this.Title = "Add New Product";
                    break;
            }
        }

        private void OnBeginEditProduct()
        {
            if (!this.IsReading)
                return;

            var product = this.targetProduct.Copy();
            this.Mode = PresentationMode.Edit;
            UpdateTitle();
            this.InitializeEditData(product);
            this.DraftProduct = product;
        }

        private async Task OnDeleteProduct()
        {
            bool result = await App.Current.MainPage.DisplayAlert("Delete product", $"Are you sure you want to delete product {this.targetProduct.Name}?", "Yes", "No");
            if (!result)
                return;

            await this.erpService.RemoveProductAsync(this.targetProduct);
            if (Device.Idiom == TargetIdiom.Phone)
            {
                await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(ProductsViewModel)));
            }
        }

        private async Task OnCancel()
        {
            if (this.mode == PresentationMode.Read)
                return;

            this.DraftProduct = null;
            this.UpdateTitle();

            if (this.mode == PresentationMode.Edit)
            {
                this.Mode = PresentationMode.Read;
            }

            await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(ProductsViewModel)));
        }

        private async Task OnCommitEditOrder()
        {
            if (this.Mode == PresentationMode.Read)
                return;

            if (!this.draftProduct.Validate(out IList<string> errors))
            {
                await App.Current.MainPage.DisplayAlert("Validation failed", "Please check your data and try again" + Environment.NewLine + String.Join(Environment.NewLine, errors), "OK");
                return;
            }

            var updatedProduct = await this.erpService.SaveProductAsync(this.draftProduct);

            this.DraftProduct = null;
            this.targetProduct = null;
            this.Product = updatedProduct;
            this.Mode = PresentationMode.Read;

            this.UpdateTitle();

            if (Device.Idiom != TargetIdiom.Phone)
                await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(ProductsViewModel)));
        }

        private void InitializeEditData(Product product)
        {
            this.AvailableBrushes = new List<RadBrush>()
            {
                product.Brush,
                new RadSolidColorBrush(Color.FromHex("#3949AB")),
                new RadSolidColorBrush(Color.FromHex("#349EB8")),
                new RadSolidColorBrush(Color.FromHex("#FFAC3E")),
                new RadSolidColorBrush(Color.FromHex("#F85446"))
            };

            this.SelectedBrushIndex = this.availableBrushes.IndexOf(product.Brush);
        }

        private void OnSelectProductColor(RadBrush brush)
        {
            this.draftProduct.ChangeBrush(brush);
            this.SelectedBrushIndex = this.availableBrushes.IndexOf(brush);
        }
    }
}
