using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.ProgressBarControl.ConfigurationExample
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string colorString = (string)value;
            Color color = Color.Default;
            switch (colorString)
            {
                case "Default":
                    color = Color.Default;
                    break;
                case "Blue":
                    color = Color.FromHex("#007AFF");
                    break;
                case "Red":
                    color = Color.FromHex("#F85446");
                    break;
                case "Green":
                    color = Color.FromHex("#019688");
                    break;
                case "Orange":
                    color = Color.FromHex("#FFAC3E");
                    break;
                case "Purple":
                    color = Color.Purple;
                    break;
                case "Light Green":
                    color = Color.FromHex("#56AF51");
                    break;
                case "Light Grey":
                    color = Color.FromHex("#DFDFDF");
                    break;
                default:
                    break;
            }

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
