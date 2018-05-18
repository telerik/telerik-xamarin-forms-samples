using System.Threading.Tasks;
using QSF.Services;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.DataFormControl.ReservationsExample
{
    public class DataFormViewModel : ExampleViewModel
    {
        private readonly INavigationService navigationService;
        private ReservationsViewModel viewModel;

        public Reservation FormSource { get; set; }
        public string PageTitle { get; set; }

        public DataFormViewModel()
        {
            this.navigationService = DependencyService.Get<INavigationService>();
        }

        protected override Task InitializeAsyncOverride(object parameter)
        {
            this.viewModel = (ReservationsViewModel)parameter;
            this.FormSource = this.viewModel.Reservation;

            if (this.viewModel.IsNewReservation)
            {
                this.PageTitle = "New Reservation";
            }
            else
            {
                this.PageTitle = "Edit Reservation";
            }

            return Task.FromResult(true);
        }

        public void CommitReservation()
        {
            if (this.viewModel.IsNewReservation)
            {
                this.viewModel.Reservations.Add(this.FormSource);
            }

            this.navigationService.NavigateBackAsync();
        }

        public void CancelReservation()
        {
            if (!this.viewModel.IsNewReservation)
            {
                this.viewModel.Reservations.Remove(this.FormSource);
            }

            this.navigationService.NavigateBackAsync();
        }

        public void CancelEditing()
        {
            this.navigationService.NavigateBackAsync();
        }
    }
}