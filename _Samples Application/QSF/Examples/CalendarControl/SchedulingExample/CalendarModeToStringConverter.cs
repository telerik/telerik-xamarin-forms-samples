using System;
using System.Globalization;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingExample
{
    public class CalendarModeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((CalendarViewMode)value)
            {
                case CalendarViewMode.Month:
                    return "MONTH";
                case CalendarViewMode.Week:
                    return "WEEK";
                case CalendarViewMode.Year:
                    return "YEAR";
                case CalendarViewMode.Day:
                    return "DAY";
                case CalendarViewMode.MultiDay:
                    return "MULTI-DAY";
                case CalendarViewMode.Agenda:
                    return "AGENDA";
                default:
                    break;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
