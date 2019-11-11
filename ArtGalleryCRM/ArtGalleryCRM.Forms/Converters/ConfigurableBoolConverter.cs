using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Converters
{
    internal class ConfigurableBoolConverter<T> : IValueConverter
    {
        public ConfigurableBoolConverter() { }

        public ConfigurableBoolConverter(T trueResult, T falseResult)
        {
            this.TrueResult = trueResult;
            this.FalseResult = falseResult;
        }

        public T TrueResult { get; set; }

        public T FalseResult { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(this.TrueResult == null || this.FalseResult == null)
            {
                return !(bool) value;
            }

            return value is bool b && b ? this.TrueResult : this.FalseResult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(this.TrueResult == null || this.FalseResult == null)
            {
                return !(bool) value;
            }

            return value is T variable && EqualityComparer<T>.Default.Equals(variable, this.TrueResult);
        }
    }
}