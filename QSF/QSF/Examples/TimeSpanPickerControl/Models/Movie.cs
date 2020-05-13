using System;
using QSF.ViewModels;

namespace QSF.Examples.TimeSpanPickerControl.Models
{
    public class Movie : BindableBase
    {
        public Movie(string title, string actors, string description, TimeSpan time, Genre genre)
        {
            this.Title = title;
            this.Actors = actors;
            this.Description = description;
            this.Time = time;
            this.Genre = genre;
        }

        public string Title { get; }

        public string Actors { get; }

        public string Description { get; }

        public TimeSpan Time { get; }

        public Genre Genre { get; }
    }
}
