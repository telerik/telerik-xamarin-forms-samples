using QSF.Examples.CalendarControl.SchedulingExample;
using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using Telerik.XamarinForms.Input;

namespace QSF.Examples.CalendarControl.SpecialSlotsExample
{
    public class SpecialSlotsViewModel : ExampleViewModel
    {
        private ViewMode selectedMode;
        private DateTime displayDate;

        public SpecialSlotsViewModel()
        {
            this.Modes = new ObservableCollection<ViewMode>();
            this.Modes.Add(new ViewMode(char.ConvertFromUtf32(0xe862), CalendarViewMode.MultiDay));
            this.Modes.Add(new ViewMode(char.ConvertFromUtf32(0xe861), CalendarViewMode.Day));
            this.SelectedMode = this.Modes[0];

            this.Holidays = this.GetHolidays();
            this.DisplayDate = new DateTime(2019, 11, 28);
        }

        public ObservableCollection<ViewMode> Modes { get; set; }

        public ObservableCollection<HolidaySlot> Holidays { get; set; }

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
                        return;
                    }

                    this.selectedMode = value;
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
                    this.OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<HolidaySlot> GetHolidays()
        {
            var holidays = new ObservableCollection<HolidaySlot>();
            var recursUntilDate = new DateTime(2021, 1, 1);

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 1, 1),
                EndDate = new DateTime(2017, 1, 1, 23, 59, 59),
                Name = "New Year's Day",
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 1,
                    DaysOfMonth = new int[] { 1 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 1, 21),
                EndDate = new DateTime(2017, 1, 21, 23, 59, 59),
                Name = "Martin Luther King Jr. Day",
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 1,
                    DaysOfMonth = new int[] { 21 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 2, 18),
                EndDate = new DateTime(2017, 2, 18, 23, 59, 59),
                Name = "President's Day",
                IsRegional = true,
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 2,
                    DaysOfMonth = new int[] { 18 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 5, 12),
                EndDate = new DateTime(2017, 5, 12, 23, 59, 59),
                Name = "Mother's Day",
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 5,
                    DaysOfMonth = new int[] { 12 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 5, 27),
                EndDate = new DateTime(2017, 5, 27, 23, 59, 59),
                Name = "Memorial Day",
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 5,
                    DaysOfMonth = new int[] { 27 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 6, 16),
                EndDate = new DateTime(2017, 6, 16, 23, 59, 59),
                Name = "Father's Day",
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 6,
                    DaysOfMonth = new int[] { 16 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 7, 4),
                EndDate = new DateTime(2017, 7, 4, 23, 59, 59),
                Name = "Independence Day",
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 7,
                    DaysOfMonth = new int[] { 4 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 9, 2),
                EndDate = new DateTime(2017, 9, 2, 23, 59, 59),
                Name = "Labor Day",
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 9,
                    DaysOfMonth = new int[] { 2 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 10, 14),
                EndDate = new DateTime(2017, 10, 14, 23, 59, 59),
                Name = "Columbus Day",
                IsRegional = true,
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 10,
                    DaysOfMonth = new int[] { 14 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 10, 14),
                EndDate = new DateTime(2017, 10, 14, 23, 59, 59),
                Name = "US Indigenous People's Day",
                IsRegional = true,
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 10,
                    DaysOfMonth = new int[] { 14 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 11, 11),
                EndDate = new DateTime(2017, 11, 11, 23, 59, 59),
                Name = "Veterans Day",
                IsRegional = true,
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 11,
                    DaysOfMonth = new int[] { 11 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 11, 28),
                EndDate = new DateTime(2017, 11, 28, 23, 59, 59),
                Name = "Thanksgiving",
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 11,
                    DaysOfMonth = new int[] { 28 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 11, 29),
                EndDate = new DateTime(2017, 11, 29, 23, 59, 59),
                Name = "Day after Thanksgiving",
                IsRegional = true,
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 11,
                    DaysOfMonth = new int[] { 29 }
                }
            });

            holidays.Add(new HolidaySlot()
            {
                StartDate = new DateTime(2017, 12, 25),
                EndDate = new DateTime(2017, 12, 25, 23, 59, 59),
                Name = "Christmas Day",
                RecurrencePattern = new RecurrencePattern()
                {
                    RecursUntil = recursUntilDate,
                    Frequency = RecurrenceFrequency.Yearly,
                    MonthOfYear = 12,
                    DaysOfMonth = new int[] { 25 }
                }
            });

            return holidays;
        }
    }
}