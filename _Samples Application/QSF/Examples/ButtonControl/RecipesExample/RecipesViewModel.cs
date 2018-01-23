using System.Windows.Input;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ButtonControl.RecipesExample
{
    public class RecipesViewModel : ViewModelBase
    {
        private string category;
        private string popularity;
        private string ingredient;

        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                if (this.category != value)
                {
                    this.category = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Popularity
        {
            get
            {
                return this.popularity;
            }
            set
            {
                if (this.popularity != value)
                {
                    this.popularity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Ingredient
        {
            get
            {
                return this.ingredient;
            }
            set
            {
                if (this.ingredient != value)
                {
                    this.ingredient = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand SelectByCategoryCommand { get; private set; }
        public ICommand SelectByPopularityCommand { get; private set; }
        public ICommand SelectByIngredientCommand { get; private set; }

        public RecipesViewModel()
        {
            this.Category = "<none>";
            this.Popularity = "<none>";
            this.Ingredient = "<none>";

            this.SelectByCategoryCommand = new Command<string>(category => this.Category = category);
            this.SelectByPopularityCommand = new Command<string>(popularity => this.Popularity = popularity);
            this.SelectByIngredientCommand = new Command<string>(ingredient => this.Ingredient = ingredient);
        }
    }
}
