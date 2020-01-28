using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using Telerik.XamarinForms.Input;

namespace ArtGalleryCRM.Forms.Views.ProductPages
{
    public partial class ProductEditPage : IDataFormView
    {
        public ProductEditPage()
        {
            this.InitializeComponent();

            this.ViewModel.DataFormView = this;
            this.ViewModel.IsNewProduct = true;
            this.ViewModel.SelectedProduct = new Product();
            this.ViewModel.DataFormView.ConfigureDataFormEditors();

            this.DataForm.Source = this.ViewModel.SelectedProduct;
        }

        public ProductEditPage(Product product)
        {
            this.InitializeComponent();

            this.ViewModel.DataFormView = this;
            this.ViewModel.SelectedProduct = product;
            this.ViewModel.DataFormView.ConfigureDataFormEditors();

            this.DataForm.Source = this.ViewModel.SelectedProduct;
        }

        public void ConfigureDataFormEditors()
        {
            this.DataForm.RegisterEditor(nameof(Product.Title), EditorType.TextEditor);
            this.DataForm.RegisterEditor(nameof(Product.Price), EditorType.DecimalEditor);
            this.DataForm.RegisterEditor(nameof(Product.InventoryCount), EditorType.IntegerEditor);
            this.DataForm.RegisterEditor(nameof(Product.Description), EditorType.TextEditor);
        }

        public void CommitChanges()
        {
            this.DataForm.CommitAll();
        }
    }
}