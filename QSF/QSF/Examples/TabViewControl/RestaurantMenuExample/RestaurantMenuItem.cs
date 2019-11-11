using QSF.ViewModels;

namespace QSF.Examples.TabViewControl.RestaurantMenuExample
{
    public class RestaurantMenuItem : BindableBase
    {
        private string name;
        private string image;
        private bool isSaved;

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

        public bool IsSaved
        {
            get
            {
                return this.isSaved;
            }
            set
            {
                if (this.isSaved != value)
                {
                    this.isSaved = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public RestaurantMenuItem(string name, string image)
        {
            this.name = name;
            this.image = image;
        }
    }
}
