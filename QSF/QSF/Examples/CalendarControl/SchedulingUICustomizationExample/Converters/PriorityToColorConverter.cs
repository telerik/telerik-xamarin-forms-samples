using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    public class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Priority priority)
            {
                switch (priority)
                {
                    case Priority.Low:
                        return Color.FromHex("#FFAC3E");
                    case Priority.High:
                        return Color.FromHex("#FF6F00");
                    case Priority.VeryHigh:
                        return Color.FromHex("#F85446");
                }
            }

            return Color.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
