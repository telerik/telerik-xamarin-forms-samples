using System;

namespace QSF.Examples.CalendarControl.MultiDayViewConfigurationExample
{
    public interface IMultiDayViewConfiguration
    {
        int VisibleDays { get; set; }
        int PeopleCount { get; set; }
        TimeSpan DayStartTime { get; set; }
        TimeSpan DayEndTime { get; set; }
        TimeSpan TimelineInterval { get; set; }
        bool WeekendsVisible { get; set; }
        bool CurrentTimeVisible { get; set; }
    }
}
