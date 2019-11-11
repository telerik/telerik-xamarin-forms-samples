using System;
using Input = Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.DayViewExample
{
    public class Appointment : Input.Appointment
    {
        public Appointment()
        {

        }

        public Appointment(DateTime start, DateTime end, string title, string detail, Color color, bool isAllDay = false)
        {
            this.StartDate = start;
            this.EndDate = end;
            this.Title = title;
            this.Detail = detail;
            this.Color = color;
            this.IsAllDay = isAllDay;
        }
    }
}
