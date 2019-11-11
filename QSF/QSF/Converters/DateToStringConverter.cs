using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = value as DateTime?;
            if (date != null)
            {
                return string.Format("{0:MMMMM yyyy}", date);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
