using Telerik.XamarinForms.Input;

namespace QSF.Examples.CalendarControl.SchedulingExample
{
    public class ViewMode
    {
        public ViewMode(string text, CalendarViewMode calendarMode)
        {
            this.Text = text;
            this.CalendarMode = calendarMode;
        }

        public string Text { get; set; }
        public CalendarViewMode CalendarMode { get; set; }
    }
}