using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.ImageEditorControl.CustomToolbarExample
{
    public class SelectedItemStyleConverter : IValueConverter
    {
        public Style DefaultStyle { get; set; }
        public Style SelectedStyle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return this.SelectedStyle;
            }

            return this.DefaultStyle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
