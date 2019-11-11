using QSF.ViewModels;

namespace QSF.Examples.TabViewControl.RestaurantMenuExample
{
    public class RestaurantSectionViewModel : ExampleViewModel
    {
        private bool isSelected;
        private string name;
        private string icon;
        private string normalIcon;
        private string selectedIcon;

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

        public string Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                if (this.icon != value)
                {
                    this.icon = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged();
                    this.OnIsSelectedChanged();
                }
            }
        }

        public RestaurantSectionViewModel(string name, string normalIcon, string selectedIcon)
        {
            this.name = name;
            this.icon = normalIcon;
            this.normalIcon = normalIcon;
            this.selectedIcon = selectedIcon;
        }

        private void OnIsSelectedChanged()
        {
            this.Icon = this.isSelected ? this.selectedIcon : this.normalIcon;
        }
    }
}
