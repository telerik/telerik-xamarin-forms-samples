using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tagit.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = App.ViewModel.Settings;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //Make sure app resources, etc. are cleanly applied
            App.ViewModel.Settings.FinalizeAppTheme();
        }
    }
}