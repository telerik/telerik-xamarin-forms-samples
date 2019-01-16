using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using QSF.Services;
using QSF.ViewModels;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Input.Calendar.Commands;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewPeopleExample
{
    public class MultiDayViewPeopleViewModel : ExampleViewModel
    {
        private IEnumerable<PersonViewModel> people;
        private IEnumerable<Appointment> appointments;
        private TimeSpan dayStartTime;
        private TimeSpan dayEndTime;

        public IEnumerable<PersonViewModel> People
        {
            get
            {
                return this.people;
            }
            private set
            {
                if (this.people != value)
                {
                    this.people = value;
                    this.OnPropertyChanged();
                    this.UpdateTimeline();
                    this.UpdateAppointments();
                }
            }
        }

        public IEnumerable<Appointment> Appointments
        {
            get
            {
                return this.appointments;
            }
            private set
            {
                if (this.appointments != value)
                {
                    this.appointments = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public TimeSpan DayStartTime
        {
            get
            {
                return this.dayStartTime;
            }
            private set
            {
                if (this.dayStartTime != value)
                {
                    this.dayStartTime = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public TimeSpan DayEndTime
        {
            get
            {
                return this.dayEndTime;
            }
            private set
            {
                if (this.dayEndTime != value)
                {
                    this.dayEndTime = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand AppointmentTappedCommand { get; private set; }
        public ICommand PersonCheckedCommand { get; private set; }

        public MultiDayViewPeopleViewModel()
        {
            this.People = DataProvider.GetData(DateTime.Today);
            this.AppointmentTappedCommand = new Command<AppointmentTapCommandContext>(this.OnAppointmentTapped);
            this.PersonCheckedCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                   this.UpdateAppointments();
                });
            });
        }

        private void UpdateTimeline()
        {
            if (this.People != null)
            {
                var startHour = this.People
                    .SelectMany(person => person.Appointments)
                    .Where(appointment => !appointment.IsAllDay)
                    .Min(appointment => appointment.StartDate.Hour);
                var endHour = this.People
                    .SelectMany(person => person.Appointments)
                    .Where(appointment => !appointment.IsAllDay)
                    .Max(appointment => appointment.EndDate.Hour);

                this.DayStartTime = TimeSpan.FromHours(startHour);
                this.DayEndTime = TimeSpan.FromHours(endHour);
            }
            else
            {
                this.DayStartTime = TimeSpan.FromHours(0);
                this.DayEndTime = TimeSpan.FromHours(24);
            }
        }

        private void UpdateAppointments()
        {
            if (this.People != null)
            {
                this.Appointments = this.People
                    .Where(person => person.IsSelected)
                    .SelectMany(person => person.Appointments)
                    .ToArray();
            }
            else
            {
                this.Appointments = null;
            }
        }

        private void OnAppointmentTapped(AppointmentTapCommandContext context)
        {
            var appointment = context.Appointment;
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(appointment.Title);
            stringBuilder.AppendLine(appointment.Detail);

            var appointmentMessage = stringBuilder.ToString();
            var messageService = DependencyService.Get<IMessageService>();

            messageService.ShowMessage("Appointment", appointmentMessage);
        }
    }
}
