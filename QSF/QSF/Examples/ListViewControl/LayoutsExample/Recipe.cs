﻿namespace QSF.Examples.ListViewControl.LayoutsExample
{
    public class Recipe
    {
        public string Image { get; private set; }
        public string Author { get; private set; }
        public string Title { get; private set; }
        public string Group { get; private set; }

        public Recipe(string image, string title, string autor, string group)
        {
            this.Image = image;
            this.Title = title;
            this.Author = autor;
            this.Group = group;
        }
    }
}
