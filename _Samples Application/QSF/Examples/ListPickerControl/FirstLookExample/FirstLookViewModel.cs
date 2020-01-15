using System.Collections.ObjectModel;
using System.Windows.Input;
using QSF.Services.Toast;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ListPickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private ProductViewModel product;
        private ColorViewModel color;
        private double size;
        private double quantity;
        private double price;

        public FirstLookViewModel()
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

            this.Color = this.Colors[0];
            this.Size = this.Sizes[7];
            this.Quantity = 1;
            this.Price = 230;

            this.AddToCartCommand = new Command(this.OnAddToCartCommand);
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

        public ICommand AddToCartCommand { get; set; }

        private void OnQuantityChanged()
        {
            this.Price = this.Quantity * this.Product.Price;
        }

        private void OnAddToCartCommand()
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            var toastMessage = $"The product '{this.Product.Name}' added to cart.";

            toastService.ShortAlert(toastMessage);
        }
    }
}