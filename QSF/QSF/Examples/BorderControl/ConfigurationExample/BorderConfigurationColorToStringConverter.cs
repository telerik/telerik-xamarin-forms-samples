using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.BorderControl.ConfigurationExample
{
	public class BorderConfigurationColorToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string colorString = (string)value;
            Color color = Color.Default;
            switch (colorString)
            {
                case "Light Grey":
                    color = Color.FromHex("#E5E5E5");
                    break;
                case "Dark Grey":
                    color = Color.FromHex("#A8AAAD");
                    break;
                case "Pink":
                    color = Color.FromHex("#EF2648");
                    break;
                case "Blue":
                    color = Color.FromHex("#3D5AFE");
                    break;
                default:
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
