using System.ComponentModel;
using QSF.ViewModels;

namespace QSF.Examples.TabViewControl.RestaurantMenuExample
{
    public class RestaurantMenuViewModel : ExampleViewModel
    {
        public RestaurantExploreSectionViewModel Explore { get; private set; }
        public RestaurantFlatSectionViewModel Saved { get; private set; }
        public RestaurantFlatSectionViewModel ShoppingList { get; private set; }

        public RestaurantMenuViewModel()
        {
            this.Explore = new RestaurantExploreSectionViewModel("Explore", "TabView_Restaurant_Explore.png", "TabView_Restaurant_Explore_Selected.png");
            this.Saved = new RestaurantFlatSectionViewModel("Saved", "TabView_Restaurant_Saved.png", "TabView_Restaurant_Saved_Selected.png");
            this.ShoppingList = new RestaurantFlatSectionViewModel("Shopping list", "TabView_Restaurant_ShoppingList.png", "TabView_Restaurant_ShoppingList_Selected.png");

            this.AttachIsSavedListeners();

            this.Explore.BreakfastItems[1].IsSaved = true;
            this.Explore.MainItems[0].IsSaved = true;
            this.Explore.DessertItems[0].IsSaved = true;

            this.ShoppingList.Items.Add(this.Explore.BreakfastItems[1]);
            this.ShoppingList.Items.Add(this.Explore.MainItems[1]);
        }

        private void AttachIsSavedListeners()
        {
            foreach (RestaurantMenuItem item in this.Explore.BreakfastItems)
            {
                item.PropertyChanged += this.OnItemPropertyChanged;
            }

            foreach (RestaurantMenuItem item in this.Explore.MainItems)
            {
                item.PropertyChanged += this.OnItemPropertyChanged;
            }

            foreach (RestaurantMenuItem item in this.Explore.DessertItems)
            {
                item.PropertyChanged += this.OnItemPropertyChanged;
            }

            foreach (RestaurantMenuItem item in this.Explore.DrinksItems)
            {
                item.PropertyChanged += this.OnItemPropertyChanged;
            }
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RestaurantMenuItem.IsSaved))
            {
                RestaurantMenuItem item = (RestaurantMenuItem)sender;
                if (item.IsSaved)
                {
                    this.Saved.Items.Add(item);
                }
                else
                {
                    this.Saved.Items.Remove(item);
                }
            }
        }
    }
}
