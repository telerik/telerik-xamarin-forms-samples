namespace QSF.Examples.AutoCompleteViewControl.RemoteSearchExample
{
    public class Movie
    {
        public Movie(string title, string actors, string description, Genre genre)
        {
            this.Title = title;
            this.Actors = actors;
            this.Description = description;
            this.Genre = genre;
        }

        public string Title { get; set; }

        public string Actors { get; set; }

        public string Description { get; set; }

        public Genre Genre { get; set; }
    }
}
