using System;
using Telerik.XamarinForms.Input;

namespace QSF.Examples.CalendarControl.AgendaViewCustomizationExample
{
    public class AgendaViewStyleSelector : AgendaItemStyleSelector
    {
        public AgendaTextItemStyle MonthItemStyle { get; set; }
        public AgendaTextItemStyle WeekItemStyle { get; set; }
        public AgendaTextItemStyle DayItemStyle { get; set; }
        public AgendaAppointmentItemStyle AppointmentItemStyle { get; set; }
        public AgendaTextItemStyle TodayStyle { get; set; }

        public override AgendaTextItemStyle SelectMonthItemStyle(AgendaMonthItem item)
        {
            return this.MonthItemStyle;
        }

        public override AgendaTextItemStyle SelectWeekItemStyle(AgendaWeekItem item)
        {
            return this.WeekItemStyle;
        }

        public override AgendaTextItemStyle SelectDayItemStyle(AgendaDayItem item)
        {
            if (item.Date.Date == DateTime.Today)
            {
                return this.TodayStyle;
            }

            return this.DayItemStyle;
        }

        public override AgendaAppointmentItemStyle SelectAppointmentItemStyle(AgendaAppointmentItem item)
        {
            return this.AppointmentItemStyle;
        }
    }
}
