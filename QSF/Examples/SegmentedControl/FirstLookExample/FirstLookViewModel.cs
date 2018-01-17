using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.SegmentedControl.FirstLookExample
{
    public class FirstLookViewModel : ViewModelBase
    {
        private readonly IList<MenuItem> menuItems;
        private int selectedIndex;
        private string selectedCategory;

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                if (this.selectedIndex != value)
                {
                    this.selectedIndex = value;
                    this.OnPropertyChanged();
                    this.OnSelectionChanged();
                }
            }
        }

        public string SelectedCategory
        {
            get
            {
                return this.selectedCategory;
            }
            set
            {
                if (this.selectedCategory != value)
                {
                    this.selectedCategory = value;
                    this.OnPropertyChanged();
                    this.OnCategoryChanged();
                }
            }
        }

        public ObservableCollection<string> Categories { get; private set; }
        public ObservableCollection<MenuItem> MenuItems { get; private set; }
        public ObservableCollection<ImageSource> LargeImages { get; private set; }
        public ObservableCollection<ImageSource> SmallImages { get; private set; }

        public FirstLookViewModel()
        {
            this.menuItems = new[]
            {
                new MenuItem("Dinner", "Filet Mignon", 26),
                new MenuItem("Dinner", "Sirloin Steak", 28),
                new MenuItem("Dinner", "Fried Chicken", 21),
                new MenuItem("Drinks", "Coca-Cola", 2),
                new MenuItem("Drinks", "Mineral Water", 1.5),
                new MenuItem("Drinks", "Orange Juice", 3),
                new MenuItem("Snacks", "Crackers", 9),
                new MenuItem("Snacks", "Cheese Burger", 12),
                new MenuItem("Snacks", "Energy Bar", 5)
            };
            this.MenuItems = new ObservableCollection<MenuItem>();
            this.Categories = new ObservableCollection<string>
            {
                "Dinner",
                "Drinks",
                "Snacks"
            };
            this.LargeImages = new ObservableCollection<ImageSource>
            {
                CreateImage("Segmented_Dinner.png"),
                CreateImage("Segmented_Drinks.png"),
                CreateImage("Segmented_Snacks.png")
            };
            this.SmallImages = new ObservableCollection<ImageSource>
            {
                CreateImage("Segmented_Dinner_Small.png"),
                CreateImage("Segmented_Drinks_Small.png"),
                CreateImage("Segmented_Snacks_Small.png")
            };
            this.SelectedCategory = this.Categories.FirstOrDefault();
        }

        private void OnSelectionChanged()
        {
            this.SelectedCategory = this.Categories[this.SelectedIndex];
        }

        private void OnCategoryChanged()
        {
            var selectedItems = this.menuItems.Where(menuItem =>
                menuItem.Category == this.SelectedCategory);

            this.MenuItems.Clear();

            foreach (var menuItem in selectedItems)
            {
                this.MenuItems.Add(menuItem);
            }
        }

        private static ImageSource CreateImage(string name)
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                name = string.Format("Assets/{0}", name);
            }

            return ImageSource.FromFile(name);
        }
    }
}
