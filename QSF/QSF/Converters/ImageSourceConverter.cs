using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (Device.RuntimePlatform == Device.UWP)
            {
                return "Assets/" + value;
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                return ((string)value).Replace(".png", string.Empty);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
