using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        private static char[] separators = new char[] { ';', ' ' };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string imageName = TryGetImageName((string)value);

            if (Device.RuntimePlatform == Device.UWP)
            {
                return "Assets/" + imageName;
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                return ((string)imageName).Replace(".png", string.Empty);
            }

            return imageName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string TryGetImageName(string value)
        {
            string[] names = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (names.Length == 0)
            {
                return value;
            }

            if (names.Length == 1)
            {
                return names[0];
            }

            string name;

            switch (Application.Current.RequestedTheme)
            {
                case OSAppTheme.Dark:
                    name = names[1];
                    break;
                case OSAppTheme.Light:
                case OSAppTheme.Unspecified:
                default:
                    name = names[0];
                    break;
            }

            return name;
        }
    }
}
