using QSF.Services.Toast;
using QSF.ViewModels;
using QSF.ViewModels.Common;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QSF.Examples.DatePickerControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private int guests;
        private DateTime? checkInDate;
        private DateTime? checkOutDate;
        private PickerConfigurationMenuViewModel datePickerConfigurationViewModel;
        private bool isHeaderVisible;
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
            this.guests = 1;
            this.BookHotelRoomCommand = new Command(this.BookHotelRoom, this.CanBookHotelRoom);
            this.CheckInDate = null;
            this.CheckOutDate = null;
            this.datePickerConfigurationViewModel = new PickerConfigurationMenuViewModel();
        }

        public DateTime? CheckInDate
        {
            get
            {
                return this.checkInDate;
            }
            set
            {
                if (this.checkInDate != value)
                {
                    this.checkInDate = value;
                    this.OnPropertyChanged();
                    this.OnCheckInDateChanged();
                }
            }
        }

        public DateTime? CheckOutDate
        {
            get
            {
                return this.checkOutDate;
            }
            set
            {
                if (this.checkOutDate != value)
                {
                    this.checkOutDate = value;
                    this.OnPropertyChanged();
                    this.OnCheckOutDateChanged();
                }
            }
        }

        public int Guests
        {
            get
            {
                return this.guests;
            }
            set
            {
                if (this.guests != value)
                {
                    this.guests = value;
                    this.OnPropertyChanged();
                    this.OnGuestsChanged();
                }
            }
        }

        public override bool HasConfiguration
        {
            get
            {
                return true;
            }
        }

        public override bool IsPopupHintOpen => true;

        public Command BookHotelRoomCommand { get; }

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

        private void OnCheckInDateChanged()
        {
            if (this.CheckOutDate < this.CheckInDate)
            {
                this.CheckOutDate = this.CheckInDate;
            }
            this.BookHotelRoomCommand.ChangeCanExecute();
        }

        private void OnCheckOutDateChanged()
        {
            if (this.CheckInDate > this.CheckOutDate)
            {
                this.CheckInDate = this.CheckOutDate;
            }
            this.BookHotelRoomCommand.ChangeCanExecute();
        }

        private void OnGuestsChanged()
        {
            this.BookHotelRoomCommand.ChangeCanExecute();
        }

        private void BookHotelRoom()
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Hotel booked");
            this.CheckInDate = null;
            this.CheckOutDate = null;
        }

        private bool CanBookHotelRoom()
        {
            return this.CheckInDate != null && this.CheckOutDate != null && this.Guests > 0;
        }

        internal override void OnAppearing()
        {
            base.OnAppearing();

            this.IsHeaderVisible = this.datePickerConfigurationViewModel.IsHeaderVisible;
            this.PopupHeaderBackgroundColor = this.datePickerConfigurationViewModel.PopupHeaderBackgroundColor.Color;
            this.PopupHeaderFontColor = this.datePickerConfigurationViewModel.PopupHeaderFontColor.Color;
            this.ConfirmationButtonText = this.datePickerConfigurationViewModel.ConfirmationButtonText;
            this.CancellationButtonText = this.datePickerConfigurationViewModel.CancellationButtonText;
            this.SelectedItemFontColor = this.datePickerConfigurationViewModel.SelectedItemFontColor.Color;
            this.SelectedItemFontSize = this.datePickerConfigurationViewModel.SelectedItemFontSize;
            this.SelectedItemFontAttribute = this.datePickerConfigurationViewModel.SelectedItemFontAttribute;
            this.SelectedItemBackgroundColor = this.datePickerConfigurationViewModel.SelectedItemBackgroundColor.Color;
            this.SpinnerItemFontSize = this.datePickerConfigurationViewModel.SpinnerItemFontSize;
            this.SpinnerItemFontAttribute = this.datePickerConfigurationViewModel.SpinnerItemFontAttribute;
            this.SpinnerItemFontColor = this.datePickerConfigurationViewModel.SpinnerItemFontColor.Color;
            this.SpinnerItemBackgroundColor = this.datePickerConfigurationViewModel.SpinnerItemBackgroundColor.Color;
            this.ConfirmationButtonBackgroundColor = this.datePickerConfigurationViewModel.ConfirmationButtonBackgroundColor.Color;
            this.CancellationButtonBackgroundColor = this.datePickerConfigurationViewModel.CancellationButtonBackgroundColor.Color;
        }

        protected override Task NavigateToConfigurationOverride()
        {
            return this.NavigationService.NavigateToConfigurationAsync(this.datePickerConfigurationViewModel);
        }
    }
}
