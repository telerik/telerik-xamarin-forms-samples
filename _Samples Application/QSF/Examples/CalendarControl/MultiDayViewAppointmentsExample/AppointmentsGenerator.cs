using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewAppointmentsExample
{
    public static class AppointmentsGenerator
    {
        public static List<Color> AppointmentColors;

        static AppointmentsGenerator()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                AppointmentColors = new List<Color>
                {
                    Color.FromHex("#009688"),
                    Color.FromHex("#FD818D"),
                    Color.FromHex("#3F74D3"),
                    Color.FromHex("#81ADFD"),
                };
            }
            else
            {
                AppointmentColors = new List<Color>
                {
                    Color.FromHex("#FF5225"),
                    Color.FromHex("#30BCFF"),
                    Color.FromHex("#FF6F00"),
                    Color.FromHex("#007AFF"),
                };
            }
        }

        public static ObservableCollection<Appointment> GenerateAppointments()
        {
            return new ObservableCollection<Appointment>
            {
                new Appointment(
                    DateTime.Today.AddHours(8),
                    DateTime.Today.AddHours(8.5),
                    "Meeting",
                    AppointmentColors[0]),
                new Appointment(
                    DateTime.Today.AddDays(1),
                    DateTime.Today.AddDays(3),
                    "UX Conference",
                    AppointmentColors[0],
                    true),
                new Appointment(
                    DateTime.Today.AddHours(9),
                    DateTime.Today.AddHours(10),
                    "Retrospective meeting",
                    AppointmentColors[1]),
                new Appointment(
                    DateTime.Today.AddDays(1).AddHours(10),
                    DateTime.Today.AddDays(1).AddHours(12),
                    "Planning",
                    AppointmentColors[2]),
                new Appointment(
                    DateTime.Today.AddDays(3).AddHours(8),
                    DateTime.Today.AddDays(3).AddHours(9),
                    "Meeting",
                    AppointmentColors[3])
            };
        }
    }
}
