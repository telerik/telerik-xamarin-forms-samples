using System;
using System.Globalization;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;

namespace QSF.Converters
{
    public class ItemTapEventArgsToItem : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ItemTapEventArgs itemTapEventArgs = (ItemTapEventArgs)value;
            return itemTapEventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
