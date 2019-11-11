using Xamarin.Forms;

namespace ErpApp.Pages.Vendors
{
    public partial class VendorsPhoneView : ContentView
	{
		public VendorsPhoneView()
		{
			InitializeComponent();
		}

        public void OpenPopup()
        {
            this.popup.IsOpen = true;
        }
    }
}