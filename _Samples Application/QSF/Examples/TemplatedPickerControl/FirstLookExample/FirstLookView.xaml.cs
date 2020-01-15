using QSF.Services.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.TemplatedPickerControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            InitializeComponent();

            this.BindingContext = new ViewModel();
        }

        private void ShowToastMessage(object sender, EventArgs e)
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Flight Origin & Destination Chosen");
        }
    }
}