using QSF.ViewModels;

namespace QSF.Examples.AutoCompleteViewControl.RemoteSearchExample
{
    public class DetailsViewModel : ConfigurationViewModel
    {
        public DetailsViewModel(Movie selectedMovie)
        {
            this.SelectedMovie = selectedMovie;
            this.Title = selectedMovie.Title;
        }

        public Movie SelectedMovie { get; set; }
    }
}