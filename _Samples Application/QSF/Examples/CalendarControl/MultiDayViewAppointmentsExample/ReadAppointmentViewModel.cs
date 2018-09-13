using System;
using System.Collections.Generic;
using System.Windows.Input;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewAppointmentsExample
{
	public class ReadAppointmentViewModel : ConfigurationViewModel
    {
        private ICollection<Appointment> appointments;

        public ReadAppointmentViewModel(Appointment appointment, ICollection<Appointment> appointments)
        {
            this.Title = "Appointment";

            this.Appointment = appointment;
            this.appointments = appointments;
            this.CancelCommand = new Command(this.GoBack);
            this.EditCommand = new Command(this.Edit);
            this.DeleteCommand = new Command(this.Delete);
        }

        public Appointment Appointment { get; set; }

        public ICommand CancelCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private void GoBack()
        {
            this.NavigationService.NavigateBackAsync();
        }

        private void Edit()
        {
            var editAppointmentViewModel = new EditAppointmentViewModel(this.Appointment, this.appointments);
            this.NavigationService.NavigateToConfigurationAsync<EditAppointmentViewModel>(editAppointmentViewModel);
        }

        private void Delete()
        {
            this.appointments.Remove(this.Appointment);
            this.NavigationService.NavigateBackAsync();
        }
    }
}
