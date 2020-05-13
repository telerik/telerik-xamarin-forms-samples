using System;
using Telerik.XamarinForms.Input;

namespace QSF.Examples.CalendarControl.AgendaViewConfigurationExample
{
    public class TodayAgendaItemStyleSelector : AgendaItemStyleSelector
    {
        public AgendaTextItemStyle TodayStyle { get; set; }

        public override AgendaTextItemStyle SelectDayItemStyle(AgendaDayItem item)
        {
            if (item.Date.Date == DateTime.Today)
            {
                return this.TodayStyle;
            }

            return base.SelectDayItemStyle(item);
        }
    }
}
