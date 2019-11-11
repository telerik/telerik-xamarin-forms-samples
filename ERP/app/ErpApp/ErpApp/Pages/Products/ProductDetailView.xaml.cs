using Xamarin.Forms;

namespace ErpApp.Pages.Products
{
    public partial class ProductDetailView : ContentView, IPopupHost
    {
        public ProductDetailView()
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
    }
}
