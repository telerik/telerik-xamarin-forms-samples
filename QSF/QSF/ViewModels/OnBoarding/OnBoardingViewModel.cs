using QSF.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public class OnBoardingViewModel : ViewModelBase
    {
        private string icon;

        public OnBoardingViewModel()
        {
            var configurationService = DependencyService.Get<IConfigurationService>();
            var splashScreenConfig = configurationService.GetSplashScreenConfiguration();

            this.Icon = splashScreenConfig.Icon;
            this.Title = splashScreenConfig.Title;
            this.Slides = new ObservableCollection<SlideViewModel>(splashScreenConfig.Slides.Select(p => new SlideViewModel(p)));
        }

        public string Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                if (this.icon != value)
                {
                    this.icon = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Title { get; }

        public ObservableCollection<SlideViewModel> Slides { get; }
    }
}
