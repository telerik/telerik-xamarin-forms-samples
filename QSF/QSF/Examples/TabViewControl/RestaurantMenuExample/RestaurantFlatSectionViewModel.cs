using System.Collections.ObjectModel;

namespace QSF.Examples.TabViewControl.RestaurantMenuExample
{
    public class RestaurantFlatSectionViewModel : RestaurantSectionViewModel
    {
        public ObservableCollection<RestaurantMenuItem> Items { get; private set; }

        public RestaurantFlatSectionViewModel(string name, string normalIcon, string selectedIcon)
            : base(name, normalIcon, selectedIcon)
        {
            this.Items = new ObservableCollection<RestaurantMenuItem>();
        }
    }
}
