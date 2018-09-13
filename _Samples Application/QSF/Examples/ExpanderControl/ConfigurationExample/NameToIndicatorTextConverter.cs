using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.ExpanderControl.ConfigurationExample
{
    public class NameToIndicatorTextConverter : IValueConverter
    {
        public static readonly string Triangle = "\uE806";
        public static readonly string Arrow = "\uF105";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = (string)value;

            if (text == Triangle)
            {
                return nameof(Triangle);
            }

            return nameof(Arrow);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = (string)value;

            if (name == nameof(Triangle))
            {
                return Triangle;
            }

            return Arrow;
        }
    }
}
