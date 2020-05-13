using Xamarin.Forms;

namespace QSF.Examples.DateTimePickerControl.DateModeExample
{
    public class Session
    {
        private FormattedString text;
        private Color indicatorColor;
        private string time;
        private string name;

        public Session(string time, string name, Color indicatorColor)
        {
            this.time = time;
            this.name = name;
            this.Text = new FormattedString();
            this.Text.Spans.Add(new Span()
            {
                Text = this.time + " ",
                TextColor = Color.FromHex("#919191")
            });
            this.Text.Spans.Add(new Span()
            {
                Text = this.name,
                TextColor = Color.Black
            });
            this.IndicatorColor = indicatorColor;
        }

        public FormattedString Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (this.text != value)
                {
                    this.text = value;
                }
            }
        }

        public Color IndicatorColor
        {
            get
            {
                return this.indicatorColor;
            }
            set
            {
                if (this.indicatorColor != value)
                {
                    this.indicatorColor = value;
                }
            }
        }
    }
}
