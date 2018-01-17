using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.DayViewExample
{
    public class Appointment : IAppointment
    {
        public Appointment(DateTime start, DateTime end, string title, string detail, Color color, bool isAllDay = false)
        {
            this.StartDate = start;
            this.EndDate = end;
            this.Title = title;
            this.Detail = detail;
            this.Color = color;
            this.IsAllDay = isAllDay;
        }

        public Color Color { get; set; }

        public string Detail { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsAllDay { get; set; }

        public DateTime StartDate { get; set; }

        public string Title { get; set; }
    }
}
