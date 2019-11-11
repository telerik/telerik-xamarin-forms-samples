using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace tagit.Models
{
    /// <summary>
    ///     Used to display image creation information
    ///     in the Timeline view
    /// </summary>
    public class ImageCreationEvent : IAppointment
    {
        private const string timeFormat = "t";

        public ImageCreationEvent(DateTime startTime,
            DateTime endTime,
            string eventText,
            Color accentColor,
            bool isEventAllDay = false)
        {
            Color = accentColor;
            EndDate = endTime;
            StartDate = startTime;
            Title = eventText;
        }

        public string StartTimeString => StartDate.ToString(timeFormat);
        public string EndTimeString => EndDate.ToString(timeFormat);
        public string AllDayString => "All Day";
        public string Title { get; set; }
        public Color Color { get; set; }
        public string Detail { get; set; }        
        public bool IsAllDay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}