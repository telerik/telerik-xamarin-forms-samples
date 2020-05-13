using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.TimeSpanPickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private readonly IEnumerable<FlightViewModel> availableFlights;
        private TimeSpan? flightDuration;
        private bool isFlightInfoVisible;
        private bool isFlightListVisible;
        private bool isFlightEmptyVisible;

        public FirstLookViewModel()
        {
            this.IsFlightInfoVisible = true;
            this.MaximumFlightTime = new TimeSpan(2, 23, 59, 00);
            this.ShowFlightsCommand = new Command(this.ShowFlights, this.IsFlightDurationSet);
            this.Flights = new ObservableCollection<FlightViewModel>();

            this.availableFlights = new[]
            {
                new FlightViewModel
                {
                    FlightDuration = new TimeSpan(0, 14, 0, 0),
                    TicketPrice = 505,
                    TicketImage = "Ticket_1.png"
                },
                new FlightViewModel
                {
                    FlightDuration = new TimeSpan(0, 17, 0, 0),
                    TicketPrice = 595,
                    TicketImage = "Ticket_2.png"
                },
                new FlightViewModel
                {
                    FlightDuration = new TimeSpan(0, 17, 40, 0),
                    TicketPrice = 634,
                    TicketImage = "Ticket_3.png"
                },
                new FlightViewModel
                {
                    FlightDuration = new TimeSpan(0, 11, 50, 0),
                    TicketPrice = 654,
                    TicketImage = "Ticket_4.png"
                },
                new FlightViewModel
                {
                    FlightDuration = new TimeSpan(0, 3, 0, 0),
                    TicketPrice = 1600,
                    TicketImage = "Ticket_5.png"
                },
                new FlightViewModel
                {
                    FlightDuration = new TimeSpan(0, 2, 0, 0),
                    TicketPrice = 1820,
                    TicketImage = "Ticket_6.png"
                },
                new FlightViewModel
                {
                    FlightDuration = new TimeSpan(0, 3, 0, 0),
                    TicketPrice = 890,
                    TicketImage = "Ticket_7.png"
                },
                new FlightViewModel
                {
                    FlightDuration = new TimeSpan(0, 2, 15, 0),
                    TicketPrice = 1650,
                    TicketImage = "Ticket_8.png"
                },
            };
        }

        public TimeSpan? FlightDuration
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
                    this.ShowFlightsCommand.ChangeCanExecute();
                }
            }
        }

        public bool IsFlightInfoVisible
        {
            get
            {
                return this.isFlightInfoVisible;
            }
            set
            {
                if (this.isFlightInfoVisible != value)
                {
                    this.isFlightInfoVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsFlightListVisible
        {
            get
            {
                return this.isFlightListVisible;
            }
            set
            {
                if (this.isFlightListVisible != value)
                {
                    this.isFlightListVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsFlightEmptyVisible
        {
            get
            {
                return this.isFlightEmptyVisible;
            }
            set
            {
                if (this.isFlightEmptyVisible != value)
                {
                    this.isFlightEmptyVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public TimeSpan MaximumFlightTime { get; }
        public Command ShowFlightsCommand { get; }
        public ObservableCollection<FlightViewModel> Flights { get; }

        private void ShowFlights()
        {
            var flightDuration = this.FlightDuration.GetValueOrDefault();
            var matchingFlights = this.availableFlights
                .Where(flight => flight.FlightDuration <= flightDuration);

            this.IsFlightInfoVisible = false;
            this.IsFlightListVisible = matchingFlights.Any();
            this.IsFlightEmptyVisible = !matchingFlights.Any();

            this.Flights.Clear();

            foreach (var flight in matchingFlights)
            {
                this.Flights.Add(flight);
            }
        }

        private bool IsFlightDurationSet()
        {
            return this.FlightDuration != null;
        }
    }
}
