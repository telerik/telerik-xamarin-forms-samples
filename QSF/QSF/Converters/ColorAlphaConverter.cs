using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Converters
{
    public class ColorAlphaConverter : IValueConverter
    {
        public double Alpha { get; set; } = 1.0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = value as Color?;
            if (color == null)
            {
                return Color.Default;
            }

            return color.Value.MultiplyAlpha(this.Alpha);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
