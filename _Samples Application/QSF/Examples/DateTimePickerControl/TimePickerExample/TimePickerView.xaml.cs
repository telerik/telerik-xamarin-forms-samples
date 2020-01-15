using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.DateTimePickerControl.TimePickerExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimePickerView : ContentView, INotifyPropertyChanged
    {
        private ViewModel vm;
        public TimePickerView()
        {
            InitializeComponent();
            this.vm = new ViewModel();
            this.BindingContext = this.vm;
        }

        private void ScrollItemIntoViewClicked(object sender, EventArgs e)
        {
            var item = this.vm.Alarms[this.vm.Alarms.Count - 1];
            this.listView.ScrollItemIntoView(item);
        }
    }
}