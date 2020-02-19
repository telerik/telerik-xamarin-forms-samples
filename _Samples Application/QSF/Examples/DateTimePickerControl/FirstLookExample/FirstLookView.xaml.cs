using QSF.Services.Toast;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.DateTimePickerControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            InitializeComponent();
        }

        private void OnCarRequested(object sender, EventArgs e)
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Request sent");
        }
    }
}