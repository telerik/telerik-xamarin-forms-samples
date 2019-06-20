using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MonthViewExample
{
    public class EventData : Appointment
    {
        private const string timeFormat = "t";

        public EventData()
        {
        }

        public EventData(DateTime startTime, DateTime endTime, string eventText, Color leadColor, Color itemColor, bool isEventAllDay = false)
        {
            this.Color = leadColor;
            this.EndDate = endTime;
            this.IsEventAllDay = isEventAllDay;
            this.ItemBackgroundColor = itemColor;
            this.LeadBorderColor = leadColor;
            this.StartDate = startTime;
            this.StartDateOnly = startTime.Date;
            this.Title = eventText;

            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.UWP)
            {
                this.AllDayString = "ALL DAY";
            }
            else
            {
                this.AllDayString = "All Day";
            }
        }

        public string AllDayString { get; }

        public string EndTimeString
        {
            get
            {
                return this.EndDate.ToString(timeFormat);
            }
        }

        public bool IsEventAllDay { get; set; }

        public Color ItemBackgroundColor { get; set; }

        public Color LeadBorderColor { get; set; }

        public DateTime StartDateOnly { get; set; }

        public string StartTimeString
        {
            get
            {
                return this.StartDate.ToString(timeFormat);
            }
        }
    }
}
