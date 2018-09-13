using System;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewAppointmentsExample
{
    public partial class MultiDayViewAppointmentsView : ContentView
    {
        public MultiDayViewAppointmentsView()
        {
            InitializeComponent();
            View addAppointmentButton = null;

            if (Device.RuntimePlatform == Device.Android)
            {
                addAppointmentButton = this.CreateAndroidAddAppointmentButton();
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                addAppointmentButton = this.CreateiOSAddAppointmentButton();
            }
            else
            {
                addAppointmentButton = this.CreateUWPAddAppointmentButton();
            }

            this.root.Children.Add(addAppointmentButton);
        }

        public void AppointmentTapped(object sender, Telerik.XamarinForms.Input.AppointmentTappedEventArgs e)
        {
            ((MultiDayViewAppointmentsViewModel)this.BindingContext).NavigateToReadAppointmentPage((Appointment)e.Appointment);
        }

        public void TimeSlotTapped(object sender, Telerik.XamarinForms.Input.TimeSlotTapEventArgs e)
        {
            ((MultiDayViewAppointmentsViewModel)this.BindingContext).NavigateToAddAppointmentPage(e.StartTime, e.EndTime);
        }

        private void CalendarLoaded(object sender, EventArgs e)
        {
            RadCalendar calendar = sender as RadCalendar;
            calendar.TrySetViewMode(CalendarViewMode.MultiDay, false);
        }

        private View CreateAndroidAddAppointmentButton()
        {
            var border = new RadBorder
            {
                WidthRequest = 56,
                HeightRequest = 56,
                BackgroundColor = Color.FromHex("#3F74D3"),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 16, 50),
                CornerRadius = 28
            };

            border.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                ((MultiDayViewAppointmentsViewModel) this.BindingContext).NavigateToAddAppointmentPage())
            });

            var label = new Label
            {
                Text = "+",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 20
            };

            border.Content = label;

            return border;
        }

        private View CreateiOSAddAppointmentButton()
        {
            var label = new Label
            {
                Text = "+",
                TextColor = Color.FromHex("#007AFF"),
                FontSize = 30,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0, -5, 10, 0),
            };

            label.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                ((MultiDayViewAppointmentsViewModel)this.BindingContext).NavigateToAddAppointmentPage())
            });

            return label;
        }

        private View CreateUWPAddAppointmentButton()
        {
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.FromHex("#EBEBEB"),
                HeightRequest = 50
            };

            var label = new Label
            {
                Text = "+",
                TextColor = Color.FromHex("#3148CA"),
                FontSize = 35,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 30, 0),
            };

            grid.Children.Add(label);

            label.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                ((MultiDayViewAppointmentsViewModel) this.BindingContext).NavigateToAddAppointmentPage())
            });

            return grid;
        }
    }
}
