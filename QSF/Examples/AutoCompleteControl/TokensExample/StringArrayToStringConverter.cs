using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.AutoCompleteControl.TokensExample
{
    public class StringArrayToStringConverter : IValueConverter
    {
        private static char[] separators = new char[] { ' ', ',', '.' };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] stringArray = (string[])value;
            string stringValue = string.Empty;

            foreach (string stringItem in stringArray)
            {
                if (!string.IsNullOrEmpty(stringValue))
                {
                    stringValue += ", ";
                }

                stringValue += stringItem;
            }

            return stringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = (string)value;
            string[] stringArray = stringValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return stringArray;
        }
    }
}
