using System;
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
            if (!this.StreetEntry.IsInputAccepted ||
                !this.CityEntry.IsInputAccepted ||
                !this.StateEntry.IsInputAccepted ||
                !this.ZipCodeEntry.IsInputAccepted ||
                !this.CountryEntry.IsInputAccepted)
            {
                this.ViewModel.IsReadyForSave = false;
            }
            else if (Convert.ToInt32(this.QuantityNumericInput.Value) == 0)
            {
                Application.Current.MainPage.DisplayAlert("No Quantity", "You need to select a quantity greater than 0.", "OK");

                this.ViewModel.IsReadyForSave = false;
            }
            else
            {
                this.ViewModel.IsReadyForSave = true;
            }
        }
        
        public void ConfigureDataFormEditors()
        {
            // Not used for this page
        }
    }
}