using QSF.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.Examples.TemplatedPickerControl.FirstLookExample
{
    public class ViewModel : ExampleViewModel
    {
        private ColorViewModel selectedColor;
        private SizeViewModel selectedSize;
        private Color defaultDeselectedColor;
        private Color defaultSelectedColor;
        private Color defaultDeselectedTextColor;
        private Color defaultSelectedTextColor;
        private string highlightedValue;
        private string selectedValue;
        private bool isSelectedValue;

        public ViewModel()
        {
            this.defaultDeselectedColor = Color.FromHex("#EAEAEA");
            this.defaultSelectedColor = Color.FromHex("#919191");
            this.defaultDeselectedTextColor = Color.Black;
            this.defaultSelectedTextColor = Color.White;
            this.HighlightedValue = string.Empty;
            this.SelectedValue = string.Empty;
            this.IsSelectedValue = false;

            this.XS = new SizeViewModel("XS");
            this.S = new SizeViewModel("S");
            this.M = new SizeViewModel("M");
            this.L = new SizeViewModel("L");
            this.XL = new SizeViewModel("XL");
            this.XXL = new SizeViewModel("XXL");

            this.Blue = new ColorViewModel("Blue", "#007AFF");
            this.Yellow = new ColorViewModel("Yellow", "#F3C163");
            this.Purple = new ColorViewModel("Purple", "#CE3A6D");
            this.Orange = new ColorViewModel("Orange", "#EE6C4D");
            this.LightGray = new ColorViewModel("LightGray", "#EAEAEA");
            this.DarkGray = new ColorViewModel("DarkGray", "#919191");

            this.SelectSizeCommand = new Command<SizeViewModel>(
                execute: (SizeViewModel arg) =>
                {
                    if (this.SelectedSize != null)
                    {
                        this.SelectedSize.BackgroundColor = this.defaultDeselectedColor;
                        this.SelectedSize.TextColor = this.defaultDeselectedTextColor;
                    }
                    arg.BackgroundColor = this.defaultSelectedColor;
                    arg.TextColor = this.defaultSelectedTextColor;
                    this.SelectedSize = arg;

                    if (this.HighlightedValue.Contains(", "))
                    {
                        this.HighlightedValue = arg.Name + this.HighlightedValue.Substring(this.HighlightedValue.IndexOf(','));
                    }
                    else
                    {
                        this.HighlightedValue = arg.Name + ", ";
                    }
                });
            this.SelectColorCommand = new Command<ColorViewModel>(
                execute: (ColorViewModel arg) =>
                {
                    if (this.SelectedColor != null)
                    {
                        this.SelectedColor.BorderColor = Color.Transparent;
                    }
                    arg.BorderColor = arg.Color;
                    this.SelectedColor = arg;

                    this.HighlightedValue = this.HighlightedValue.Substring(0, this.HighlightedValue.IndexOf(',') + 2) + arg.Name;
                });
            this.AcceptCommand = new Command(Accept);
            this.CancelCommand = new Command(Cancel);

            this.SelectSizeCommand.Execute(this.XS);
            this.SelectColorCommand.Execute(this.Blue);
        }

        public ColorViewModel Blue { get; private set; }
        public ColorViewModel Yellow { get; private set; }
        public ColorViewModel Purple { get; private set; }
        public ColorViewModel Orange { get; private set; }
        public ColorViewModel LightGray { get; private set; }
        public ColorViewModel DarkGray { get; private set; }
        public ColorViewModel SelectedColor
        {
            get
            {
                return this.selectedColor;
            }
            set
            {
                if (this.selectedColor != value)
                {
                    this.selectedColor = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public ColorViewModel LastAcceptedColor { get; set; }

        public SizeViewModel XS { get; private set; }
        public SizeViewModel S { get; private set; }
        public SizeViewModel M { get; private set; }
        public SizeViewModel L { get; private set; }
        public SizeViewModel XL { get; private set; }
        public SizeViewModel XXL { get; private set; }
        public SizeViewModel SelectedSize
        {
            get
            {
                return this.selectedSize;
            }
            set
            {
                if (this.selectedSize != value)
                {
                    this.selectedSize = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public SizeViewModel LastAcceptedSize { get; set; }

        public ICommand SelectColorCommand { get; set; }
        public ICommand SelectSizeCommand { get; set; }
        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string HighlightedValue
        {
            get
            {
                return this.highlightedValue;
            }
            set
            {
                if (this.highlightedValue != value)
                {
                    this.highlightedValue = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string SelectedValue
        {
            get
            {
                return this.selectedValue;
            }
            set
            {
                if (this.selectedValue != value)
                {
                    this.selectedValue = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public bool IsSelectedValue
        {
            get
            {
                return this.isSelectedValue;
            }
            set
            {
                if (this.isSelectedValue != value)
                {
                    this.isSelectedValue = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void Accept()
        {
            if (!this.IsSelectedValue)
            {
                this.IsSelectedValue = true;
            }
            this.SelectedValue = this.HighlightedValue;
            this.LastAcceptedSize = this.SelectedSize;
            this.LastAcceptedColor = this.SelectedColor;
        }

        private void Cancel()
        {
            if (this.SelectedValue != string.Empty)
            {
                this.HighlightedValue = this.SelectedValue;
                this.SelectSizeCommand.Execute(this.LastAcceptedSize);
                this.SelectColorCommand.Execute(this.LastAcceptedColor);
            }
            else
            {
                this.SelectSizeCommand.Execute(this.XS);
                this.SelectColorCommand.Execute(this.Blue);
            }
        }
    }
}
