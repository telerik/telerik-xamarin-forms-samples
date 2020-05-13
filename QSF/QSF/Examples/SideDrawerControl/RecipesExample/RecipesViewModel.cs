using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using QSF.ViewModels;
using Telerik.XamarinForms.DataControls.ListView;

namespace QSF.Examples.SideDrawerControl.RecipesExample
{
    public class RecipesViewModel : ExampleViewModel
    {
        private string selectedCategory;
        private Func<object, bool> filterCondition;

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

        public Func<object, bool> FilterCondition
        {
            get
            {
                return this.filterCondition;
            }
            private set
            {
                if (this.filterCondition != value)
                {
                    this.filterCondition = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Categories { get; private set; }

        public ObservableCollection<Recipe> Recipes { get; private set; }

        public RecipesViewModel()
        {
            this.Recipes = new ObservableCollection<Recipe>
            {
                new Recipe("Breakfast0.png", "Breakfast", "kkendra", "Breakfast"),
                new Recipe("Breakfast1.png", "Breakfast", "diddo", "Breakfast"),
                new Recipe("Breakfast2.png", "Breakfast", "kkendra", "Breakfast"),
                new Recipe("Breakfast3.png", "Breakfast", "diddo", "Breakfast"),
                new Recipe("Sandwich0.png", "Sandwich", "by diddo", "Sandwiches"),
                new Recipe("Sandwich1.png", "Sandwich", "by diddo", "Sandwiches"),
                new Recipe("Sandwich2.png", "Sandwich", "by kkendra", "Sandwiches"),
                new Recipe("Sandwich3.png", "Sandwich", "by kkendra", "Sandwiches"),
                new Recipe("Desserts0.png", "Dessert", "by diddo", "Desserts"),
                new Recipe("Desserts2.png", "Dessert", "by diddo", "Desserts"),
                new Recipe("Desserts3.png", "Dessert", "by kkendra", "Desserts"),
                new Recipe("Desserts1.png", "Dessert", "by kkendra", "Desserts"),
                new Recipe("Paleo0.png", "Paleo", "by kkendra", "Paleo"),
                new Recipe("Paleo2.png", "Paleo", "by diddo", "Paleo"),
                new Recipe("Paleo3.png", "Paleo", "by kkendra", "Paleo"),
                new Recipe("Cocktails0.png", "Cocktail", "by kkendra", "Cocktails"),
                new Recipe("Cocktails2.png", "Cocktail", "by kkendra", "Cocktails"),
                new Recipe("Cocktails3.png", "Cocktail", "by diddo", "Cocktails"),
                new Recipe("Cocktails1.png", "Cocktail", "by diddo", "Cocktails")
            };
            this.Categories = new ObservableCollection<string>
            {
                "Breakfast",
                "Sandwiches",
                "Desserts",
                "Paleo",
                "Cocktails"
            };
            this.SelectedCategory = this.Categories.FirstOrDefault();
        }

        private void OnCategoryChanged()
        {
            var filterCategory = this.selectedCategory;

            this.FilterCondition = value =>
            {
                var recipe = (Recipe)value;

                return recipe.Category == filterCategory;
            };
        }
    }
}
