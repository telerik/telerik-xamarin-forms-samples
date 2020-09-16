using QSF.ViewModels;
using System.Threading.Tasks;

namespace QSF.Examples.BarcodeControl.PDF417Example
{
    public class PDF417ViewModel : ExampleViewModel
    {
        private PDF417ConfigurationViewModel configurationViewModel;
        private string value;

        public PDF417ViewModel()
        {
            this.configurationViewModel = new PDF417ConfigurationViewModel();
        }

        public override bool HasConfiguration
        {
            get
            {
                return true;
            }
        }

        public override bool IsPopupHintOpen => true;

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.OnPropertyChanged();
                }
            }
        }

        protected override Task NavigateToConfigurationOverride()
        {
            return this.NavigationService.NavigateToConfigurationAsync(this.configurationViewModel);
        }

        internal override void OnAppearing()
        {
            base.OnAppearing();

            this.ApplyChanges();
        }

        private void ApplyChanges()
        {
            this.Value = string.Join(" ", 
                this.configurationViewModel.OrderNumber,
                this.configurationViewModel.SelectedCountry,
                this.configurationViewModel.City,
                this.configurationViewModel.Address,
                this.configurationViewModel.SelectedDate.ToString(),
                this.configurationViewModel.FullName,
                this.configurationViewModel.PhoneNumber);
        }
    }
}
