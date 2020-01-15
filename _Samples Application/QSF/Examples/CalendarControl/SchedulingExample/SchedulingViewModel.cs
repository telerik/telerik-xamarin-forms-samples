using QSF.Examples.CalendarControl.MonthViewExample;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingExample
{
    public class SchedulingViewModel : ExampleViewModel
    {
        private bool isAgendaVisible;
        private ViewMode selectedMode;
        private DateTime displayDate;
        private bool isDrawerOpen;
        private Dictionary<string, Tuple<Color, Color>> appColors = new Dictionary<string, Tuple<Color, Color>>();

        public SchedulingViewModel()
        {
            this.Modes = new ObservableCollection<ViewMode>();
            this.Modes.Add(new ViewMode(char.ConvertFromUtf32(0xe872), Telerik.XamarinForms.Input.CalendarViewMode.Agenda));
            this.Modes.Add(new ViewMode(char.ConvertFromUtf32(0xe861), Telerik.XamarinForms.Input.CalendarViewMode.Day));
            this.Modes.Add(new ViewMode(char.ConvertFromUtf32(0xe862), Telerik.XamarinForms.Input.CalendarViewMode.MultiDay));
            this.Modes.Add(new ViewMode(char.ConvertFromUtf32(0xe863), Telerik.XamarinForms.Input.CalendarViewMode.Week));
            this.Modes.Add(new ViewMode(char.ConvertFromUtf32(0xe864), Telerik.XamarinForms.Input.CalendarViewMode.Month));
            this.Modes.Add(new ViewMode(char.ConvertFromUtf32(0xe865), Telerik.XamarinForms.Input.CalendarViewMode.Year));

            if (Device.RuntimePlatform == Device.Android)
            {
                this.appColors.Add("FirstColor", new Tuple<Color, Color>(Color.FromHex("#FFF1F0"), Color.FromHex("#FFF1F0")));
                this.appColors.Add("SecondColor", new Tuple<Color, Color>(Color.FromHex("#EAF8FF"), Color.FromHex("#EAF8FF")));
                this.appColors.Add("ThirdColor", new Tuple<Color, Color>(Color.FromHex("#F0FDFF"), Color.FromHex("#F0FDFF")));
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                this.appColors.Add("FirstColor", new Tuple<Color, Color>(Color.FromHex("#FF6F00"), Color.FromHex("#FFF4F4")));
                this.appColors.Add("SecondColor", new Tuple<Color, Color>(Color.FromHex("#30BCFF"), Color.FromHex("#EAF8FF")));
                this.appColors.Add("ThirdColor", new Tuple<Color, Color>(Color.FromHex("#8EE7F4"), Color.FromHex("#F0FDFF")));
            }

            this.Appointments = this.CreateAppointments();

            this.SelectedMode = this.Modes[0];
            this.WeekEvents = new ObservableCollection<EventData>();

            this.DisplayDate = DateTime.Now;

            this.OpenDrawerCommand = new Command(this.OnOpenDrawerCommandExecute);
        }

        public ObservableCollection<ViewMode> Modes { get; set; }
        public ObservableCollection<EventData> Appointments { get; }
        public ObservableCollection<EventData> WeekEvents { get; set; }
        public Command OpenDrawerCommand { get; set; }

        public bool IsAgendaVisible
        {
            get
            {
                return this.isAgendaVisible;
            }
            set
            {
                if (this.isAgendaVisible != value)
                {
                    this.isAgendaVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ViewMode SelectedMode
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
                        var oldSelectedItem = this.selectedMode;
                        this.selectedMode = value;
                        this.SelectedMode = oldSelectedItem;
                        return;
                    }

                    this.selectedMode = value;
                    this.IsAgendaVisible = this.selectedMode != null && this.selectedMode.CalendarMode == Telerik.XamarinForms.Input.CalendarViewMode.Week;
                    this.IsDrawerOpen = false;
                    this.SetWeekEvents();
                    this.OnPropertyChanged();
                }
            }
        }

        public DateTime DisplayDate
        {
            get
            {
                return this.displayDate;
            }
            set
            {
                if (this.displayDate != value)
                {
                    this.displayDate = value;
                    this.SetWeekEvents();
                    this.OnPropertyChanged();
                }
            }
        }

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

        internal static DateTime GetFirstDayOfCurrentWeek(DateTime date, DayOfWeek startDayOfWeek)
        {
            DayOfWeek currentDayOfWeek = date.DayOfWeek;
            int daysToSubtract = currentDayOfWeek - startDayOfWeek;
            if (daysToSubtract <= 0)
            {
                daysToSubtract += 7;
            }

            return date.Date == DateTime.MinValue.Date ? date : date.AddDays(-daysToSubtract);
        }

        private DayOfWeek GetFirstDayOfWeek()
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        }

        private void SetWeekEvents()
        {
            if (this.selectedMode == null || this.selectedMode.CalendarMode != Telerik.XamarinForms.Input.CalendarViewMode.Week)
            {
                return;
            }

            DayOfWeek firstDayOfWeek = this.GetFirstDayOfWeek();
            DateTime firstDateOfCurrentWeek = SchedulingViewModel.GetFirstDayOfCurrentWeek(this.displayDate, firstDayOfWeek).Date;

            this.WeekEvents.Clear();
            foreach (EventData item in this.Appointments)
            {
                var startDate = item.StartDate;
                if (startDate.CompareTo(firstDateOfCurrentWeek) >= 0 && startDate.CompareTo(firstDateOfCurrentWeek.AddDays(7)) < 0)
                {
                    this.WeekEvents.Add(new EventData(item.StartDate, item.EndDate, item.Title, item.LeadBorderColor, item.ItemBackgroundColor));
                }
            }
        }

        private ObservableCollection<EventData> CreateAppointments()
        {
            return new ObservableCollection<EventData>()
            {
                new EventData(
                    DateTime.Today.AddHours(11),
                    DateTime.Today.AddHours(11).AddMinutes(15),
                    "Daily SCRUM",
                    appColors["FirstColor"].Item1,
                    appColors["FirstColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(-7).AddHours(10),
                    DateTime.Today.AddDays(-7).AddHours(11),
                    "Tokyo Deall call",
                    appColors["SecondColor"].Item1,
                    appColors["SecondColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(-7).AddHours(16) ,
                    DateTime.Today.AddDays(-7).AddHours(17).AddMinutes(30),
                    "Dinner with the Morgans",
                    appColors["FirstColor"].Item1,
                    appColors["FirstColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(-4).AddHours(15),
                    DateTime.Today.AddDays(-4).AddHours(16).AddMinutes(30),
                    "Theater evening",
                    appColors["SecondColor"].Item1,
                    appColors["SecondColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(-3).AddHours(9),
                    DateTime.Today.AddDays(-3).AddHours(10),
                    "Conference call with HQ2",
                    appColors["ThirdColor"].Item1,
                    appColors["ThirdColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(-2),
                    DateTime.Today.AddDays(-2).AddSeconds(1),
                    "Weekend barbecue",
                    appColors["FirstColor"].Item1,
                    appColors["FirstColor"].Item2,
                    true),
                new EventData(
                    DateTime.Today.AddDays(-1),
                    DateTime.Today.AddDays(-1).AddSeconds(1),
                    "Mountain biking",
                    appColors["SecondColor"].Item1,
                    appColors["SecondColor"].Item2,
                    true),
                new EventData(
                    DateTime.Today.AddHours(4),
                    DateTime.Today.AddHours(6),
                    "Job Interview",
                    appColors["ThirdColor"].Item1,
                    appColors["ThirdColor"].Item2),
                new EventData(
                    DateTime.Today.AddHours(1),
                    DateTime.Today.AddHours(3),
                    "Tokyo deal call",
                    appColors["FirstColor"].Item1,
                    appColors["FirstColor"].Item2),
                new EventData(
                    DateTime.Today.AddHours(14),
                    DateTime.Today.AddHours(15).AddMinutes(30),
                    "Yachting",
                    appColors["SecondColor"].Item1,
                    appColors["SecondColor"].Item2),
                new EventData(
                    DateTime.Today.AddHours(16),
                    DateTime.Today.AddHours(17).AddMinutes(30),
                    "Dinner with the Morgans",
                    appColors["ThirdColor"].Item1,
                    appColors["ThirdColor"].Item2),
                new EventData(
                    DateTime.Today.AddHours(18),
                    DateTime.Today.AddHours(19).AddMinutes(30),
                    "Fitness",
                    appColors["FirstColor"].Item1,
                    appColors["FirstColor"].Item2),
                new EventData(
                    DateTime.Today.AddHours(20),
                    DateTime.Today.AddHours(22),
                    "Watch a movie",
                    appColors["SecondColor"].Item1,
                    appColors["SecondColor"].Item2),
                new EventData(
                    DateTime.Today.AddHours(20),
                    DateTime.Today.AddHours(22),
                    "Date with Candice",
                    appColors["ThirdColor"].Item1,
                    appColors["ThirdColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(2).AddHours(18),
                    DateTime.Today.AddDays(2).AddHours(19).AddMinutes(30),
                    "Watch your favourite show",
                    appColors["FirstColor"].Item1,
                    appColors["FirstColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(2).AddHours(19),
                    DateTime.Today.AddDays(2).AddHours(20).AddMinutes(30),
                    "Football",
                    appColors["SecondColor"].Item1,
                    appColors["SecondColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(3).AddHours(10),
                    DateTime.Today.AddDays(3).AddHours(11),
                    "Coordination meeting",
                    appColors["ThirdColor"].Item1,
                    appColors["ThirdColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(3).AddHours(15),
                    DateTime.Today.AddDays(3).AddHours(16).AddMinutes(30),
                    "Theater evening",
                    appColors["FirstColor"].Item1,
                    appColors["FirstColor"].Item2),
                new EventData(
                     DateTime.Today.AddDays(3).AddHours(18),
                     DateTime.Today.AddDays(3).AddHours(19).AddMinutes(30),
                    "Table tennis",
                    appColors["SecondColor"].Item1,
                    appColors["SecondColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(4).AddHours(9),
                    DateTime.Today.AddDays(4).AddHours(10),
                    "Conference call with HQ2",
                    appColors["ThirdColor"].Item1,
                    appColors["ThirdColor"].Item2),
                new EventData(
                    DateTime.Today.AddDays(5).AddHours(21),
                    DateTime.Today.AddDays(5).AddHours(23),
                    "Birthday party",
                    appColors["FirstColor"].Item1,
                    appColors["FirstColor"].Item2),
            };
        }

        private void OnOpenDrawerCommandExecute(object obj)
        {
            this.IsDrawerOpen = true;
        }
    }
}
