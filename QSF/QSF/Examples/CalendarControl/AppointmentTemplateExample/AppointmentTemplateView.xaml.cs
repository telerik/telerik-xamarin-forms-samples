using System;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.CalendarControl.AppointmentTemplateExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentTemplateView : ContentView
    {
        public AppointmentTemplateView()
        {
            InitializeComponent();
            
            if (!(Device.RuntimePlatform == Device.Android))
            {
                this.calendar.ScrollTimeIntoView(TimeSpan.FromHours(12));
            }
        }

        private void Calendar_ViewChanged(object sender, ValueChangedEventArgs<CalendarViewMode> e)
        {
            this.calendar.DisplayDate = DateTime.Today;
            // Workaround for a limitation with ScrollToTime on Android
            if (Device.RuntimePlatform == Device.Android)
            {
                this.calendar.ScrollTimeIntoView(TimeSpan.FromHours(10));
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.calendar.ScrollTimeIntoView(TimeSpan.FromHours(12));
                });
            }
            else
            {
                this.calendar.ScrollTimeIntoView(TimeSpan.FromHours(12));
            }
        }
    }
}