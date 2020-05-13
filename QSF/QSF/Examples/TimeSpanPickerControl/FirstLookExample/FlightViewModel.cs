using System;
using QSF.ViewModels;

namespace QSF.Examples.TimeSpanPickerControl.FirstLookExample
{
    public class FlightViewModel : BindableBase
    {
        private TimeSpan flightDuration;
        private double ticketPrice;
        private string ticketImage;

        public TimeSpan FlightDuration
        {
            get
            {
                return this.flightDuration;
            }
            set
            {
                if (this.flightDuration != value)
                {
                    this.flightDuration = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double TicketPrice
        {
            get
            {
                return this.ticketPrice;
            }
            set
            {
                if (this.ticketPrice != value)
                {
                    this.ticketPrice = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string TicketImage
        {
            get
            {
                return this.ticketImage;
            }
            set
            {
                if (this.ticketImage != value)
                {
                    this.ticketImage = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
