using Telerik.XamarinForms.Input;

namespace QSF.Examples.CalendarControl.SpecialSlotsExample
{
    public class HolidaySlotStyleSelector : SpecialSlotStyleSelector
    {
        public CalendarSpecialSlotStyle RegionalHolidayStyle { get; set; }
        public CalendarSpecialSlotStyle FederalHolidayStyle { get; set; }

        public override CalendarSpecialSlotStyle SelectStyle(object item)
        {
            var holidaySlot = item as HolidaySlot;
            if (holidaySlot != null)
            {
                if (holidaySlot.IsRegional)
                {
                    return this.RegionalHolidayStyle;
                }

                return this.FederalHolidayStyle;
            }

            return null;
        }
    }
}
