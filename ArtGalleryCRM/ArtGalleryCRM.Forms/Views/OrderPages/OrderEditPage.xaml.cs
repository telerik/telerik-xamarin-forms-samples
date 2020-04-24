using System;
using System.Text;
using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Views.OrderPages
{
    public partial class OrderEditPage : IDataFormView
    {
        public OrderEditPage()
        {
            this.InitializeComponent();

            this.ViewModel.DataFormView = this;

            this.ViewModel.SelectedOrder = new Order();
        }

        public OrderEditPage(BaseDataObject dataObject)
        {
            this.InitializeComponent();

            this.ViewModel.DataFormView = this;

            if (dataObject != null)
            {
                switch (dataObject)
                {
                    case Product product:
                        this.ViewModel.SelectedOrder = new Order { ProductId = product.Id };
                        break;
                    case Customer customer:
                        this.ViewModel.SelectedOrder = new Order { CustomerId = customer.Id };
                        break;
                    case Employee employee:
                        this.ViewModel.SelectedOrder = new Order { EmployeeId = employee.Id };
                        break;
                    case Order order:
                        this.ViewModel.IsNewOrder = false;
                        this.ViewModel.SelectedOrder = order;
                        break;
                }
            }
        }

        public void CommitChanges()
        {
            var hasErrors = false;
            StringBuilder errors = new StringBuilder();

            if (Convert.ToInt32(this.QuantityNumericInput.Value) == 0)
            {
                hasErrors = true;
                errors.AppendLine("- Quantity");
            }

            if (string.IsNullOrEmpty(this.StreetEntry.Text))
            {
                hasErrors = true;
                errors.AppendLine("- Street");
            }

            if (string.IsNullOrEmpty(this.CityEntry.Text))
            {
                hasErrors = true;
                errors.AppendLine("- City");
            }

            if (string.IsNullOrEmpty(this.StateEntry.Text))
            {
                hasErrors = true;
                errors.AppendLine("- State");
            }

            if (string.IsNullOrEmpty(this.ZipCodeEntry.Text))
            {
                hasErrors = true;
                errors.AppendLine("- ZIP Code");
            }

            if (string.IsNullOrEmpty(this.CountryEntry.Text))
            {
                hasErrors = true;
                errors.AppendLine("- Country");
            }

            if (hasErrors)
            {
                Application.Current.MainPage.DisplayAlert("Please fill in the following missing fields:", errors.ToString(), "OK");
            }

            this.ViewModel.IsReadyForSave = !hasErrors;
        }
        
        public void ConfigureDataFormEditors()
        {
            // Not used for this page
        }
    }
}