using QSF.Services.Toast;
using QSF.ViewModels;
using System.Windows.Input;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace QSF.Examples.ListPickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private string genreKind;

        public FirstLookViewModel()
        {
            this.Genres = new ObservableItemCollection<string>()
            {
                "Alternative Rock",
                "New Wave",
                "Jazz",
                "Pop Rock",
                "Punk Rock",
                "Progressive House",
            };

            this.RecentlyPlayed = new ObservableItemCollection<Music>()
            {
                new Music("Nirvana","Smells Like Teen Spirit", Color.FromHex("#F3C163")),
                new Music("Queen","I Want To Break Free", Color.FromHex("#007AFF")),
                new Music("Depeche Mode","Personal Jesus", Color.FromHex("#CE3A6D")),
                new Music("The Police","Personal Jesus", Color.FromHex("#CE3A6D")),
                new Music("Green Day ","Basket Case", Color.FromHex("#F3C163")),
                new Music("David Guetta ft. Ne-Yo, Akon","Play Hard", Color.FromHex("#CE3A6D")),
                new Music("Louis Armstrong","What a wonderful world", Color.FromHex("#007AFF")),
                new Music("Radiohead ","Creep", Color.FromHex("#F3C163")),
                new Music("The Clash","Should I Stay or Should I Go ", Color.FromHex("#007AFF")),
                new Music("Blondie","Call Me", Color.FromHex("#CE3A6D")),
                new Music("Calvin Harris","Call Me", Color.FromHex("#CE3A6D")),
                new Music("Ray Charles ","I got a woman", Color.FromHex("#007AFF")),
                new Music("Red Hot Chili Peppers","Aeroplane", Color.FromHex("#F3C163")),
                new Music("The Beatles","Help", Color.FromHex("#007AFF")),
            };

            this.PlayGenreCommand = new Command(this.PlayGenre, this.CanExecutePlayGenre);
            ((Command)this.PlayGenreCommand).ChangeCanExecute();
        }

        public ObservableItemCollection<string> Genres { get; set; }

        public ObservableItemCollection<Music> RecentlyPlayed { get; set; }

        public string GenreKind
        {
            get
            {
                return this.genreKind;
            }
            set
            {
                if (this.genreKind != value)
                {
                    this.genreKind = value;
                    ((Command)this.PlayGenreCommand).ChangeCanExecute();
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand PlayGenreCommand
        {
            get;
            set;
        }

        private void PlayGenre()
        {
            if (this.genreKind != null)
            {
                var toastService = DependencyService.Get<IToastMessageService>();
                toastService.ShortAlert($"{this.GenreKind} playing");
            }
        }

        private bool CanExecutePlayGenre()
        {
            return this.genreKind != null;
        }
    }
}
