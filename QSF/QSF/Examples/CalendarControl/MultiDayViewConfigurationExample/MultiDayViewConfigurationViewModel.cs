using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QSF.Services;
using QSF.ViewModels;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Input.Calendar.Commands;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewConfigurationExample
{
    public class MultiDayViewConfigurationViewModel : ExampleViewModel, IMultiDayViewConfiguration
    {
        private IEnumerable<PersonViewModel> people;
        private IEnumerable<Appointment> appointments;
        private int visibleDays;
        private int peopleCount;
        private TimeSpan dayStartTime;
        private TimeSpan dayEndTime;
        private TimeSpan timelineInterval;
        private bool weekendsVisible;
        private bool currentTimeVisible;

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

        public int VisibleDays
        {
            get
            {
                return this.visibleDays;
            }
            set
            {
                if (this.visibleDays != value)
                {
                    this.visibleDays = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int PeopleCount
        {
            get
            {
                return this.peopleCount;
            }
            set
            {
                if (this.peopleCount != value)
                {
                    this.peopleCount = value;
                    this.OnPropertyChanged();
                    this.UpdateDataSource();
                }
            }
        }

        public TimeSpan DayStartTime
        {
            get
            {
                return this.dayStartTime;
            }
            set
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
            set
            {
                if (this.dayEndTime != value)
                {
                    this.dayEndTime = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public TimeSpan TimelineInterval
        {
            get
            {
                return this.timelineInterval;
            }
            set
            {
                if (this.timelineInterval != value)
                {
                    this.timelineInterval = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool WeekendsVisible
        {
            get
            {
                return this.weekendsVisible;
            }
            set
            {
                if (this.weekendsVisible != value)
                {
                    this.weekendsVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool CurrentTimeVisible
        {
            get
            {
                return this.currentTimeVisible;
            }
            set
            {
                if (this.currentTimeVisible != value)
                {
                    this.currentTimeVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public override bool HasConfiguration
        {
            get
            {
                return true;
            }
        }

        public ICommand AppointmentTappedCommand { get; private set; }
        public ICommand TimeSlotTappedCommand { get; private set; }

        public MultiDayViewConfigurationViewModel()
        {
            this.VisibleDays = 5;
            this.PeopleCount = 3;
            this.DayStartTime = TimeSpan.FromHours(8);
            this.DayEndTime = TimeSpan.FromHours(23);
            this.TimelineInterval = TimeSpan.FromHours(1);
            this.WeekendsVisible = true;
            this.CurrentTimeVisible = true;
            this.AppointmentTappedCommand = new Command<AppointmentTapCommandContext>(this.OnAppointmentTapped);
            this.TimeSlotTappedCommand = new Command<TimeSlotTapCommandContext>(this.OnTimeSlotTapped);
        }

        protected override Task NavigateToConfigurationOverride()
        {
            var viewModel = new MultiDayViewPropertiesViewModel(this);

            return this.NavigationService.NavigateToConfigurationAsync(viewModel);
        }

        private void UpdateDataSource()
        {
            var dataSource = DataProvider.GetData(DateTime.Today);

            this.People = dataSource
                .Take(this.PeopleCount)
                .ToArray();
            this.Appointments = dataSource
                .Take(this.PeopleCount)
                .SelectMany(person => person.Appointments)
                .ToArray();
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

        private void OnTimeSlotTapped(TimeSlotTapCommandContext context)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Start Time: {0}", context.StartTime);
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("End Time: {0}", context.EndTime);
            stringBuilder.AppendLine();

            var timeSlotMessage = stringBuilder.ToString();
            var messageService = DependencyService.Get<IMessageService>();

            messageService.ShowMessage("Time Slot", timeSlotMessage);
        }
    }
}
