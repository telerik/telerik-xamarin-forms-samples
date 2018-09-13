using System;
using System.Collections.ObjectModel;
using QSF.ViewModels;

namespace QSF.Examples.CalendarControl.MultiDayViewConfigurationExample
{
    public class MultiDayViewPropertiesViewModel : ConfigurationViewModel
    {
        private readonly IMultiDayViewConfiguration configuration;
        private int visibleDays;
        private int peopleCount;
        private DateTime displayDate;
        private TimeSpan dayStartTime;
        private TimeSpan dayEndTime;
        private TimeSpan timelineInterval;
        private bool weekendsVisible;
        private bool currentTimeVisible;
        private string validationMessage;
        private bool hasValidationErrors;

        public int VisibleDays
        {
            get
            {
                return this.visibleDays;
            }
            set
            {
                if (this.visibleDays != value)
                {
                    this.visibleDays = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int PeopleCount
        {
            get
            {
                return this.peopleCount;
            }
            set
            {
                if (this.peopleCount != value)
                {
                    this.peopleCount = value;
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

        public TimeSpan DayStartTime
        {
            get
            {
                return this.dayStartTime;
            }
            set
            {
                if (this.dayStartTime != value)
                {
                    this.dayStartTime = value;
                    this.OnPropertyChanged();
                    this.ValidateProperties();
                }
            }
        }

        public TimeSpan DayEndTime
        {
            get
            {
                return this.dayEndTime;
            }
            set
            {
                if (this.dayEndTime != value)
                {
                    this.dayEndTime = value;
                    this.OnPropertyChanged();
                    this.ValidateProperties();
                }
            }
        }

        public TimeSpan TimelineInterval
        {
            get
            {
                return this.timelineInterval;
            }
            set
            {
                if (this.timelineInterval != value)
                {
                    this.timelineInterval = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool WeekendsVisible
        {
            get
            {
                return this.weekendsVisible;
            }
            set
            {
                if (this.weekendsVisible != value)
                {
                    this.weekendsVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool CurrentTimeVisible
        {
            get
            {
                return this.currentTimeVisible;
            }
            set
            {
                if (this.currentTimeVisible != value)
                {
                    this.currentTimeVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ValidationMessage
        {
            get
            {
                return this.validationMessage;
            }
            private set
            {
                if (this.validationMessage != value)
                {
                    this.validationMessage = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool HasValidationErrors
        {
            get
            {
                return this.hasValidationErrors;
            }
            private set
            {
                if (this.hasValidationErrors != value)
                {
                    this.hasValidationErrors = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<int> VisibleDaysSource { get; private set; }
        public ObservableCollection<int> PeopleCountSource { get; private set; }
        public ObservableCollection<TimeSpan> TimelineIntervalSource { get; private set; }

        public MultiDayViewPropertiesViewModel(IMultiDayViewConfiguration configuration)
        {
            this.configuration = configuration;
            this.VisibleDays = this.configuration.VisibleDays;
            this.PeopleCount = this.configuration.PeopleCount;
            this.DayStartTime = this.configuration.DayStartTime;
            this.DayEndTime = this.configuration.DayEndTime;
            this.TimelineInterval = this.configuration.TimelineInterval;
            this.WeekendsVisible = this.configuration.WeekendsVisible;
            this.CurrentTimeVisible = this.configuration.CurrentTimeVisible;
            this.VisibleDaysSource = new ObservableCollection<int> { 1, 3, 5, 7 };
            this.PeopleCountSource = new ObservableCollection<int> { 1, 2, 3 };
            this.TimelineIntervalSource = new ObservableCollection<TimeSpan>
            {
                TimeSpan.FromMinutes(15),
                TimeSpan.FromMinutes(30),
                TimeSpan.FromMinutes(60)
            };
        }

        protected override void ApplyChangesOverride()
        {
            if (!this.HasValidationErrors)
            {
                this.configuration.VisibleDays = this.VisibleDays;
                this.configuration.PeopleCount = this.PeopleCount;
                this.configuration.DayStartTime = this.DayStartTime;
                this.configuration.DayEndTime = this.DayEndTime;
                this.configuration.TimelineInterval = this.TimelineInterval;
                this.configuration.WeekendsVisible = this.WeekendsVisible;
                this.configuration.CurrentTimeVisible = this.CurrentTimeVisible;
            }
        }

        private void ValidateProperties()
        {
            if (this.DayEndTime < this.DayStartTime)
            {
                this.ValidationMessage = "Start time must be earlier than end time.";
                this.HasValidationErrors = true;
            }
            else
            {
                this.ValidationMessage = null;
                this.HasValidationErrors = false;
            }
        }
    }
}
