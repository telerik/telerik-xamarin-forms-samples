using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MonthViewExample
{
    public class MonthViewViewModel : ExampleViewModel
    {
        private DateTime? selectedDate;

        public MonthViewViewModel()
        {
            this.SelectedEvents = new ObservableCollection<EventData>();
            this.selectedDate = DateTime.Today;
            var recurrencePattern = new RecurrencePattern(new int[] { }, RecurrenceDays.WeekDays, RecurrenceFrequency.Daily, 1, null, null) { MaxOccurrences = 37 };
            var dailyRecurrenceRule = new RecurrenceRule(recurrencePattern);

            this.Events = new ObservableCollection<EventData>()
            {
                new EventData(
                    DateTime.Today.AddHours(11),
                    DateTime.Today.AddHours(11).AddMinutes(15),
                    "Daily SCRUM",
                    Color.FromHex("59B6B8"),
                    Color.FromHex("59B6B8")) { RecurrenceRule = dailyRecurrenceRule },
                new EventData(
                    DateTime.Today.AddDays(-7).AddHours(10),
                    DateTime.Today.AddDays(-7).AddHours(11),
                    "Tokyo Deall call",
                    Color.FromHex("FFA200"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddDays(-7).AddHours(16) ,
                    DateTime.Today.AddDays(-7).AddHours(17).AddMinutes(30),
                    "Dinner with the Morgans",
                    Color.FromHex("59B6B8"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddDays(-4).AddHours(15),
                    DateTime.Today.AddDays(-4).AddHours(16).AddMinutes(30),
                    "Theater evening",
                    Color.FromHex("59B6B8"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddDays(-3).AddHours(9),
                    DateTime.Today.AddDays(-3).AddHours(10),
                    "Conference call with HQ2",
                    Color.FromHex("FFA200"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddDays(-2),
                    DateTime.Today.AddDays(-2).AddSeconds(1),
                    "Weekend barbecue",
                    Color.FromHex("59B6B8"),
                    Color.FromHex("59B6B8"),
                    true),
                new EventData(
                    DateTime.Today.AddDays(-1),
                    DateTime.Today.AddDays(-1).AddSeconds(1),
                    "Mountain biking",
                    Color.FromHex("C9353E"),
                    Color.FromHex("C9353E"),
                    true),
                //Today`s events
                new EventData(
                    DateTime.Today.AddHours(9),
                    DateTime.Today.AddHours(10),
                    "Job Interview",
                    Color.FromHex("FFA200"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddHours(10),
                    DateTime.Today.AddHours(11),
                    "Tokyo deal call",
                    Color.FromHex("FFA200"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddHours(14),
                    DateTime.Today.AddHours(15).AddMinutes(30),
                    "Yachting",
                    Color.FromHex("59B6B8"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddHours(16),
                    DateTime.Today.AddHours(17).AddMinutes(30),
                    "Dinner with the Morgans",
                    Color.FromHex("59B6B8"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddHours(18),
                    DateTime.Today.AddHours(19).AddMinutes(30),
                    "Fitness",
                    Color.FromHex("C9353E"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddHours(20),
                    DateTime.Today.AddHours(22),
                    "Watch a movie",
                    Color.FromHex("C42FBA"),
                    Color.White),
                //Tomorrow
                new EventData(
                    DateTime.Today.AddHours(20),
                    DateTime.Today.AddHours(22),
                    "Date with Candice",
                    Color.FromHex("C42FBA"),
                    Color.White),
                //Day after tomorrow
                new EventData(
                    DateTime.Today.AddDays(2).AddHours(18),
                    DateTime.Today.AddDays(2).AddHours(19).AddMinutes(30),
                    "Watch your favourite show",
                    Color.FromHex("59B6B8"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddDays(2).AddHours(19),
                    DateTime.Today.AddDays(2).AddHours(20).AddMinutes(30),
                    "Football",
                    Color.FromHex("C9353E"),
                    Color.White),
                //Two days after tomorrow
                new EventData(
                    DateTime.Today.AddDays(3).AddHours(10),
                    DateTime.Today.AddDays(3).AddHours(11),
                    "Coordination meeting",
                    Color.FromHex("FFA200"),
                    Color.White),
                new EventData(
                    DateTime.Today.AddDays(3).AddHours(15),
                    DateTime.Today.AddDays(3).AddHours(16).AddMinutes(30),
                    "Theater evening",
                    Color.FromHex("59B6B8"),
                    Color.White),
                new EventData(
                     DateTime.Today.AddDays(3).AddHours(18),
                     DateTime.Today.AddDays(3).AddHours(19).AddMinutes(30),
                    "Table tennis",
                    Color.FromHex("C9353E"),
                    Color.White),
                //Three days after tomorrow
                new EventData(
                    DateTime.Today.AddDays(4).AddHours(9),
                    DateTime.Today.AddDays(4).AddHours(10),
                    "Conference call with HQ2",
                    Color.FromHex("FFA200"),
                    Color.White),
                //Four days after tomorrow
                new EventData(
                    DateTime.Today.AddDays(5).AddHours(21),
                    DateTime.Today.AddDays(5).AddHours(23),
                    "Birthday party",
                    Color.FromHex("C42FBA"),
                    Color.White),
            };

            this.UpdateSelectedEvents(DateTime.Today);
        }

        public string DayLabel
        {
            get
            {
                if (this.SelectedDate.HasValue)
                {
                    var date = SelectedDate.Value;
                    var result = string.Format("{0}, {1} {2}", date.DayOfWeek, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month), date.Day);

                    if (Device.RuntimePlatform == Device.Android)
                    {
                        result = result.ToUpper();
                    }

                    return result;
                }

                return null;
            }
        }

        public ObservableCollection<EventData> Events { get; }

        public DateTime? SelectedDate
        {
            get
            {
                return this.selectedDate;
            }
            set
            {
                if (value != this.selectedDate)
                {
                    this.selectedDate = value;
                    this.OnPropertyChanged();
                    this.UpdateSelectedEvents(value);
                }
            }
        }

        public ObservableCollection<EventData> SelectedEvents { get; }

        private void UpdateSelectedEvents(DateTime? value)
        {
            this.SelectedEvents.Clear();
            foreach (EventData item in this.Events)
            {
                var date = value.Value;
                var recurrenceRule = item.RecurrenceRule;
                if ((recurrenceRule == null && item.StartDate.CompareTo(date) >= 0 && item.StartDate.CompareTo(date.AddDays(1)) < 0) || (recurrenceRule != null && recurrenceRule.Pattern.GetOccurrences(date, date, date).Any()))
                {
                    this.SelectedEvents.Add(new EventData(item.StartDate, item.EndDate, item.Title, item.LeadBorderColor, item.ItemBackgroundColor));
                }
            }

            this.OnPropertyChanged("DayLabel");
        }
    }
}
