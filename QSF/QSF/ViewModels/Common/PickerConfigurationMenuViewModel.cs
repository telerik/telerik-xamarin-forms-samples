using QSF.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace QSF.ViewModels.Common
{
    public class PickerConfigurationMenuViewModel : ViewModels.ConfigurationViewModel
    {
        private static readonly ColorViewModel BlueColor = new ColorViewModel("Blue", "#0E88F2");
        private static readonly ColorViewModel OrangeColor = new ColorViewModel("Orange", "#FF6F00");
        private static readonly ColorViewModel YellowColor = new ColorViewModel("Yellow", "#FFAC3E");
        private static readonly ColorViewModel LightGrayColor = new ColorViewModel("Light Gray", "#F8F8F8");
        private static readonly ColorViewModel WhiteColor = new ColorViewModel("White", "#FFFFFF");
        private static readonly ColorViewModel DarkGrayColor = new ColorViewModel("Dark Gray", "#757575");
        private static readonly ColorViewModel BlackColor = new ColorViewModel("Black", "#000000");

        private bool isHeaderVisible = true;
        private ColorViewModel popupHeaderBackgroundColor;
        private ColorViewModel popupHeaderFontColor;
        private string confirmationButtonText;
        private string cancellationButtonText;
        private ColorViewModel selectedItemFontColor;
        private int selectedItemFontSize;
        private FontAttributes selectedItemFontAttribute;
        private ColorViewModel selectedItemBackgroundColor;
        private int spinnerItemFontSize;
        private FontAttributes spinnerItemFontAttribute;
        private ColorViewModel spinnerItemFontColor;
        private ColorViewModel spinnerItemBackgroundColor;
        private ColorViewModel confirmationButtonBackgroundColor;
        private ColorViewModel cancellationButtonBackgroundColor;

        public PickerConfigurationMenuViewModel()
        {
            this.PopupHeaderBackgroundColors = new ObservableCollection<ColorViewModel> { BlueColor, OrangeColor, YellowColor, LightGrayColor };
            this.PopupHeaderBackgroundColor = BlueColor;

            this.PopupHeaderFontColors = new ObservableCollection<ColorViewModel> { WhiteColor, DarkGrayColor, BlackColor };
            this.PopupHeaderFontColor = WhiteColor;

            this.ConfirmationButtonTexts = new ObservableCollection<string> { "OK", "\uE876" };
            this.ConfirmationButtonText = this.ConfirmationButtonTexts[0];

            this.CancellationButtonTexts = new ObservableCollection<string> { "Cancel", "\uE877" };
            this.CancellationButtonText = this.CancellationButtonTexts[0];

            this.SelectedItemFontColors = new ObservableCollection<ColorViewModel> { BlueColor, OrangeColor, YellowColor, DarkGrayColor };
            this.SelectedItemFontColor = BlueColor;

            this.SelectedItemFontSizes = new ObservableCollection<int> { 16, 18, 20 };
            this.SelectedItemFontSize = this.SelectedItemFontSizes[0];

            this.SelectedItemBackgroundColors = new ObservableCollection<ColorViewModel> { WhiteColor, LightGrayColor };
            this.SelectedItemBackgroundColor = LightGrayColor;

            this.SpinnerItemFontColors = new ObservableCollection<ColorViewModel> { DarkGrayColor, BlackColor };
            this.SpinnerItemFontColor = DarkGrayColor;

            this.ItemFontAttributes = new ObservableCollection<FontAttributes> { FontAttributes.Bold, FontAttributes.Italic, FontAttributes.None };
            this.SelectedItemFontAttribute = this.ItemFontAttributes[0];
            this.SpinnerItemFontAttribute = this.ItemFontAttributes[1];

            this.SpinnerItemFontSizes = new ObservableCollection<int> { 12, 14, 16 };
            this.SpinnerItemFontSize = this.SpinnerItemFontSizes[0];

            this.SpinnerItemBackgroundColors = new ObservableCollection<ColorViewModel> { WhiteColor, LightGrayColor };
            this.SpinnerItemBackgroundColor = WhiteColor;

            this.ConfirmationButtonBackgroundColors = new ObservableCollection<ColorViewModel> { LightGrayColor, WhiteColor };
            this.ConfirmationButtonBackgroundColor = LightGrayColor;

            this.CancellationButtonBackgroundColors = new ObservableCollection<ColorViewModel> { LightGrayColor, WhiteColor };
            this.CancellationButtonBackgroundColor = LightGrayColor;
        }

        public ObservableCollection<string> ConfirmationButtonTexts { get; }

        public ObservableCollection<string> CancellationButtonTexts { get; }

        public ObservableCollection<int> SelectedItemFontSizes { get; }

        public ObservableCollection<FontAttributes> ItemFontAttributes { get; }

        public ObservableCollection<int> SpinnerItemFontSizes { get; }

        public bool IsHeaderVisible
        {
            get
            {
                return this.isHeaderVisible;
            }
            set
            {
                if (this.isHeaderVisible != value)
                {
                    this.isHeaderVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ColorViewModel PopupHeaderBackgroundColor
        {
            get
            {
                return this.popupHeaderBackgroundColor;
            }
            set
            {
                if (this.popupHeaderBackgroundColor != value)
                {
                    this.popupHeaderBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ColorViewModel> PopupHeaderBackgroundColors { get; }

        public ColorViewModel PopupHeaderFontColor
        {
            get
            {
                return this.popupHeaderFontColor;
            }
            set
            {
                if (this.popupHeaderFontColor != value)
                {
                    this.popupHeaderFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ColorViewModel> PopupHeaderFontColors { get; }

        public string ConfirmationButtonText
        {
            get
            {
                return this.confirmationButtonText;
            }
            set
            {
                if (this.confirmationButtonText != value)
                {
                    this.confirmationButtonText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CancellationButtonText
        {
            get
            {
                return this.cancellationButtonText;
            }
            set
            {
                if (this.cancellationButtonText != value)
                {
                    this.cancellationButtonText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ColorViewModel SelectedItemFontColor
        {
            get
            {
                return this.selectedItemFontColor;
            }
            set
            {
                if (this.selectedItemFontColor != value)
                {
                    this.selectedItemFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ColorViewModel> SelectedItemFontColors { get; }

        public int SelectedItemFontSize
        {
            get
            {
                return this.selectedItemFontSize;
            }
            set
            {
                if (this.selectedItemFontSize != value)
                {
                    this.selectedItemFontSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FontAttributes SelectedItemFontAttribute
        {
            get
            {
                return this.selectedItemFontAttribute;
            }
            set
            {
                if (this.selectedItemFontAttribute != value)
                {
                    this.selectedItemFontAttribute = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ColorViewModel SelectedItemBackgroundColor
        {
            get
            {
                return this.selectedItemBackgroundColor;
            }
            set
            {
                if (this.selectedItemBackgroundColor != value)
                {
                    this.selectedItemBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ColorViewModel> SelectedItemBackgroundColors { get; }

        public int SpinnerItemFontSize
        {
            get
            {
                return this.spinnerItemFontSize;
            }
            set
            {
                if (this.spinnerItemFontSize != value)
                {
                    this.spinnerItemFontSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FontAttributes SpinnerItemFontAttribute
        {
            get
            {
                return this.spinnerItemFontAttribute;
            }
            set
            {
                if (this.spinnerItemFontAttribute != value)
                {
                    this.spinnerItemFontAttribute = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ColorViewModel SpinnerItemFontColor
        {
            get
            {
                return this.spinnerItemFontColor;
            }
            set
            {
                if (this.spinnerItemFontColor != value)
                {
                    this.spinnerItemFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ColorViewModel> SpinnerItemFontColors { get; }

        public ColorViewModel SpinnerItemBackgroundColor
        {
            get
            {
                return this.spinnerItemBackgroundColor;
            }
            set
            {
                if (this.spinnerItemBackgroundColor != value)
                {
                    this.spinnerItemBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ColorViewModel> SpinnerItemBackgroundColors { get; }

        public ColorViewModel ConfirmationButtonBackgroundColor
        {
            get
            {
                return this.confirmationButtonBackgroundColor;
            }
            set
            {
                if (this.confirmationButtonBackgroundColor != value)
                {
                    this.confirmationButtonBackgroundColor = value;
                    this.OnPropertyChanged();
                }

            }
        }
        public ObservableCollection<ColorViewModel> ConfirmationButtonBackgroundColors { get; }

        public ColorViewModel CancellationButtonBackgroundColor
        {
            get
            {
                return this.cancellationButtonBackgroundColor;
            }
            set
            {
                if (this.cancellationButtonBackgroundColor != value)
                {
                    this.cancellationButtonBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ColorViewModel> CancellationButtonBackgroundColors { get; }
    }
}
