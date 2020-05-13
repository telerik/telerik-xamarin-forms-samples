using QSF.Examples.TimeSpanPickerControl.Models;
using QSF.Services.Toast;
using QSF.ViewModels;
using QSF.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QSF.Examples.TimeSpanPickerControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private readonly List<Genre> availableGenres;
        private readonly List<Movie> availableMovies;
        private TimeSpan? selectedTime;
        private string searchLabelText;
        private PickerConfigurationMenuViewModel timeSpanPickerConfigurationViewModel;
        private bool isHeaderVisible;
        private Color popupHeaderBackgroundColor;
        private Color popupHeaderFontColor;
        private string confirmationButtonText;
        private string cancellationButtonText;
        private Color selectedItemFontColor;
        private int selectedItemFontSize;
        private FontAttributes selectedItemFontAttribute;
        private Color selectedItemBackgroundColor;
        private int spinnerItemFontSize;
        private FontAttributes spinnerItemFontAttribute;
        private Color spinnerItemFontColor;
        private Color spinnerItemBackgroundColor;
        private Color confirmationButtonBackgroundColor;
        private Color cancellationButtonBackgroundColor;

        public ConfigurationViewModel()
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
            this.timeSpanPickerConfigurationViewModel = new PickerConfigurationMenuViewModel();
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
            set
            {
                if (this.searchLabelText != value)
                {
                    this.searchLabelText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Command FindMoviesCommand { get; }

        public override bool HasConfiguration
        {
            get
            {
                return true;
            }
        }

        public bool IsHeaderVisible
        {
            get
            {
                return this.isHeaderVisible;
            }
            set
            {
                if (this.isHeaderVisible != value)
                {
                    this.isHeaderVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color PopupHeaderBackgroundColor
        {
            get
            {
                return this.popupHeaderBackgroundColor;
            }
            set
            {
                if (this.popupHeaderBackgroundColor != value)
                {
                    this.popupHeaderBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color PopupHeaderFontColor
        {
            get
            {
                return this.popupHeaderFontColor;
            }
            set
            {
                if (this.popupHeaderFontColor != value)
                {
                    this.popupHeaderFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ConfirmationButtonText
        {
            get
            {
                return this.confirmationButtonText;
            }
            set
            {
                if (this.confirmationButtonText != value)
                {
                    this.confirmationButtonText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CancellationButtonText
        {
            get
            {
                return this.cancellationButtonText;
            }
            set
            {
                if (this.cancellationButtonText != value)
                {
                    this.cancellationButtonText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SelectedItemFontColor
        {
            get
            {
                return this.selectedItemFontColor;
            }
            set
            {
                if (this.selectedItemFontColor != value)
                {
                    this.selectedItemFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color ConfirmationButtonBackgroundColor
        {
            get
            {
                return this.confirmationButtonBackgroundColor;
            }
            set
            {
                if (this.confirmationButtonBackgroundColor != value)
                {
                    this.confirmationButtonBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color CancellationButtonBackgroundColor
        {
            get
            {
                return this.cancellationButtonBackgroundColor;
            }
            set
            {
                if (this.cancellationButtonBackgroundColor != value)
                {
                    this.cancellationButtonBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int SelectedItemFontSize
        {
            get
            {
                return this.selectedItemFontSize;
            }
            set
            {
                if (this.selectedItemFontSize != value)
                {
                    this.selectedItemFontSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FontAttributes SelectedItemFontAttribute
        {
            get
            {
                return this.selectedItemFontAttribute;
            }
            set
            {
                if (this.selectedItemFontAttribute != value)
                {
                    this.selectedItemFontAttribute = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SelectedItemBackgroundColor
        {
            get
            {
                return this.selectedItemBackgroundColor;
            }
            set
            {
                if (this.selectedItemBackgroundColor != value)
                {
                    this.selectedItemBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int SpinnerItemFontSize
        {
            get
            {
                return this.spinnerItemFontSize;
            }
            set
            {
                if (this.spinnerItemFontSize != value)
                {
                    this.spinnerItemFontSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FontAttributes SpinnerItemFontAttribute
        {
            get
            {
                return this.spinnerItemFontAttribute;
            }
            set
            {
                if (this.spinnerItemFontAttribute != value)
                {
                    this.spinnerItemFontAttribute = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SpinnerItemFontColor
        {
            get
            {
                return this.spinnerItemFontColor;
            }
            set
            {
                if (this.spinnerItemFontColor != value)
                {
                    this.spinnerItemFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SpinnerItemBackgroundColor
        {
            get
            {
                return this.spinnerItemBackgroundColor;
            }
            set
            {
                if (this.spinnerItemBackgroundColor != value)
                {
                    this.spinnerItemBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        internal override void OnAppearing()
        {
            base.OnAppearing();

            this.IsHeaderVisible = this.timeSpanPickerConfigurationViewModel.IsHeaderVisible;
            this.PopupHeaderBackgroundColor = this.timeSpanPickerConfigurationViewModel.PopupHeaderBackgroundColor.Color;
            this.PopupHeaderFontColor = this.timeSpanPickerConfigurationViewModel.PopupHeaderFontColor.Color;
            this.ConfirmationButtonText = this.timeSpanPickerConfigurationViewModel.ConfirmationButtonText;
            this.CancellationButtonText = this.timeSpanPickerConfigurationViewModel.CancellationButtonText;
            this.SelectedItemFontColor = this.timeSpanPickerConfigurationViewModel.SelectedItemFontColor.Color;
            this.SelectedItemFontSize = this.timeSpanPickerConfigurationViewModel.SelectedItemFontSize;
            this.SelectedItemFontAttribute = this.timeSpanPickerConfigurationViewModel.SelectedItemFontAttribute;
            this.SelectedItemBackgroundColor = this.timeSpanPickerConfigurationViewModel.SelectedItemBackgroundColor.Color;
            this.SpinnerItemFontSize = this.timeSpanPickerConfigurationViewModel.SpinnerItemFontSize;
            this.SpinnerItemFontAttribute = this.timeSpanPickerConfigurationViewModel.SpinnerItemFontAttribute;
            this.SpinnerItemFontColor = this.timeSpanPickerConfigurationViewModel.SpinnerItemFontColor.Color;
            this.SpinnerItemBackgroundColor = this.timeSpanPickerConfigurationViewModel.SpinnerItemBackgroundColor.Color;
            this.ConfirmationButtonBackgroundColor = this.timeSpanPickerConfigurationViewModel.ConfirmationButtonBackgroundColor.Color;
            this.CancellationButtonBackgroundColor = this.timeSpanPickerConfigurationViewModel.CancellationButtonBackgroundColor.Color;
        }

        protected override Task NavigateToConfigurationOverride()
        {
            return this.NavigationService.NavigateToConfigurationAsync(this.timeSpanPickerConfigurationViewModel);
        }

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
