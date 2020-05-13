using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.TimePickerControl.ConfigurationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationView : ContentView, INotifyPropertyChanged
    {
        public ConfigurationView()
        {
            InitializeComponent();
        }

        private void ScrollItemIntoViewClicked(object sender, EventArgs e)
        {
            var vm = (ConfigurationViewModel)this.BindingContext;
            var item = vm.Alarms[vm.Alarms.Count - 1];
            this.listView.ScrollItemIntoView(item);
        }
    }
}