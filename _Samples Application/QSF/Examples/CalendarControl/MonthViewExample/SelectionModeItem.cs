using Telerik.XamarinForms.Input.Calendar;

namespace QSF.Examples.CalendarControl.MonthViewExample
{
    public class SelectionModeItem
    {
        public SelectionModeItem(string text, CalendarSelectionMode calendarMode)
        {
            this.Text = text;
            this.CalendarSelectionMode = calendarMode;
        }

        public string Text { get; set; }
        public CalendarSelectionMode CalendarSelectionMode { get; set; }
    }
}
