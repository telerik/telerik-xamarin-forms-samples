using System;
using Telerik.XamarinForms.Common;

namespace QSF.Examples.ConversationalUIControl.TravelAssistanceExample.Models
{
    public class FlightInfo : NotifyPropertyChangedBase
    {
        private string departureCity;
        private string arrivalCity;
        private string departureAirport;
        private string arrivalAirport;
        private DateTime departureDate;
        private DateTime arrivalDate;
        private string planeImageUrl;

        public string DepartureCity
        {
            get
            {
                return this.departureCity;
            }
            set
            {
                if (this.departureCity != value)
                {
                    this.departureCity = value;
                    this.OnPropertyChanged(nameof(this.DepartureCity));
                }
            }
        }

        public string ArrivalCity
        {
            get
            {
                return this.arrivalCity;
            }
            set
            {
                if (this.arrivalCity != value)
                {
                    this.arrivalCity = value;
                    this.OnPropertyChanged(nameof(this.ArrivalCity));
                }
            }
        }

        public string DepartureAirport
        {
            get
            {
                return this.departureAirport;
            }
            set
            {
                if (this.departureAirport != value)
                {
                    this.departureAirport = value;
                    this.OnPropertyChanged(nameof(this.DepartureAirport));
                }
            }
        }

        public string ArrivalAirport
        {
            get
            {
                return this.arrivalAirport;
            }
            set
            {
                if (this.arrivalAirport != value)
                {
                    this.arrivalAirport = value;
                    this.OnPropertyChanged(nameof(this.ArrivalAirport));
                }
            }
        }

        public DateTime DepartureDate
        {
            get
            {
                return this.departureDate;
            }
            set
            {
                if (this.departureDate != value)
                {
                    this.departureDate = value;
                    this.OnPropertyChanged(nameof(this.DepartureDate));
                }
            }
        }

        public DateTime ArrivalDate
        {
            get
            {
                return this.arrivalDate;
            }
            set
            {
                if (this.arrivalDate != value)
                {
                    this.arrivalDate = value;
                    this.OnPropertyChanged(nameof(this.ArrivalDate));
                }
            }
        }

        public string PlaneImageUrl
        {
            get
            {
                return this.planeImageUrl;
            }
            set
            {
                if (this.planeImageUrl != value)
                {
                    this.planeImageUrl = value;
                    this.OnPropertyChanged(nameof(this.PlaneImageUrl));
                }
            }
        }
    }
}