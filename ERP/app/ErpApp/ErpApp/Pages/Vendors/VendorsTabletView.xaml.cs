using System;
using Xamarin.Forms;

namespace ErpApp.Pages.Vendors
{
    public partial class VendorsTabletView : ContentView
	{
		public VendorsTabletView()
		{
			InitializeComponent();
		}

        private void ShowPopup(object sender, EventArgs e)
        {
            popup.IsOpen = true;
        }
    }
}