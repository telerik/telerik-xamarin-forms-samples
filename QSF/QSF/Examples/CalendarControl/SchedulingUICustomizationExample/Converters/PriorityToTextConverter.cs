using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    public class PriorityToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Priority priority)
            {
                switch (priority)
                {
                    case Priority.Low:
                        return "Low";
                    case Priority.High:
                        return "High";
                    case Priority.VeryHigh:
                        return "Very High";
                }
            }

            return "Low";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
