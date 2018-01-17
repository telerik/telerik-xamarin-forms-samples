using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.TabViewControl.RestaurantMenuExample
{
    public class BooleanValueConverter : IValueConverter
    {
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }

        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            return (bool)value ? this.TrueValue : this.FalseValue;
        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
