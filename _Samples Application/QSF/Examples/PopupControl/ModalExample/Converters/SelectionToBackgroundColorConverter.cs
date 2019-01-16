using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ModalExample.Converters
{
    public class SelectionToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;

            if (isSelected)
            {
                return new Color(0.9, 0.9, 0.9);
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
