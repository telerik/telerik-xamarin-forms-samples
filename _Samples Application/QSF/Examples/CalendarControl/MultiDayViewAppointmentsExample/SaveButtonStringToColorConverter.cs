using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewAppointmentsExample
{
    public class SaveButtonStringToColorConverter : IValueConverter
    {
        private Color defaultColor;
        private Color disabledColorColor;

        public SaveButtonStringToColorConverter()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                this.defaultColor = Color.Accent;
                this.disabledColorColor = Color.FromHex("#C7C7CC");
            }
            else
            {
                this.defaultColor = Color.Accent;
                this.disabledColorColor = Color.FromHex("#C7C7CC");
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;
            if (!string.IsNullOrEmpty(stringValue))
            {
                return this.defaultColor;
            }

            return this.disabledColorColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
