using QSF.Views;
using System;
using Xamarin.Forms;

namespace QSF.Examples.CheckBoxControl.FirstLookExample
{
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            InitializeComponent();
        }

        private void RadButton_Clicked(object sender, System.EventArgs e)
        {
            this.relLayout.IsVisible = true;
            Device.StartTimer(TimeSpan.FromSeconds(2.0), () =>
            {
                this.relLayout.IsVisible = false;
                return false;
            });
        }
    }
}