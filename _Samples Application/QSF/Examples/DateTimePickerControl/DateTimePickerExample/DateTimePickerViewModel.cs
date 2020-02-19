using QSF.ViewModels;
using System;

namespace QSF.Examples.DateTimePickerControl.DateTimePickerExample
{
    public class DateTimePickerViewModel : ExampleViewModel
    {
        private DateTime dropOffDate = DateTime.Now.AddDays(6);
        private DateTime pickUpDate = DateTime.Now;

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

        public DateTime PickUpDate
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
            }
        }

        public DateTime DropOffDate
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
            }
        }

        private DateTime CoerceDropOffDate(DateTime newValue)
        {
            if (newValue >= this.pickUpDate)
            {
                return newValue;
            }
            return this.pickUpDate;
        }

        private DateTime CoercePickUpDate(DateTime newValue)
        {
            if (newValue >= this.dropOffDate)
            {
                this.DropOffDate = newValue;
                return this.dropOffDate;
            }
            return newValue;
        }
    }
}
