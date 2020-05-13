using QSF.Services.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private void AddToBasketClicked(object sender, EventArgs e)
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Added to basket");
        }
    }
}