using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ContextMenuExample
{
    public class ChildrenCountToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var businessItem = (FolderViewModel)value;
            if (businessItem != null)
            {
                if (businessItem.Children != null && businessItem.Children.Count > 0)
                {
                    // show standard arrow
                    return false;
                }

                return businessItem.HasUnrealizedChildren;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
