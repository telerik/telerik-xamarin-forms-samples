using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using tagit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tagit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerPage : ContentPage
    {
        public PickerPage()
        {
            InitializeComponent();

            this.BindingContext = App.ViewModel.Picker;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            App.ViewModel.Processing.InitializeCurrentImages();
        }
    }
}