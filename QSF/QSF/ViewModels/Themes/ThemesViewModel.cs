using System.Collections.ObjectModel;
using System.Linq;
using QSF.Services;
using QSF.Services.Configuration;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public class ThemesViewModel : ViewModelBase
    {
        private ThemeViewModel selectedTheme;

        public ThemesViewModel()
        {
            this.AppBarLeftButtonCommand = new Command(async () => await this.NavigateBack());

            var themesService = DependencyService.Get<IThemesService>();
            var themes = themesService.GetThemes();
            var themesItemsSource = new ObservableCollection<ThemeViewModel>();

            foreach (var theme in themes)
            {
                themesItemsSource.Add(new ThemeViewModel(theme));
            }

            this.Themes = themesItemsSource;
            this.selectedTheme = themesItemsSource.Single(p => p.Name == themesService.CurrentTheme.Name);
            this.UpdateSelectedTheme();
        }

        public Command AppBarLeftButtonCommand { get; }

        public ObservableCollection<ThemeViewModel> Themes { get; }

        public ThemeViewModel SelectedTheme
        {
            get
            {
                return this.selectedTheme;
            }
            set
            {
                if (this.selectedTheme != value)
                {
                    this.selectedTheme = value;
                    this.OnSelectedThemeChanged();
                    this.OnPropertyChanged();
                }
            }
        }

        private async void OnSelectedThemeChanged()
        {
            if (this.SelectedTheme == null)
            {
                return;
            }

            this.UpdateSelectedTheme();

            AnalyticsHelper.TraceThemeChanged(this.SelectedTheme.Name);

            await this.NavigateBack();
        }

        private void UpdateSelectedTheme()
        {
            var themesService = DependencyService.Get<IThemesService>();
            Theme selectedTheme = themesService.GetTheme(this.SelectedTheme.Name);
            themesService.SetTheme(selectedTheme);

            foreach (var theme in this.Themes)
            {
                theme.IsSelected = theme.Name == this.SelectedTheme.Name;
            }
        }
    }
}
