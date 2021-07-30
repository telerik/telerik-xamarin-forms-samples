using Xamarin.Forms;

namespace QSF.Converters
{
    public class HightlitedTextConverterParameter : BindableObject
    {
        public static readonly BindableProperty HighlightTextColorProperty =
            BindableProperty.Create(nameof(HighlightTextColor), typeof(Color), typeof(HightlitedTextConverterParameter), Color.Default);

        public static readonly BindableProperty HighlightBackgroundColorProperty =
            BindableProperty.Create(nameof(HighlightBackgroundColor), typeof(Color), typeof(HightlitedTextConverterParameter), Color.Default);

        public Color HighlightTextColor
        {
            get
            {
                return (Color)this.GetValue(HighlightTextColorProperty);
            }
            set
            {
                this.SetValue(HighlightTextColorProperty, value);
            }
        }

        public Color HighlightBackgroundColor
        {
            get
            {
                return (Color)this.GetValue(HighlightBackgroundColorProperty);
            }
            set
            {
                this.SetValue(HighlightBackgroundColorProperty, value);
            }
        }
    }
}
