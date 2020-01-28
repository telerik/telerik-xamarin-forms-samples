using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using Telerik.XamarinForms.Input;

namespace ArtGalleryCRM.Forms.Views.CustomerPages
{
    public partial class CustomerEditPage : IDataFormView
    {
        public CustomerEditPage()
        {
            this.InitializeComponent();

            this.ViewModel.DataFormView = this;

            this.ViewModel.IsNewCustomer = true;
            this.ViewModel.SelectedCustomer = new Customer();
            this.ViewModel.DataFormView.ConfigureDataFormEditors();

            this.DataForm.Source = ViewModel.SelectedCustomer;
        }

        public CustomerEditPage(Customer customer)
        {
            this.InitializeComponent();

            this.ViewModel.DataFormView = this;
            this.ViewModel.SelectedCustomer = customer;
            this.ViewModel.DataFormView.ConfigureDataFormEditors();

            this.DataForm.Source = ViewModel.SelectedCustomer;
        }

        public void ConfigureDataFormEditors()
        {
            this.DataForm.RegisterEditor(nameof(Customer.Name), EditorType.TextEditor);
            this.DataForm.RegisterEditor(nameof(Customer.Street), EditorType.TextEditor);
            this.DataForm.RegisterEditor(nameof(Customer.City), EditorType.TextEditor);
            this.DataForm.RegisterEditor(nameof(Customer.State), EditorType.TextEditor);
            this.DataForm.RegisterEditor(nameof(Customer.Country), EditorType.TextEditor);
            this.DataForm.RegisterEditor(nameof(Customer.ZipCode), EditorType.TextEditor);
            this.DataForm.RegisterEditor(nameof(Customer.Notes), EditorType.TextEditor);
        }

        public void CommitChanges()
        {
            this.DataForm.CommitAll();
        }
    }
}