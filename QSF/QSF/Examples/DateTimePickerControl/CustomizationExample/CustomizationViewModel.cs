using QSF.Services.Toast;
using QSF.ViewModels;
using System;
using Xamarin.Forms;

namespace QSF.Examples.DateTimePickerControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        private DateTime? dropOffDate;
        private DateTime? pickUpDate;

        public CustomizationViewModel()
        {
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
