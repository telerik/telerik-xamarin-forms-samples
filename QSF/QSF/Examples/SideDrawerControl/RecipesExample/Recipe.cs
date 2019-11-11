namespace QSF.Examples.SideDrawerControl.RecipesExample
{
    public class Recipe
    {
        public string Image { get; private set; }
        public string Author { get; private set; }
        public string Title { get; private set; }
        public string Category { get; private set; }

        public Recipe(string image, string title, string autor, string category)
        {
            this.Image = image;
            this.Title = title;
            this.Author = autor;
            this.Category = category;
        }
    }
}
