using QSF.Examples.TimeSpanPickerControl.Models;
using QSF.Services.Toast;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace QSF.Examples.TimeSpanPickerControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        private readonly List<Genre> availableGenres;
        private readonly List<Movie> availableMovies;
        private TimeSpan? selectedTime;
        private string searchLabelText;

        public CustomizationViewModel()
        {
            this.availableGenres = new List<Genre>()
            {
                new Genre("Crime", "#9760FE"),
                new Genre("Drama", "#B73562"),
                new Genre("Epics", "#FE6078")
            };
            this.availableMovies = new List<Movie>()
            {
                new Movie("The Godfather",
                          "Al Pacino, Marlon Brando, James Caan", 
                          "The Godfather is a 1972 American crime film directed by Francis Ford Coppola...",
                          new TimeSpan(0, 45, 0),
                          this.availableGenres[0]),
                new Movie("The Shawshank Redemption", 
                          "Morgarn Freeman, Rita Hayworth, Tim Robbins",
                          "The Shawshank Redemption is a 1994 American drama film written and directed by Frank Darabont...",
                          new TimeSpan(1, 30, 0),
                          this.availableGenres[1]),
                new Movie("Star Wars",
                          "Harrison Ford, Carrie Fisher, Mark Hamill", 
                          "Star Wars is a 1977 American epic space opera film written and directed by George Lucas...",
                          new TimeSpan(2, 15, 0),
                          this.availableGenres[2]),
                new Movie("The Lord of the Rings",
                          "Elijah Wood, Ian McKellen, Orlando Bloom",
                          "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
                          new TimeSpan(3, 45, 0),
                          this.availableGenres[2]),
                new Movie("Rocky",
                          "Sylvester Stallone, Talia Shire, Burt Young",
                          "A small-time boxer gets a supremely rare chance to fight a heavy-weight champion in a bout in which he strives to go the   distance     for     his self-respect.",
                          new TimeSpan(4, 30, 0),
                          this.availableGenres[1])
            };
            this.Movies = new ObservableCollection<Movie>(this.availableMovies);
            this.MaximumTime = new TimeSpan(3, 59, 59);
            this.FindMoviesCommand = new Command(this.FindMovies, this.CanFindMovies);
            this.SearchLabelText = "All Movies:";
        }

        public TimeSpan MaximumTime { get; }

        public TimeSpan? SelectedTime
        {
            get
            {
                return this.selectedTime;
            }
            set
            {
                if (this.selectedTime != value)
                {
                    this.selectedTime = value;
                    this.OnPropertyChanged();
                    this.FindMoviesCommand.ChangeCanExecute();
                }
            }
        }

        public string TodayCinemaProgram
        {
            get
            {
                return DateTime.Today.ToString("d");
            }
        }

        public ObservableCollection<Movie> Movies { get; }

        public string SearchLabelText
        {
            get
            {
                return this.searchLabelText;
            }
            private set
            {
                if (this.searchLabelText != value)
                {
                    this.searchLabelText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Command FindMoviesCommand { get; }

        private void FindMovies()
        {
            this.SearchLabelText = "Movies Found:";
            this.Movies.Clear();

            var selectedTime = this.SelectedTime.GetValueOrDefault();

            foreach (var movie in this.availableMovies)
            {
                if (movie.Time >= selectedTime)
                {
                    this.Movies.Add(movie);
                }
            }

            int hours = selectedTime.Hours;
            int minutes = selectedTime.Minutes;
            var toastMessage = $"Movies starting in: {hours}h {minutes} min displayed.";
            var toastService = DependencyService.Get<IToastMessageService>();

            toastService.ShortAlert(toastMessage);
        }

        private bool CanFindMovies()
        {
            return this.SelectedTime != null;
        }
    }
}
