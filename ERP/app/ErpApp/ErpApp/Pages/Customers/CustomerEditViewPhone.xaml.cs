using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ErpApp.Pages.Customers
{
    public partial class CustomerEditViewPhone : ContentView
    {
        public CustomerEditViewPhone()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (Device.RuntimePlatform == Device.iOS && propertyName == IsVisibleProperty.PropertyName && IsVisible)
            {
                InvalidateAddressListHeight();
            }
        }

        private void InvalidateAddressListHeight()
        {
            int addressCount = (this.BindingContext as ViewModels.CustomerDetailViewModel)?.DraftCustomerAddresses?.Count ?? 0;
            this.addressList.HeightRequest = addressCount * this.addressList.LayoutDefinition.ItemLength;
        }

        private void OnDeleteAddressClicked(object sender, System.EventArgs e)
        {
            this.InvalidateAddressListHeight();
        }

        private void OnCreateAddressClicked(object sender, System.EventArgs e)
        {
            this.InvalidateAddressListHeight();
        }
    }
}
