using QSF.Services.Toast;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.DateTimePickerControl.DateTimePickerExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateTimePickerView : ContentView
    {
        public DateTimePickerView()
        {
            InitializeComponent();

            this.BindingContext = new ViewModel();
        }

        private void ShowToastMessage(object sender, EventArgs e)
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Pick-up and drop-off date and time chosen.");
        }
    }
}