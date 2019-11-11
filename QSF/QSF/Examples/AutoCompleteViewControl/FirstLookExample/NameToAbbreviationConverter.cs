using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace QSF.Examples.AutoCompleteViewControl.FirstLookExample
{
    public class NameToAbbreviationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] names = value.ToString().Split(' ');
            return string.Format("{0}{1}", names[0].ToCharArray().ElementAt(0), names[1].ToCharArray().ElementAt(0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
