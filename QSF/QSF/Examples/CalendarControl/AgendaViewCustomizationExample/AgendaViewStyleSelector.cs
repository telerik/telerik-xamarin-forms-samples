using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.AgendaViewCustomizationExample
{
    public class AgendaViewStyleSelector : AgendaItemStyleSelector
    {
        public AgendaTextItemStyle MonthItemStyle { get; set; }
        public AgendaTextItemStyle WeekItemStyle { get; set; }
        public AgendaTextItemStyle DayItemStyle { get; set; }
        public AgendaAppointmentItemStyle AppointmentItemStyleLight { get; set; }
        public AgendaAppointmentItemStyle AppointmentItemStyleDark { get; set; }
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
            if (Application.Current.RequestedTheme != OSAppTheme.Dark)
            {
                return this.AppointmentItemStyleLight;
            }

            return this.AppointmentItemStyleDark;
        }
    }
}
