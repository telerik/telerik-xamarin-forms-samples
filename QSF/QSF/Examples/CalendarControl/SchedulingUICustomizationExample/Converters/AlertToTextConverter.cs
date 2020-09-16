using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    public class AlertToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Alert alert)
            {
                switch (alert)
                {
                    case Alert.None:
                        return "None";
                    case Alert.FiveMinutes:
                        return "5 minutes before";
                    case Alert.FifteenMinutes:
                        return "15 minutes before";
                    case Alert.ThirtyMinutes:
                        return "30 minutes before";
                    case Alert.OneHour:
                        return "1 hour before";
                }
            }
            return "None";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
