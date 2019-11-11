using Xamarin.Forms;

namespace QSF.Examples.BarcodeControl.PDF417Example
{
    public partial class PDF417ConfigurationView : ContentView
    {
        public PDF417ConfigurationView()
        {
            this.InitializeComponent();
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var viewModel = (PDF417ConfigurationViewModel)this.BindingContext;
            viewModel.SelectedDate = e.NewDate;
        }
    }
}