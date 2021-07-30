using Xamarin.Forms;

namespace QSF.Examples.ShadowControl.IntegrationExample
{
    public partial class PopupIntegrationView : ContentView
    {
        public PopupIntegrationView()
        {
            InitializeComponent();
        }

        void OpenPopupButtonClicked(object sender, System.EventArgs e)
        {
            this.popup.IsOpen = true;
        }

        void ClosePopupButtonClicked(object sender, System.EventArgs e)
        {
            this.popup.IsOpen = false;
        }
    }
}
