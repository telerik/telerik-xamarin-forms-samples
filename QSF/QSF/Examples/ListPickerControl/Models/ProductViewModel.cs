using QSF.ViewModels;

namespace QSF.Examples.ListPickerControl.Models
{
    public class ProductViewModel : BindableBase
    {
        private string name;
        private string image;
        private string description;
        private double price;

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

        public string Image
        {
            get
            {
                return this.image;
            }
            set
            {
                if (this.image != value)
                {
                    this.image = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.OnPropertyChanged();
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
    }
}
