using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewAppointmentsExample
{
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = "No";
            var boolValue = (bool)value;
            if (boolValue)
            {
                result = "Yes";
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
