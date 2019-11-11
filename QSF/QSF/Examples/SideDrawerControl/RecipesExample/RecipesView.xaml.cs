using System;
using Xamarin.Forms;

namespace QSF.Examples.SideDrawerControl.RecipesExample
{
    public partial class RecipesView : ContentView
    {
        public RecipesView()
        {
            this.InitializeComponent();
        }

        private void OnMenuTapped(object sender, EventArgs eventArgs)
        {
            this.drawer.IsOpen = !this.drawer.IsOpen;
        }
    }
}