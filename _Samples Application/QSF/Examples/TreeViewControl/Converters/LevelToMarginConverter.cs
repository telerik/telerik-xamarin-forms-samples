using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.TreeViewControl.Converters
{
    public class LevelToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            var level = (uint)value;

            return new Thickness(level * 40, 0, 0, 0);
        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
