using Xamarin.Forms;

namespace QSF.Examples.SignaturePadControl.ConfigurationExample
{
    public partial class ConfigurationView : ContentView
    {
        private ConfigurationViewModel viewModel;

        public ConfigurationView()
        {
            InitializeComponent();
            this.viewModel = new ConfigurationViewModel();
            this.BindingContext = this.viewModel;
        }

        private void BorderThicknessSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var thickness = (int)e.NewValue;
            this.viewModel.BorderThickness = new Thickness(thickness);
        }

        private void signaturePad_StrokeStarted(object sender, System.EventArgs e)
        {
            this.clearButton.IsVisible = true;
        }

        private void signaturePad_Cleared(object sender, System.EventArgs e)
        {
            this.clearButton.IsVisible = false;
        }
    }
}