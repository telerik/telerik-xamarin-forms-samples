using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    public class PriorityToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Priority priority)
            {
                switch (priority)
                {
                    case Priority.Low:
                        return char.ConvertFromUtf32(0xe879);
                    case Priority.High:
                        return char.ConvertFromUtf32(0xe81a);
                    case Priority.VeryHigh:
                        return char.ConvertFromUtf32(0xe87a);
                }
            }

            return char.ConvertFromUtf32(0xe879);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
