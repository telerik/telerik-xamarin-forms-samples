using System.Collections.ObjectModel;
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

    }
}
