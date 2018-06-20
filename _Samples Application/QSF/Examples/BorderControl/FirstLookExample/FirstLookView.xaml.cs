using System;
using Xamarin.Forms;

namespace QSF.Examples.BorderControl.FirstLookExample
{
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            InitializeComponent();
        }

        private void ConnectButtonTapped(object sender, EventArgs args)
        {
            this.awatingApprovalLabel.IsVisible = true;
            this.connectButon.TextColor = Color.FromHex("#f1b3aa");
        }
    }
}
