using QSF.Services;
using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace QSF.Examples.DataFormControl.ReservationsExample
{
    public class ReservationsViewModel : ExampleViewModel
    {
        private readonly INavigationService navigationService;

        public ObservableCollection<Reservation> Reservations { get; private set; }
        public Reservation Reservation { get; private set; }
        public bool IsNewReservation { get; private set; }

        public ReservationsViewModel()
        {
            this.navigationService = DependencyService.Get<INavigationService>();

            this.Reservations = new ObservableCollection<Reservation>()
            {
                new Reservation()
                {
                    ReservationHolder = "Christian Heilmann",
                    HolderPhoneNumber = "359-55-1236",
                    ReservationDate = DateTime.Today,
                    ReservationTime = DateTime.Today.AddHours(20).AddMinutes(30),
                    GuestNumber = 2,
                    TableNumber = 5,
                    TableSection = 0,
                    OrderOrigin = OrderOrigin.Online
                },
                new Reservation()
                {
                    ReservationHolder = "Thomas Drake",
                    HolderPhoneNumber = "359-55-1237",
                    ReservationDate = DateTime.Today,
                    ReservationTime = DateTime.Today.AddHours(20).AddMinutes(30),
                    GuestNumber = 5,
                    TableNumber = 2,
                    TableSection = 0,
                    OrderOrigin = OrderOrigin.Online
                },
                new Reservation()
                {
                    ReservationHolder = "Aaron White",
                    HolderPhoneNumber = "359-55-1238",
                    ReservationDate = DateTime.Today,
                    ReservationTime = DateTime.Today.AddHours(20).AddMinutes(30),
                    GuestNumber = 4,
                    TableNumber = 4,
                    TableSection = 0,
                    OrderOrigin = OrderOrigin.Phone
                },
                new Reservation()
                {
                    ReservationHolder = "Rachel Nabors",
                    HolderPhoneNumber = "359-55-1239",
                    ReservationDate = DateTime.Today,
                    ReservationTime = DateTime.Today.AddHours(20).AddMinutes(30),
                    GuestNumber = 3,
                    TableNumber = 3,
                    TableSection = 0,
                    OrderOrigin = OrderOrigin.Inperson
                },
            };
        }

        public void AddReservation()
        {
            this.Reservation = new Reservation();
            this.IsNewReservation = true;

            this.navigationService.NavigateToAsync<DataFormViewModel>(this);
        }

        public void EditReservation(Reservation reservation)
        {
            this.Reservation = reservation;
            this.IsNewReservation = false;

            this.navigationService.NavigateToAsync<DataFormViewModel>(this);
        }
    }
}