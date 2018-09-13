using System;
using System.Collections.Generic;
using System.Windows.Input;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewAppointmentsExample
{
    public class AddAppointmentViewModel : ConfigurationViewModel
    {
        private string title;
        private string detail;
        private Color color;
        private bool isAllDay;
        private DateTime startDate;
        private DateTime endDate;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private List<Color> appointmentColors;
        private ICollection<Appointment> appointments;

        public AddAppointmentViewModel()
        {
            this.Title = "Add Appointment";
            
            this.AppointmentColors = AppointmentsGenerator.AppointmentColors;
            this.CancelCommand = new Command(this.GoBack);
            this.AddCommand = new Command(this.Add);

            this.Color = this.AppointmentColors[0];

            this.PropertyChanged += HandlePropertyChanged;
        }

        public AddAppointmentViewModel(ICollection<Appointment> appointments) : this()
        {
            this.appointments = appointments;

            var dateTimeNow = DateTime.Now;
            var startDate = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day, dateTimeNow.Hour, 0, 0);
            var endDate = startDate.AddHours(1);

            this.EndTime = endDate.TimeOfDay;
            this.StartTime = startDate.TimeOfDay;

            this.EndDate = endDate;
            this.StartDate = startDate;
        }

        public AddAppointmentViewModel(ICollection<Appointment> appointments, DateTime startDate, DateTime endDate) : this(appointments)
        {
            this.StartTime = startDate.TimeOfDay;
            this.EndTime = endDate.TimeOfDay;

            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public ICommand CancelCommand { get; }
        public ICommand AddCommand { get; }

        public string AppointmentTitle
        {
            get
            {
                return this.title;
            }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Detail
        {
            get
            {
                return this.detail;
            }
            set
            {
                if (this.detail != value)
                {
                    this.detail = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsAllDay
        {
            get
            {
                return this.isAllDay;
            }
            set
            {
                if (this.isAllDay != value)
                {
                    this.isAllDay = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                if (this.startDate != value)
                {
                    this.startDate = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                if (this.endDate != value)
                {
                    this.endDate = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public TimeSpan StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                if (this.startTime != value)
                {
                    if (value >= this.EndTime)
                    {
                        this.EndTime = AddHoursToTimeSpan(value, 1);
                    }

                    this.startTime = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public TimeSpan EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                if (this.endTime != value)
                {
                    if (value <= this.StartTime)
                    {
                        this.StartTime = AddHoursToTimeSpan(value, -1);
                    }

                    this.endTime = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public List<Color> AppointmentColors
        {
            get
            {
                return this.appointmentColors;
            }
            set
            {
                if (this.appointmentColors != value)
                {
                    this.appointmentColors = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string propertyName = e.PropertyName;

            if (propertyName.Equals(nameof(this.StartTime)))
            {
                this.StartDate = SetTime(this.StartDate, this.StartTime);
            }
            else if (propertyName.Equals(nameof(this.EndTime)))
            {
                this.EndDate = SetTime(this.EndDate, this.EndTime);
            }
            else if (propertyName.Equals(nameof(this.StartDate)))
            {
                if (this.StartDate.TimeOfDay != this.StartTime)
                {
                    this.StartDate = SetTime(this.StartDate, this.StartTime);
                }

                this.SynchronizeEndDate();
            }
            else if (propertyName.Equals(nameof(this.EndDate)) && this.EndDate.TimeOfDay != this.EndTime)
            {
                this.EndDate = SetTime(this.EndDate, this.EndTime);
            }
        }

        private void SynchronizeEndDate()
        {
            if (this.StartDate.Date != this.EndDate.Date)
            {
                var endTime = this.EndDate.TimeOfDay;
                var endDate = this.StartDate;

                endDate = SetTime(endDate, endTime);
                this.EndDate = endDate;
            }
        }

        private void Add()
        {
            if (string.IsNullOrWhiteSpace(this.AppointmentTitle))
            {
                return;
            }

            var newAppointment = new Appointment(this.StartDate,
                                                 this.EndDate,
                                                 this.AppointmentTitle,
                                                 this.Color,
                                                 this.IsAllDay);
            this.appointments.Add(newAppointment);
            this.GoBack();
        }

        private void GoBack()
        {
            this.NavigationService.NavigateBackAsync();
        }

        private static DateTime SetTime(DateTime date, TimeSpan time)
        {
            DateTime syncedDate = date;
            if (date.Hour != time.Hours || date.Minute != time.Minutes)
            {
                syncedDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                syncedDate = syncedDate.Add(time);
            }

            return syncedDate;
        }

        private static TimeSpan AddHoursToTimeSpan(TimeSpan timeSpan, int hours)
        {
            var oneHour = TimeSpan.FromHours(hours);
            var minTime = TimeSpan.FromHours(0);
            var maxTime = TimeSpan.FromHours(23);
            maxTime = maxTime.Add(TimeSpan.FromMinutes(59));

            if (timeSpan + oneHour >= maxTime)
            {
                return maxTime;
            }
            else if (timeSpan + oneHour <= minTime)
            {
                return minTime;
            }

            return timeSpan.Add(oneHour);
        }
    }
}
