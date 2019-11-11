using System.Collections.ObjectModel;

namespace QSF.Examples.TabViewControl.RestaurantMenuExample
{
    public class RestaurantExploreSectionViewModel : RestaurantSectionViewModel
    {
        public ObservableCollection<RestaurantMenuItem> BreakfastItems { get; private set; }
        public ObservableCollection<RestaurantMenuItem> MainItems { get; private set; }
        public ObservableCollection<RestaurantMenuItem> DessertItems { get; private set; }
        public ObservableCollection<RestaurantMenuItem> DrinksItems { get; private set; }

        public RestaurantExploreSectionViewModel(string name, string normalIcon, string selectedIcon)
            : base(name, normalIcon, selectedIcon)
        {
            this.BreakfastItems = new ObservableCollection<RestaurantMenuItem>
            {
                new RestaurantMenuItem("French Bread", "TabView_Restaurant_FrenchBread.png"),
                new RestaurantMenuItem("Healthy Breakfast", "TabView_Restaurant_HealthyBreakfast.png"),
                new RestaurantMenuItem("Indian", "TabView_Restaurant_Indian.png"),
                new RestaurantMenuItem("Sausages", "TabView_Restaurant_Sausages.png")
            };
            this.MainItems = new ObservableCollection<RestaurantMenuItem>
            {
                new RestaurantMenuItem("Bulgarian Tortilla", "TabView_Restaurant_BulgarianTortilla.png"),
                new RestaurantMenuItem("Mediterranean Fish", "TabView_Restaurant_MediterraneanFish.png"),
                new RestaurantMenuItem("Pork With Blueberries", "TabView_Restaurant_PorkWithBlueberries.png"),
                new RestaurantMenuItem("Spinach Potato Nest Bites", "TabView_Restaurant_SpinachPotatoNestBites.png")
            };
            this.DessertItems = new ObservableCollection<RestaurantMenuItem>
            {
                new RestaurantMenuItem("American Pancakes", "TabView_Restaurant_AmericanPancakes.png"),
                new RestaurantMenuItem("Belgian Chocolate", "TabView_Restaurant_BelgianChocolate.png"),
                new RestaurantMenuItem("Blueberry Waffle", "TabView_Restaurant_BlueberryWaffle.png"),
                new RestaurantMenuItem("Tiramisu", "TabView_Restaurant_Tiramisu.png")
            };
            this.DrinksItems = new ObservableCollection<RestaurantMenuItem>
            {
                new RestaurantMenuItem("Ale", "TabView_Restaurant_Ale.png"),
                new RestaurantMenuItem("Lager", "TabView_Restaurant_Lager.png"),
                new RestaurantMenuItem("White Wine", "TabView_Restaurant_WhiteWine.png"),
                new RestaurantMenuItem("Wine", "TabView_Restaurant_Wine.png")
            };
        }
    }
}
