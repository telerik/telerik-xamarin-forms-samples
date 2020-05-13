using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.TemplatedPickerControl.FirstLookExample
{
    public class SizeViewModel : BindableBase
    {
        private string name;
        private Color backgroundColor;
        private Color textColor;

        public SizeViewModel(string name, string backgroundColor, string textColor)
            : this(name, Color.FromHex(backgroundColor), Color.FromHex(textColor))
        {
        }

        public SizeViewModel(string name, Color backgroundColor, Color textColor)
        {
            this.name = name;
            this.backgroundColor = backgroundColor;
            this.textColor = textColor;
        }

        public SizeViewModel(string name)
        {
            this.name = name;
            this.backgroundColor = Color.FromHex("#EAEAEA");
            this.textColor = Color.Black;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }
            set
            {
                if (this.backgroundColor != value)
                {
                    this.backgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                if(this.textColor != value)
                {
                    this.textColor = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
