using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.DayViewExample
{
    public partial class DayViewView : ContentView
    {
        public DayViewView()
        {
            this.InitializeComponent();

            var today = new TapGestureRecognizer();
            today.Tapped += DisplayToday;
            this.todayLabel.GestureRecognizers.Add(today);
        }

        private void CalendarLoaded(object sender, EventArgs e)
        {
            (sender as RadCalendar).TrySetViewMode(CalendarViewMode.Day, false);
        }

        private void DisplayToday(object sender, EventArgs e)
        {
            calendar.DisplayDate = DateTime.Today;
        }
    }

    public class DayViewCalendar : RadCalendar
    {
    }
}