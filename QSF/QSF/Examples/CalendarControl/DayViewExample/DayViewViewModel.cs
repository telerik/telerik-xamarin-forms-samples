using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Input = Telerik.XamarinForms.Input;

namespace QSF.Examples.CalendarControl.DayViewExample
{
    public class DayViewViewModel : ExampleViewModel
    {
        public ObservableCollection<Appointment> Appointments { get; }

        public DayViewViewModel()
        {
            this.Appointments = this.CreateAppointments();
        }

        private ObservableCollection<Appointment> CreateAppointments()
        {
            var work = Color.FromHex("C9353E");
            var home = Color.FromHex("59B6B8");
            var other = Color.FromHex("C42FBA");
            var party = Color.FromHex("FFA200");
            var recurrencePattern = new Input.RecurrencePattern(new int[] { }, Input.RecurrenceDays.WeekDays, Input.RecurrenceFrequency.Daily, 1, null, null) { MaxOccurrences = 15 };
            var dailyRecurrenceRule = new Input.RecurrenceRule(recurrencePattern);

            return new ObservableCollection<Appointment>
            {
                // today
                new Appointment(
                    DateTime.Today.AddHours(8).AddMinutes(30),
                    DateTime.Today.AddHours(9).AddMinutes(30),
                    "Fitness with Peter",
                    "@TopFit",
                    home),
                new Appointment(
                    DateTime.Today.AddHours(11),
                    DateTime.Today.AddHours(11).AddMinutes(15),
                    "Daily SCRUM", "",
                    work) { RecurrenceRule = dailyRecurrenceRule },
                new Appointment(
                    DateTime.Today.AddHours(11),
                    DateTime.Today.AddHours(12),
                    "Job Interview", "HQ1 213",
                    work),
                new Appointment(
                    DateTime.Today.AddHours(12).AddMinutes(30),
                    DateTime.Today.AddHours(14),
                    "Lunch with Sam", "BBQ and Beer",
                    party),
                new Appointment(
                    DateTime.Today.AddHours(14).AddMinutes(30),
                    DateTime.Today.AddHours(15).AddMinutes(30),
                    "Webinar", "@work",
                    work),
                new Appointment(
                    DateTime.Today.AddHours(16),
                    DateTime.Today.AddHours(17).AddMinutes(30),
                    "Tokio Deal call", "",
                    work),
                new Appointment(
                    DateTime.Today.AddHours(16).AddMinutes(45),
                    DateTime.Today.AddHours(17).AddMinutes(45),
                    "Dentist", "Dental Clinic",
                    home),
                new Appointment(
                    DateTime.Today.AddHours(18),
                    DateTime.Today.AddHours(19).AddMinutes(30),
                    "Home theater evening", "Samanta",
                    other),
                new Appointment(
                    DateTime.Today.AddHours(20),
                    DateTime.Today.AddHours(22),
                    "Watch a movie", "Cinema City",
                    party),
                new Appointment(
                    DateTime.Today ,
                    DateTime.Today.AddMinutes(1),
                    "Alex's Birthday", "Cinema City",
                    party, true),
                // -1
                new Appointment(
                    DateTime.Today.AddDays(-1).AddHours(10),
                    DateTime.Today.AddDays(-1).AddHours(11).AddMinutes(30),
                    "Innovation Explorer " + DateTime.Today.AddDays(-1).Year, "Sofia Event Center",
                    home),
                new Appointment(
                    DateTime.Today.AddDays(-1).AddHours(18),
                    DateTime.Today.AddDays(-1).AddHours(19),
                    "Swimming", "Holiday Inn",
                    work),
                new Appointment(
                    DateTime.Today.AddDays(-1).AddHours(13),
                    DateTime.Today.AddDays(-1).AddHours(14).AddMinutes(30),
                    "Lunch with the Morgans", "",
                    home),
                new Appointment(
                    DateTime.Today.AddDays(-1).AddHours(20),
                    DateTime.Today.AddDays(-1).AddHours(22).AddMinutes(30),
                    "Theater evening", "City Theater",
                    party),
                new Appointment(
                    DateTime.Today.AddDays(-1).AddHours(15),
                    DateTime.Today.AddDays(-1).AddHours(16),
                    "Conference call with HQ2", "",
                    work),
                new Appointment(
                    DateTime.Today.AddDays(2),
                    DateTime.Today.AddDays(3),
                    "Movies Festival", "",
                    other, true),
                // -2
                new Appointment(
                    DateTime.Today.AddDays(-2),
                    DateTime.Today.AddDays(-2).AddSeconds(1),
                    "Team Building Barbecue", "Tokio",
                    party, true),
                new Appointment(
                    DateTime.Today.AddDays(-2),
                    DateTime.Today.AddDays(-2).AddSeconds(1),
                    "Mountain biking",
                    "Vitosha",
                    other, true),
                // +1
                new Appointment(
                    DateTime.Today.AddDays(1).AddHours(20),
                    DateTime.Today.AddDays(1).AddHours(22),
                    "Date with Candice", "Happy Bar&Grill",
                    home),
                // +2
                new Appointment(
                    DateTime.Today.AddDays(2).AddHours(16).AddMinutes(30),
                    DateTime.Today.AddDays(2).AddHours(17),
                    "Daily meeting", "",
                    work),
                new Appointment(
                    DateTime.Today.AddDays(2).AddHours(18),
                    DateTime.Today.AddDays(2).AddHours(19).AddMinutes(30),
                    "Favourite show", "@Home",
                    home),
                new Appointment(
                    DateTime.Today.AddDays(2).AddHours(19),
                    DateTime.Today.AddDays(2).AddHours(20).AddMinutes(30),
                    "Football", "National Stadium",
                    party),
                new Appointment(
                    DateTime.Today.AddDays(2).AddHours(19),
                    DateTime.Today.AddDays(2).AddHours(20).AddMinutes(30),
                    "Planting Trees", "Rila Mountain",
                    other, true),
                new Appointment(
                    DateTime.Today.AddDays(2),
                    DateTime.Today.AddDays(10),
                    "Vacation", "Florence",
                    party, true),
                // +3
                new Appointment(
                    DateTime.Today.AddDays(3).AddHours(10),
                    DateTime.Today.AddDays(3).AddHours(11),
                    "Coordination meeting", "HQ3 205",
                    work),
                new Appointment(
                    DateTime.Today.AddDays(3).AddHours(15),
                    DateTime.Today.AddDays(3).AddHours(16).AddMinutes(30),
                    "Table tennis", "",
                    party),
                new Appointment(
                     DateTime.Today.AddDays(3).AddHours(18),
                     DateTime.Today.AddDays(3).AddHours(19).AddMinutes(30),
                     "Theater evening", "",
                     party),
                // +4
                new Appointment(
                    DateTime.Today.AddDays(4).AddHours(9),
                    DateTime.Today.AddDays(4).AddHours(10),
                    "Conference call with Tokio", "HQ1 Sofia",
                    work),
                // +5
                new Appointment(
                    DateTime.Today.AddDays(5).AddHours(21),
                    DateTime.Today.AddDays(5).AddHours(23),
                    "Birthday party",
                    "@Sam",
                    party)
            };
        }
    }
}
