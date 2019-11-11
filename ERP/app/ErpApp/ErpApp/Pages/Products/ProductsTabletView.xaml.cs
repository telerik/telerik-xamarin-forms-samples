using System;
using Xamarin.Forms;

namespace ErpApp.Pages.Products
{
    public partial class ProductsTabletView : ContentView
	{
		public ProductsTabletView()
		{
			InitializeComponent();
		}

        private void ShowPopup(object sender, EventArgs e)
        {
            popup.IsOpen = true;
        }
    }
}