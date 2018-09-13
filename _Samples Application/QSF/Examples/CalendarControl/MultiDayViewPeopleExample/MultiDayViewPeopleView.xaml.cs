using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.CalendarControl.MultiDayViewPeopleExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiDayViewPeopleView : ContentView
    {
        public MultiDayViewPeopleView()
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
