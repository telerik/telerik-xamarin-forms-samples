using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Views;
using Telerik.XamarinForms.Input.DataForm;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.ProductViewModels
{
    public class ProductEditViewModel : PageViewModelBase
    {
        private bool _isReadyToSave;
        private readonly Random _rand = new Random();
        private Product _selectedProduct;

        public ProductEditViewModel()
        {
            this.Title = "Edit Product";
            this.SetPhotoCommand = new Command(async () => await this.ShowModalAsync(new ImageEditorPage(this.SelectedProduct)));
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
            this.FormValidationCompleteCommand = new Command(this.FormValidationCompleted);
        }
        
        public bool IsNewProduct { get; set; }

        public Product SelectedProduct
        {
            get => this._selectedProduct;
            set => SetProperty(ref this._selectedProduct, value);
        }

        public ICommand SetPhotoCommand { get; set; }

        public ICommand ToolbarCommand { get; set; }

        public ICommand FormValidationCompleteCommand { get; set; }

        public IDataFormView DataFormView { get; set; }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (string.IsNullOrEmpty(this.SelectedProduct.PhotoUri))
            {
                this.SelectedProduct.PhotoUri = "art_placeholder.png";
            }
        }

        private void FormValidationCompleted(object obj)
        {
            if (obj is FormValidationCompletedEventArgs e)
            {
                this._isReadyToSave = e.IsValid;
            }
        }

        private async void ToolbarItemTapped(object obj)
        {
            switch (obj.ToString())
            {
                case "save":
                    // Commit the Editor's values for validation
                    this.DataFormView.CommitChanges();
                    
                    // If validation passes
                    if (this._isReadyToSave)
                    {
                        // Save the record
                        if (await this.UpdateDatabaseAsync())
                        {
                            await this.NavigateBackAsync();
                        }
                    }
                    break;
            }
        }

        public async Task<bool> UpdateDatabaseAsync()
        {
            await App.DisplayReadOnlyDemoAlert();
            return false;

            // ****** Update Record Logic ****** //

            //try
            //{
            //    this.IsBusy = true;

            //    if (string.IsNullOrEmpty(this.SelectedProduct.PhotoUri) || this.SelectedProduct.PhotoUri == "art_placeholder.png")
            //    {
            //        this.SetPhoto();
            //    }

            //    if (this.IsNewProduct)
            //    {
            //        await DependencyService.Get<IDataStore<Product>>().AddItemAsync(this.SelectedProduct);
            //    }
            //    else
            //    {
            //        await DependencyService.Get<IDataStore<Product>>().UpdateItemAsync(this.SelectedProduct);
            //    }

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    MessagingCenter.Send(new MessagingCenterAlert
            //    {
            //        Title = "Error",
            //        Message = $"There was a problem updating the database. Details:\r\n\n{ex.Message}",
            //        Cancel = "OK"
            //    }, "Alert");

            //    return false;
            //}
            //finally
            //{
            //    this.IsBusy = false;
            //}
        }
    }
}