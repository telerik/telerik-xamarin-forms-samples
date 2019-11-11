using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Input.Calendar;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MonthViewExample
{
    public class MonthViewViewModel : ExampleViewModel
    {
        private SelectionModeItem selectedMode;
        private bool isDrawerOpen;
        private DateTimeRange selectedRange;

        public MonthViewViewModel()
        {
            this.SelectedEvents = new ObservableCollection<EventData>();
            var recurrencePattern = new RecurrencePattern(new int[] { }, RecurrenceDays.WeekDays, RecurrenceFrequency.Daily, 1, null, null) { MaxOccurrences = 37 };
            var dailyRecurrenceRule = new RecurrenceRule(recurrencePattern);

            this.OpenDrawerCommand = new Command(this.OnOpenDrawerCommandExecute);
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
                    Color.White,
                    true),
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
                    Color.White,
                    true),
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
                    Color.White,
                    true),
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
                    Color.White,
                    true),
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

            this.SelectionModes = new List<SelectionModeItem>();
            this.SelectionModes.Add(new SelectionModeItem(char.ConvertFromUtf32(0xe864), CalendarSelectionMode.None));
            this.SelectionModes.Add(new SelectionModeItem(char.ConvertFromUtf32(0xe866), CalendarSelectionMode.Single));

            if (!Device.RuntimePlatform.Equals("UWP"))
            {
                this.SelectionModes.Add(new SelectionModeItem(char.ConvertFromUtf32(0xe867), CalendarSelectionMode.Multiple));
                this.SelectionModes.Add(new SelectionModeItem(char.ConvertFromUtf32(0xe868), CalendarSelectionMode.Range));
            }

            this.selectedMode = Device.RuntimePlatform == "UWP" ? this.SelectionModes[1] : this.SelectionModes[3];
            this.selectedRange = new DateTimeRange(DateTime.Today, DateTime.Today.AddDays(2));
        }

        public DateTimeRange SelectedRange
        {
            get
            {
                return this.selectedRange;
            }
            set
            {
                this.selectedRange = value;
                this.OnPropertyChanged();
            }
        }

        public SelectionModeItem SelectedMode
        {
            get
            {
                return this.selectedMode;
            }
            set
            {
                if (this.selectedMode != value)
                {
                    if (value == null)
                    {
                        var selectedModeCache = this.selectedMode;
                        this.selectedMode = value;
                        this.SelectedMode = selectedModeCache;
                    }
                    else
                    {
                        this.selectedMode = value;
                        this.IsDrawerOpen = false;
                        this.OnPropertyChanged();
                    }
                }
            }
        }

        public List<SelectionModeItem> SelectionModes { get; set; }
        public ObservableCollection<EventData> Events { get; }

        public bool IsDrawerOpen
        {
            get
            {
                return this.isDrawerOpen;
            }
            set
            {
                if (this.isDrawerOpen != value)
                {
                    this.isDrawerOpen = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<EventData> SelectedEvents { get; }
        public Command OpenDrawerCommand { get; set; }

        internal void UpdateSelectedEvents(DateTime? value)
        {
            if (!value.HasValue)
            {
                return;
            }

            foreach (EventData item in this.Events)
            {
                var date = value.Value;
                var recurrenceRule = item.RecurrenceRule;
                if (recurrenceRule == null && item.StartDate.CompareTo(date) >= 0 && item.StartDate.CompareTo(date.AddDays(1)) < 0)
                {
                    this.SelectedEvents.Add(new EventData(item.StartDate, item.EndDate, item.Title, item.LeadBorderColor, item.ItemBackgroundColor, item.IsAllDay));
                }

                if (recurrenceRule != null && recurrenceRule.Pattern.GetOccurrences(item.StartDate, date, date.AddDays(1)).Any())
                {
                    EventData newEvent = new EventData(date.Date.Add(item.StartDate.TimeOfDay), date.Date.Add(item.EndDate.TimeOfDay), item.Title, item.LeadBorderColor, item.ItemBackgroundColor, item.IsAllDay);
                    this.SelectedEvents.Add(newEvent);
                }
            }
        }

        private void OnOpenDrawerCommandExecute(object obj)
        {
            this.IsDrawerOpen = true;
        }
    }
}
