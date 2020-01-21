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
            var hasErrors = false;

            if (Convert.ToInt32(this.QuantityNumericInput.Value) == 0)
            {
                hasErrors = true;
                Application.Current.MainPage.DisplayAlert("No Quantity", "You need to select a quantity greater than 0.", "OK");
            }

            if (string.IsNullOrEmpty(this.StreetEntry.Text))
            {
                hasErrors = true;
                Application.Current.MainPage.DisplayAlert("Missing Street", "You need to enter a value for Street.", "OK");
            }

            if (string.IsNullOrEmpty(this.CityEntry.Text))
            {
                hasErrors = true;
                Application.Current.MainPage.DisplayAlert("Missing City", "You need to enter a value for City.", "OK");
            }

            if (string.IsNullOrEmpty(this.StateEntry.Text))
            {
                hasErrors = true;
                Application.Current.MainPage.DisplayAlert("Missing State", "You need to enter a value for State.", "OK");
            }

            if (string.IsNullOrEmpty(this.ZipCodeEntry.Text))
            {
                hasErrors = true;
                Application.Current.MainPage.DisplayAlert("Missing ZIP Code", "You need to enter a value for ZIP Code.", "OK");
            }

            if (string.IsNullOrEmpty(this.CountryEntry.Text))
            {
                hasErrors = true;
                Application.Current.MainPage.DisplayAlert("Missing Country", "You need to enter a value for Country.", "OK");
            }

            this.ViewModel.IsReadyForSave = !hasErrors;
        }
        
        public void ConfigureDataFormEditors()
        {
            // Not used for this page
        }
    }
}