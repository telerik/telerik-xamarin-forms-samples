using System.Collections.ObjectModel;
using System.Threading.Tasks;
using QSF.Examples.ListPickerControl.Models;
using QSF.Services.Toast;
using QSF.ViewModels;
using QSF.ViewModels.Common;
using Xamarin.Forms;

namespace QSF.Examples.ListPickerControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private const int defaultColorIndex = 0;
        private const int defaultSizeIndex = 7;

        private ProductViewModel product;
        private ColorViewModel color;
        private double size;
        private double quantity;
        private double price;
        private PickerConfigurationMenuViewModel timePickerConfigurationViewModel;
        private bool isHeaderVisible;
        private Color popupHeaderBackgroundColor;
        private Color popupHeaderFontColor;
        private string confirmationButtonText;
        private string cancellationButtonText;
        private Color selectedItemFontColor;
        private int selectedItemFontSize;
        private FontAttributes selectedItemFontAttribute;
        private Color selectedItemBackgroundColor;
        private int spinnerItemFontSize;
        private FontAttributes spinnerItemFontAttribute;
        private Color spinnerItemFontColor;
        private Color spinnerItemBackgroundColor;
        private Color confirmationButtonBackgroundColor;
        private Color cancellationButtonBackgroundColor;

        public ConfigurationViewModel()
        {
            this.Product = new ProductViewModel
            {
                Name = "Kuako Womens Trainers",
                Description = "Comfort running shoes using air cushion design.",
                Image = "Picker_Demo1_Header.png",
                Price = 230
            };

            this.Colors = new ObservableCollection<ColorViewModel>
            {
                new ColorViewModel("Purple", "#AE3C63"),
                new ColorViewModel("Black", "#000000"),
                new ColorViewModel("Azure Blue", "#316FC8"),
                new ColorViewModel("Grey", "#CCCCCC"),
                new ColorViewModel("Light Blue", "#6FA2DC"),
                new ColorViewModel("Light Green", "#9CCC65"),
                new ColorViewModel("Blue", "#42A5F5"),
                new ColorViewModel("Yellow", "#FFEE58"),
                new ColorViewModel("Red", "#EE534F"),
                new ColorViewModel("Orange", "#FFA726"),
                new ColorViewModel("Brown", "#8D6E63"),
            };

            this.Sizes = new ObservableCollection<double>
            {
                5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10
            };

            this.AddToCartCommand = new Command(this.ExecuteAddToCartCommand, this.CanExecuteAddToCartCommand);

            this.Color = this.Colors[defaultColorIndex];
            this.Size = this.Sizes[defaultSizeIndex];
            this.Quantity = 1;
            this.timePickerConfigurationViewModel = new PickerConfigurationMenuViewModel();
        }

        public ProductViewModel Product
        {
            get
            {
                return this.product;
            }
            set
            {
                if (this.product != value)
                {
                    this.product = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ColorViewModel Color
        {
            get
            {
                return this.color;
            }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double Size
        {
            get
            {
                return this.size;
            }
            set
            {
                if (this.size != value)
                {
                    this.size = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double Quantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                if (this.quantity != value)
                {
                    this.quantity = value;
                    this.OnPropertyChanged();
                    this.OnQuantityChanged();
                }
            }
        }

        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                if (this.price != value)
                {
                    this.price = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ColorViewModel> Colors { get; }

        public ObservableCollection<double> Sizes { get; }

        public Command AddToCartCommand { get; }

        public override bool HasConfiguration
        {
            get
            {
                return true;
            }
        }

        public override bool IsPopupHintOpen => true;

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

        public Color PopupHeaderBackgroundColor
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

        public Color PopupHeaderFontColor
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

        public Color SelectedItemFontColor
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

        public Color ConfirmationButtonBackgroundColor
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

        public Color CancellationButtonBackgroundColor
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

        public Color SelectedItemBackgroundColor
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

        public Color SpinnerItemFontColor
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

        public Color SpinnerItemBackgroundColor
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

        internal override void OnAppearing()
        {
            base.OnAppearing();

            this.IsHeaderVisible = this.timePickerConfigurationViewModel.IsHeaderVisible;
            this.PopupHeaderBackgroundColor = this.timePickerConfigurationViewModel.PopupHeaderBackgroundColor.Color;
            this.PopupHeaderFontColor = this.timePickerConfigurationViewModel.PopupHeaderFontColor.Color;
            this.ConfirmationButtonText = this.timePickerConfigurationViewModel.ConfirmationButtonText;
            this.CancellationButtonText = this.timePickerConfigurationViewModel.CancellationButtonText;
            this.SelectedItemFontColor = this.timePickerConfigurationViewModel.SelectedItemFontColor.Color;
            this.SelectedItemFontSize = this.timePickerConfigurationViewModel.SelectedItemFontSize;
            this.SelectedItemFontAttribute = this.timePickerConfigurationViewModel.SelectedItemFontAttribute;
            this.SelectedItemBackgroundColor = this.timePickerConfigurationViewModel.SelectedItemBackgroundColor.Color;
            this.SpinnerItemFontSize = this.timePickerConfigurationViewModel.SpinnerItemFontSize;
            this.SpinnerItemFontAttribute = this.timePickerConfigurationViewModel.SpinnerItemFontAttribute;
            this.SpinnerItemFontColor = this.timePickerConfigurationViewModel.SpinnerItemFontColor.Color;
            this.SpinnerItemBackgroundColor = this.timePickerConfigurationViewModel.SpinnerItemBackgroundColor.Color;
            this.ConfirmationButtonBackgroundColor = this.timePickerConfigurationViewModel.ConfirmationButtonBackgroundColor.Color;
            this.CancellationButtonBackgroundColor = this.timePickerConfigurationViewModel.CancellationButtonBackgroundColor.Color;
        }

        protected override Task NavigateToConfigurationOverride()
        {
            return this.NavigationService.NavigateToConfigurationAsync(this.timePickerConfigurationViewModel);
        }


        private void OnQuantityChanged()
        {
            this.Price = this.Quantity * this.Product.Price;

            this.AddToCartCommand.ChangeCanExecute();
        }

        private bool CanExecuteAddToCartCommand()
        {
            return this.Quantity > 0;
        }

        private void ExecuteAddToCartCommand()
        {
            this.Color = this.Colors[defaultColorIndex];
            this.Size = this.Sizes[defaultSizeIndex];
            this.Quantity = 0;

            var toastService = DependencyService.Get<IToastMessageService>();
            var toastMessage = $"The product '{this.Product.Name}' added to cart.";

            toastService.ShortAlert(toastMessage);
        }
    }
}
