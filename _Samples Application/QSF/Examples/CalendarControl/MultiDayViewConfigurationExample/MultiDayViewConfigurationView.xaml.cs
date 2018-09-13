using System;
using QSF.Services;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewConfigurationExample
{
    public partial class MultiDayViewConfigurationView : ContentView
    {
        public MultiDayViewConfigurationView()
        {
            this.InitializeComponent();
        }

        private void OnCalendarLoaded(object sender, EventArgs eventArgs)
        {
            var calendar = (RadCalendar)sender;

            calendar.TrySetViewMode(CalendarViewMode.MultiDay);
        }
    }
}
