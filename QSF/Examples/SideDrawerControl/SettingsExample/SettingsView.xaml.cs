using System;
using Xamarin.Forms;

namespace QSF.Examples.SideDrawerControl.SettingsExample
{
    public partial class SettingsView : ContentView
    {
        public SettingsView()
        {
            this.InitializeComponent();
        }

        private void OnMenuTapped(object sender, EventArgs eventArgs)
        {
            this.drawer.IsOpen = !this.drawer.IsOpen;
        }
    }
}
