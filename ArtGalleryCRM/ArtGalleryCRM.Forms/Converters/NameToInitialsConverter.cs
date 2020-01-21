using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Converters
{
    internal class NameToInitialsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = value as string;

            return string.IsNullOrEmpty(name) 
                ? "XX" 
                : new Regex(@"\s*([^\s])[^\s]*\s*").Replace(name, "$1" + " ").ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}