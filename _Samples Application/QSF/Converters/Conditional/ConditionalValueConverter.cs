using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Converters
{
    public class ConditionalValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool condition = (bool)value;

            var param = (ConditionalValueConverterParameter)parameter;

            return condition ? param.TrueValue : param.FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
