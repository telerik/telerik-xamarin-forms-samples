using QSF.Services.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.DateTimePickerControl.DatePickerExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePickerView : ContentView
    {
        public DatePickerView()
        {
            InitializeComponent();
        }

        private void ShowToastMessage(object sender, EventArgs e)
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Hotel booked");
        }
    }
}