using QSF.Services.Toast;
using QSF.ViewModels;
using QSF.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QSF.Examples.DateTimePickerControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private DateTime? dropOffDate;
        private DateTime? pickUpDate;
        private PickerConfigurationMenuViewModel dateTimePickerConfigurationViewModel;
        private bool isHeaderVisible;
        private bool isLooping;
        private Color popupHeaderBackgroundColor;
        private Color popupHeaderFontColor;
        private string confirmationButtonText;
        private string cancellationButtonText;
        private Color selectedItemFontColor;
        private int selectedItemFontSize;
        private FontAttributes selectedItemFontAttribute;
        private Color selectedItemBackgroundColor;
        private int spinnerItemFontSize;
        private FontAttributes spinnerItemFontAttribute;
        private Color spinnerItemFontColor;
        private Color spinnerItemBackgroundColor;
        private Color confirmationButtonBackgroundColor;
        private Color cancellationButtonBackgroundColor;

        public ConfigurationViewModel()
        {
            this.dateTimePickerConfigurationViewModel = new PickerConfigurationMenuViewModel();
            this.dateTimePickerConfigurationViewModel.IsHeaderVisible = false;
            this.dateTimePickerConfigurationViewModel.IsLooping = false;
            this.dateTimePickerConfigurationViewModel.IsLoopingPickerVisible = true;
            this.SelectDateCommand = new Command(this.SelectDate, this.CanExecuteSelectDate);
        }

        public DateTime StartDate
        {
            get
            {
                return DateTime.Today;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return DateTime.Today.AddYears(4);
            }
        }

        public DateTime? PickUpDate
        {
            get
            {
                return this.pickUpDate;
            }
            set
            {
                var coercedValue = this.CoercePickUpDate(value);
                this.pickUpDate = coercedValue;
                this.OnPropertyChanged();
                this.SelectDateCommand.ChangeCanExecute();
            }
        }

        public DateTime? DropOffDate
        {
            get
            {
                return this.dropOffDate;
            }
            set
            {
                var coercedValue = this.CoerceDropOffDate(value);
                this.dropOffDate = coercedValue;
                this.OnPropertyChanged();
                this.SelectDateCommand.ChangeCanExecute();
            }
        }

        public Command SelectDateCommand { get; }

        public override bool HasConfiguration
        {
            get
            {
                return true;
            }
        }

        public override bool IsPopupHintOpen => true;

        public bool IsHeaderVisible
        {
            get
            {
                return this.isHeaderVisible;
            }
            set
            {
                if (this.isHeaderVisible != value)
                {
                    this.isHeaderVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsLooping
        {
            get
            {
                return this.isLooping;
            }
            set
            {
                if (this.isLooping != value)
                {
                    this.isLooping = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color PopupHeaderBackgroundColor
        {
            get
            {
                return this.popupHeaderBackgroundColor;
            }
            set
            {
                if (this.popupHeaderBackgroundColor != value)
                {
                    this.popupHeaderBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color PopupHeaderFontColor
        {
            get
            {
                return this.popupHeaderFontColor;
            }
            set
            {
                if (this.popupHeaderFontColor != value)
                {
                    this.popupHeaderFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ConfirmationButtonText
        {
            get
            {
                return this.confirmationButtonText;
            }
            set
            {
                if (this.confirmationButtonText != value)
                {
                    this.confirmationButtonText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CancellationButtonText
        {
            get
            {
                return this.cancellationButtonText;
            }
            set
            {
                if (this.cancellationButtonText != value)
                {
                    this.cancellationButtonText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SelectedItemFontColor
        {
            get
            {
                return this.selectedItemFontColor;
            }
            set
            {
                if (this.selectedItemFontColor != value)
                {
                    this.selectedItemFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color ConfirmationButtonBackgroundColor
        {
            get
            {
                return this.confirmationButtonBackgroundColor;
            }
            set
            {
                if (this.confirmationButtonBackgroundColor != value)
                {
                    this.confirmationButtonBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color CancellationButtonBackgroundColor
        {
            get
            {
                return this.cancellationButtonBackgroundColor;
            }
            set
            {
                if (this.cancellationButtonBackgroundColor != value)
                {
                    this.cancellationButtonBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int SelectedItemFontSize
        {
            get
            {
                return this.selectedItemFontSize;
            }
            set
            {
                if (this.selectedItemFontSize != value)
                {
                    this.selectedItemFontSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FontAttributes SelectedItemFontAttribute
        {
            get
            {
                return this.selectedItemFontAttribute;
            }
            set
            {
                if (this.selectedItemFontAttribute != value)
                {
                    this.selectedItemFontAttribute = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SelectedItemBackgroundColor
        {
            get
            {
                return this.selectedItemBackgroundColor;
            }
            set
            {
                if (this.selectedItemBackgroundColor != value)
                {
                    this.selectedItemBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int SpinnerItemFontSize
        {
            get
            {
                return this.spinnerItemFontSize;
            }
            set
            {
                if (this.spinnerItemFontSize != value)
                {
                    this.spinnerItemFontSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FontAttributes SpinnerItemFontAttribute
        {
            get
            {
                return this.spinnerItemFontAttribute;
            }
            set
            {
                if (this.spinnerItemFontAttribute != value)
                {
                    this.spinnerItemFontAttribute = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SpinnerItemFontColor
        {
            get
            {
                return this.spinnerItemFontColor;
            }
            set
            {
                if (this.spinnerItemFontColor != value)
                {
                    this.spinnerItemFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SpinnerItemBackgroundColor
        {
            get
            {
                return this.spinnerItemBackgroundColor;
            }
            set
            {
                if (this.spinnerItemBackgroundColor != value)
                {
                    this.spinnerItemBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        internal override void OnAppearing()
        {
            base.OnAppearing();

            this.IsHeaderVisible = this.dateTimePickerConfigurationViewModel.IsHeaderVisible;
            this.IsLooping = this.dateTimePickerConfigurationViewModel.IsLooping;
            this.PopupHeaderBackgroundColor = this.dateTimePickerConfigurationViewModel.PopupHeaderBackgroundColor.Color;
            this.PopupHeaderFontColor = this.dateTimePickerConfigurationViewModel.PopupHeaderFontColor.Color;
            this.ConfirmationButtonText = this.dateTimePickerConfigurationViewModel.ConfirmationButtonText;
            this.CancellationButtonText = this.dateTimePickerConfigurationViewModel.CancellationButtonText;
            this.SelectedItemFontColor = this.dateTimePickerConfigurationViewModel.SelectedItemFontColor.Color;
            this.SelectedItemFontSize = this.dateTimePickerConfigurationViewModel.SelectedItemFontSize;
            this.SelectedItemFontAttribute = this.dateTimePickerConfigurationViewModel.SelectedItemFontAttribute;
            this.SelectedItemBackgroundColor = this.dateTimePickerConfigurationViewModel.SelectedItemBackgroundColor.Color;
            this.SpinnerItemFontSize = this.dateTimePickerConfigurationViewModel.SpinnerItemFontSize;
            this.SpinnerItemFontAttribute = this.dateTimePickerConfigurationViewModel.SpinnerItemFontAttribute;
            this.SpinnerItemFontColor = this.dateTimePickerConfigurationViewModel.SpinnerItemFontColor.Color;
            this.SpinnerItemBackgroundColor = this.dateTimePickerConfigurationViewModel.SpinnerItemBackgroundColor.Color;
            this.ConfirmationButtonBackgroundColor = this.dateTimePickerConfigurationViewModel.ConfirmationButtonBackgroundColor.Color;
            this.CancellationButtonBackgroundColor = this.dateTimePickerConfigurationViewModel.CancellationButtonBackgroundColor.Color;
        }

        protected override Task NavigateToConfigurationOverride()
        {
            return this.NavigationService.NavigateToConfigurationAsync(this.dateTimePickerConfigurationViewModel);
        }

        private DateTime? CoerceDropOffDate(DateTime? newValue)
        {
            if (newValue >= this.pickUpDate || newValue == null)
            {
                return newValue;
            }
            return this.pickUpDate;
        }

        private DateTime? CoercePickUpDate(DateTime? newValue)
        {
            if (newValue >= this.dropOffDate || newValue == null)
            {
                this.DropOffDate = newValue;
                return this.dropOffDate;
            }
            return newValue;
        }

        private void SelectDate()
        {
            var toastMessage = DependencyService.Get<IToastMessageService>();
            toastMessage.ShortAlert("Pick-up and drop-off date and time chosen.");
            this.PickUpDate = null;
            this.DropOffDate = null;
        }

        private bool CanExecuteSelectDate()
        {
            return PickUpDate != null && DropOffDate != null;
        }
    }
}
