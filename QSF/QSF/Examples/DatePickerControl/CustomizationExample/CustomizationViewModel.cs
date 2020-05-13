using QSF.Services.Toast;
using QSF.ViewModels;
using System;
using Xamarin.Forms;

namespace QSF.Examples.DatePickerControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        private int guests;
        private DateTime? checkInDate;
        private DateTime? checkOutDate;

        public CustomizationViewModel()
        {
            this.BookHotelRoomCommand = new Command(this.BookHotelRoom, this.CanBookHotelRoom);
            this.guests = 1;
            this.CheckInDate = null;
            this.CheckOutDate = null;
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

        public Command BookHotelRoomCommand { get; }

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
    }
}
