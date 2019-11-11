using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.ThemingExample
{
    public class EventData : IAppointment
    {
        private const string timeFormat = "t";

        public EventData(DateTime startTime, DateTime endTime, string eventText, Color leadColor, Color itemColor, bool isEventAllDay = false)
        {
            this.Color = leadColor;
            this.EndDate = endTime;
            this.IsEventAllDay = isEventAllDay;
            this.ItemBackgroundColor = itemColor;
            this.LeadBorderColor = leadColor;
            this.StartDate = startTime;
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

        public Color Color { get; set; }

        public string Detail { get; set; }

        public DateTime EndDate { get; set; }

        public string EndTimeString
        {
            get
            {
                return this.EndDate.ToString(timeFormat);
            }
        }

        public bool IsAllDay { get; set; }

        public bool IsEventAllDay { get; set; }

        public Color ItemBackgroundColor { get; set; }

        public Color LeadBorderColor { get; set; }

        public DateTime StartDate { get; set; }

        public string StartTimeString
        {
            get
            {
                return this.StartDate.ToString(timeFormat);
            }
        }

        public string Title { get; set; }
    }
}
