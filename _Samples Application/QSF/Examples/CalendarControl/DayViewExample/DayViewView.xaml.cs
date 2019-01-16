using System;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.DayViewExample
{
    public partial class DayViewView : ContentView
    {
        public DayViewView()
        {
            this.InitializeComponent();

            var today = new TapGestureRecognizer();
            today.Tapped += this.DisplayToday;
            this.todayLabel.GestureRecognizers.Add(today);
        }

        private void DisplayToday(object sender, EventArgs e)
        {
            calendar.DisplayDate = DateTime.Today;
        }
    }
}