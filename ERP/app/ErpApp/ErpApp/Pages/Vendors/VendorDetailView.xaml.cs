using Xamarin.Forms;

namespace ErpApp.Pages.Vendors
{
    public partial class VendorDetailView : ContentView, IPopupHost
    {
        public VendorDetailView()
        {
            InitializeComponent();
        }

        public void ClosePopup()
        {
            this.popup.IsOpen = false;
        }

        public void OpenPopup()
        {
            this.popup.IsOpen = true;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            this.ClosePopup();
        }
    }
}
