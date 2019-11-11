using Xamarin.Forms;

namespace ErpApp.Pages.Orders
{
    public partial class PhoneOrderDetail : ContentView, IPopupHost
    {
        public PhoneOrderDetail()
        {
            InitializeComponent();
        }

        public void OpenPopup()
        {
            this.popup.IsOpen = true;
        }

        public void ClosePopup()
        {
            this.popup.IsOpen = false;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            this.ClosePopup();
        }
    }
}