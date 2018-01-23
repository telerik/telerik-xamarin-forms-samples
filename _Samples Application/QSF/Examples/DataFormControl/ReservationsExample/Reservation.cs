using System;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Common.DataAnnotations;

namespace QSF.Examples.DataFormControl.ReservationsExample
{
    public class Reservation : NotifyPropertyChangedBase
    {
        private string reservationHolder;
        private string holderPhoneNumber;
        private DateTime reservationDate = DateTime.Today.AddHours(10);
        private DateTime reservationTime = DateTime.Today.AddHours(10);
        private double guestNumber;
        private int tableNumber;
        private Section tableSection;
        private OrderOrigin orderOrigin;

        [DisplayOptions(Position = 0, ColumnSpan = 2, PlaceholderText = "name", Group = "RESERVATION DETAILS")]
        [NonEmptyValidatorAttribute("Name is required!")]
        public string ReservationHolder
        {
            get
            {
                return this.reservationHolder;
            }
            set
            {
                if (this.reservationHolder != value)
                {
                    this.reservationHolder = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Position = 1, ColumnSpan = 2, PlaceholderText = "phone", Group = "RESERVATION DETAILS")]
        [NonEmptyValidatorAttribute("Phone is required!")]
        public string HolderPhoneNumber
        {
            get
            {
                return this.holderPhoneNumber;
            }
            set
            {
                if (this.holderPhoneNumber != value)
                {
                    this.holderPhoneNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Header = "Time", Position = 2, ColumnPosition = 1, PlaceholderText = "time", Group = "RESERVATION DETAILS")]
        [DisplayValueFormat(Time = "hh:mm tt")]
        [NativeConversionContext(DateTimeKind.Local)]
        public DateTime ReservationTime
        {
            get
            {
                return this.reservationTime;
            }
            set
            {
                if (this.reservationTime != value)
                {
                    this.reservationTime = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Header = "Date", Position = 2, PlaceholderText = "date", Group = "RESERVATION DETAILS")]
        [DisplayValueFormat(Date = "ddd - MM.dd")]
        public DateTime ReservationDate
        {
            get
            {
                return this.reservationDate;
            }
            set
            {
                if (this.reservationDate != value)
                {
                    this.reservationDate = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Header = "Guests", Position = 4, ColumnSpan = 2, PlaceholderText = "guests", Group = "RESERVATION DETAILS")]
        [NumericalRangeValidator(1, 17)]
        [DisplayValueFormat(Plural = "{0:G} guests", Single = "{0:G} guest", Zero = "")]
        public double GuestNumber
        {
            get
            {
                return this.guestNumber;
            }
            set
            {
                if (this.guestNumber != value)
                {
                    this.guestNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Header = "Section", Position = 4, Group = "TABLE DETAILS")]
        [Converter(typeof(SectionToStringConverter))]
        public Section TableSection
        {
            get
            {
                return this.tableSection;
            }
            set
            {
                if (this.tableSection != value)
                {
                    this.tableSection = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Header = "Table", Position = 4, ColumnPosition = 1, PlaceholderText = "table", Group = "TABLE DETAILS")]
        [DataSourceKey(nameof(TableNumber))]
        public int TableNumber
        {
            get
            {
                return this.tableNumber;
            }
            set
            {
                if (this.tableNumber != value)
                {
                    this.tableNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DataSourceKey("OrderOrigin")]
        [DisplayOptions(Header = "Origin", Position = 5, ColumnSpan = 2, PlaceholderText = "reservation origin", Group = "TABLE DETAILS")]
        [Converter(typeof(OrderOriginToStringConverter))]
        public OrderOrigin OrderOrigin
        {
            get
            {
                return this.orderOrigin;
            }
            set
            {
                if (this.orderOrigin != value)
                {
                    this.orderOrigin = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}