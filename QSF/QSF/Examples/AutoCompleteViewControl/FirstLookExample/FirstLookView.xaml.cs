using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.AutoCompleteViewControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            InitializeComponent();
        }

        private void OnSendMailClicked(object sender, EventArgs e)
        {
            this.emailSent.IsVisible = true;
            Device.StartTimer(TimeSpan.FromSeconds(2.0), () =>
            {
                this.emailSent.IsVisible = false;
                this.ClearTokens();
                return false;
            });
        }

        private void OnCancelMailClicked(object sender, EventArgs e)
        {
            this.ClearTokens();
        }

        private void ClearTokens()
        {
            ((ObservableCollection<object>)this.toContact.Tokens).Clear();
            ((ObservableCollection<object>)this.ccBccContact.Tokens).Clear();
        }
    }
}