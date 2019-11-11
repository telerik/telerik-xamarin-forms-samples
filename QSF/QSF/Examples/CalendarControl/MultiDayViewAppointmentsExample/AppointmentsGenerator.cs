using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.XamarinForms.Input;
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
            var recurrencePattern = new RecurrencePattern(new int[] { }, RecurrenceDays.WeekDays, RecurrenceFrequency.Daily, 1, null, null);
            var dailyRecurrenceRule = new RecurrenceRule(recurrencePattern);

            return new ObservableCollection<Appointment>
            {
                new Appointment
                {
                   StartDate = DateTime.Today.AddHours(8),
                   EndDate = DateTime.Today.AddHours(8.5),
                   Title = "Meeting",
                   Color = AppointmentColors[0],
                   RecurrenceRule = dailyRecurrenceRule
                },
                new Appointment
                {
                   StartDate =  DateTime.Today.AddDays(1),
                   EndDate = DateTime.Today.AddDays(3),
                   Title = "UX Conference",
                   Color = AppointmentColors[0],
                   IsAllDay = true
                },
                new Appointment
                {
                   StartDate = DateTime.Today.AddHours(9),
                   EndDate = DateTime.Today.AddHours(10),
                   Title = "Retrospective meeting",
                   Color = AppointmentColors[1]
                },
                new Appointment
                {
                   StartDate = DateTime.Today.AddDays(1).AddHours(10),
                   EndDate = DateTime.Today.AddDays(1).AddHours(12),
                   Title = "Planning",
                   Color = AppointmentColors[2]
                },
                new Appointment
                {
                   StartDate = DateTime.Today.AddDays(3).AddHours(8),
                   EndDate = DateTime.Today.AddDays(3).AddHours(9),
                   Title = "Meeting",
                   Color = AppointmentColors[3]
                }
            };
        }
    }
}
