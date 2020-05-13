using QSF.Examples.TimePickerControl.Models;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.TimePickerControl.CustomizationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomizationView : ContentView, INotifyPropertyChanged
    {
        public CustomizationView()
        {
            InitializeComponent();
        }

        private void ScrollItemIntoViewClicked(object sender, EventArgs e)
        {
            ViewModel vm = this.BindingContext as ViewModel;
            Alarm item = vm.Alarms.Last();
            this.listView.ScrollItemIntoView(item);
        }
    }
}