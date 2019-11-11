using Xamarin.Forms;

namespace ErpApp.Pages.Products
{
    public partial class ProductsPhoneView : ContentView
	{
		public ProductsPhoneView()
		{
			InitializeComponent();
		}

        public void OpenPopup()
        {
            this.popup.IsOpen = true;
        }
    }
}