using QSF.ViewModels;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Converters
{
    public class HightlitedTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HighlightedTextInfo textInfo = (HighlightedTextInfo)value;
            HightlitedTextConverterParameter param = (HightlitedTextConverterParameter)parameter;

            FormattedString result = new FormattedString();

            if (textInfo.FirstCharIndex == textInfo.Lenght)
            {
                result.Spans.Add(new Span { Text = textInfo.Text });
            }
            else
            {
                var firstPart = textInfo.Text.Substring(0, textInfo.FirstCharIndex);
                if (!string.IsNullOrEmpty(firstPart))
                {
                    result.Spans.Add(new Span { Text = firstPart });
                }

                var middlePart = textInfo.Text.Substring(textInfo.FirstCharIndex, textInfo.Lenght);
                if (!string.IsNullOrEmpty(middlePart))
                {
                    result.Spans.Add(new Span { Text = middlePart, ForegroundColor = param.HighlightTextColor, BackgroundColor = param.HighlightBackgroundColor });
                }

                var lastPart = textInfo.Text.Substring(textInfo.FirstCharIndex + textInfo.Lenght);
                if (!string.IsNullOrEmpty(lastPart))
                {
                    result.Spans.Add(new Span { Text = lastPart });
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
