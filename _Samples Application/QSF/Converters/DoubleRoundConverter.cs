using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Converters
{
    public class DoubleRoundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value);
        }
    }
}
