using System;
using Xamarin.Forms;

namespace QSF.Examples.ListViewControl.ReorderExample
{
    public partial class ReorderView : ContentView
    {
        public ReorderView()
        {
            this.InitializeComponent();
        }

        private void OnButtonClicked(object sender, EventArgs eventArgs)
        {
            this.list.EndItemSwipe();
        }
    }
}
