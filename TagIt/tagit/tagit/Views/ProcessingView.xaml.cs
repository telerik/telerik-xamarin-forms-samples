using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tagit.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tagit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProcessingView : ContentView
    {
        public ProcessingView()
        {
            InitializeComponent();
            
            App.ViewModel.Processing.ProcessingTabView = this.processingTabView;

            this.BindingContext = App.ViewModel.Processing;
        }
       
        private async void OnVisualized(object sender, EventArgs e)
        {
            if (sender is Grid control && !string.IsNullOrEmpty((control.BindingContext as ImageInformation)?.Caption))
            {
                if (Device.RuntimePlatform == Device.Android) await control.TranslateTo(0, 0, 500);
            }
        }
    }
}