using System;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewAppointmentsExample
{
    public class Appointment : NotifyPropertyChangedBase, IAppointment
    {
        private string title;
        private string detail;
        private Color color;
        private bool isAllDay;
        private DateTime startDate;
        private DateTime endDate;

        public Appointment(DateTime start, DateTime end, string title, Color color, bool isAllDay = false)
        {
            this.Id = Guid.NewGuid();
            this.StartDate = start;
            this.EndDate = end;
            this.Title = title;
            this.Color = color;
            this.IsAllDay = isAllDay;
        }

        public Guid Id { get; set; }

        public string Title
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
    }
}
