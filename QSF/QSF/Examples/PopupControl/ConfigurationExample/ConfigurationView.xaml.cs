using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ConfigurationExample
{
    public partial class ConfigurationView : ContentView
    {
        private ConfigurationViewModel viewModel;

        public ConfigurationView()
        {
            this.InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            if (this.BindingContext != null)
            {
                if (this.viewModel != null)
                {
                    this.viewModel.OpenPopup -= this.ViewModel_OpenPopup;
                    this.viewModel.ClosePopup -= this.ViewModel_ClosePopup;
                }

                this.viewModel = this.BindingContext as ConfigurationViewModel;

                if (this.viewModel != null)
                {
                    this.viewModel.OpenPopup += this.ViewModel_OpenPopup;
                    this.viewModel.ClosePopup += this.ViewModel_ClosePopup;
                }
            }

            base.OnBindingContextChanged();
        }

        private void ViewModel_ClosePopup(object sender, System.EventArgs e)
        {
            this.popup.IsOpen = false;
        }

        private void ViewModel_OpenPopup(object sender, System.EventArgs e)
        {
            var isModal = this.popup.IsModal;
            this.popup.PlacementTarget = isModal ? null : this.button;
            this.popup.ContentTemplate = isModal ? (DataTemplate)this.Resources["PopupModalContentTemplate"] : (DataTemplate)this.Resources["PopupContentTemplate"];

            this.popup.IsOpen = true;
        }
    }
}