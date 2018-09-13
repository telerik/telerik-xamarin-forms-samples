using System;
using System.Collections.ObjectModel;
using System.Linq;
using QSF.ViewModels;
using Telerik.XamarinForms.Input;

namespace QSF.Examples.CalendarControl.MultiDayViewAppointmentsExample
{
    public class MultiDayViewAppointmentsViewModel : ExampleViewModel
    {
        public MultiDayViewAppointmentsViewModel()
        {
            this.Appointments = AppointmentsGenerator.GenerateAppointments();
        }

        public ObservableCollection<Appointment> Appointments { get; }

        public void NavigateToAddAppointmentPage()
        {
            var addAppointmentViewModel = new AddAppointmentViewModel(this.Appointments);
            this.NavigationService.NavigateToConfigurationAsync<AddAppointmentViewModel>(addAppointmentViewModel);
        }

        public void NavigateToAddAppointmentPage(DateTime startTime, DateTime endTime)
        {
            var addAppointmentViewModel = new AddAppointmentViewModel(this.Appointments, startTime, endTime);
            this.NavigationService.NavigateToConfigurationAsync<AddAppointmentViewModel>(addAppointmentViewModel);
        }

        public void NavigateToReadAppointmentPage(Appointment appointment)
        {
            var readAppointmentViewModel = new ReadAppointmentViewModel(appointment, this.Appointments);
            this.NavigationService.NavigateToConfigurationAsync<ReadAppointmentViewModel>(readAppointmentViewModel);
        }
    }
}
