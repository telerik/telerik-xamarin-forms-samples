using QSF.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QSF.Examples.EntryControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private List<string> keyboards;
        private List<string> textColors;
        private List<NamedSize> fontSizes;
        private string selectedKeyboardString;
        private string selectedColorString;
        private NamedSize selectedNamedSize;
        private KeyboardTypeConverter keyboardTypeConverter;
        private ColorTypeConverter colorTypeConverter;
        private FontSizeConverter fontSizeConverter;

        public ConfigurationViewModel()
        {
            this.keyboards = new List<string>()
            {
                "Default",
                "Plain",
                "Chat",
                "Email",
                "Numeric",
                "Telephone",
                "Text",
                "Url"
            };

            this.textColors = new List<string>()
            {
                "Gray",
                "Orange",
                "Blue",
                "Pink"
            };

            this.fontSizes = new List<NamedSize>()
            {
                NamedSize.Default,
                NamedSize.Large,
                NamedSize.Small
            };

            this.keyboardTypeConverter = new KeyboardTypeConverter();
            this.colorTypeConverter = new ColorTypeConverter();
            this.fontSizeConverter = new FontSizeConverter();
        }

        public Keyboard SelectedKeyboard
        {
            get
            {
                if (string.IsNullOrEmpty(this.SelectedKeyboardString))
                {
                    return Keyboard.Default;
                }

                var val = this.keyboardTypeConverter.ConvertFromInvariantString(this.SelectedKeyboardString);
                return (Keyboard)val;
            }
        }

        public string SelectedKeyboardString
        {
            get
            {
                return this.selectedKeyboardString;
            }
            set
            {
                if (this.selectedKeyboardString != value)
                {
                    this.selectedKeyboardString = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(this.SelectedKeyboard));
                }
            }
        }

        public Color SelectedTextColor
        {
            get
            {
                if (string.IsNullOrEmpty(this.SelectedColorString))
                {
                    return Color.Default;
                }

                var val = this.colorTypeConverter.ConvertFromInvariantString(this.SelectedColorString);
                return (Color)val;
            }
        }

        public string SelectedColorString
        {
            get
            {
                return this.selectedColorString;
            }
            set
            {
                if (this.selectedColorString != value)
                {
                    this.selectedColorString = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(this.SelectedTextColor));
                }
            }
        }

        public NamedSize SelectedNamedSize
        {
            get
            {
                return this.selectedNamedSize;
            }
            set
            {
                if (this.selectedNamedSize != value)
                {
                    this.selectedNamedSize = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(this.SelectedFontSize));
                }
            }
        }

        public double SelectedFontSize
        {
            get 
            {
                var val = this.fontSizeConverter.ConvertFromInvariantString(this.SelectedNamedSize.ToString());
                return (double)val;
            }
        }

        public List<string> Keyboards
        {
            get
            {
                return this.keyboards;
            }
        }

        public List<string> TextColors
        {
            get
            {
                return this.textColors;
            }
        }

        public List<NamedSize> FontSizes
        {
            get
            {
                return this.fontSizes;
            }
        }
    }
}
