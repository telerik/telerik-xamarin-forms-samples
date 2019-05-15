using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using QSF.Services;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ImageEditorControl.FirstLookExample
{
    public class EditImageViewModel : ViewModelBase
    {
        private string image;

        public EditImageViewModel()
        {
            this.BackCommand = new Command(this.NavigateBackAsync);
            this.SaveCommand = new Command<IImageContext>(this.SaveImageAsync);
        }

        public string Image
        {
            get
            {
                return this.image;
            }
            private set
            {
                if (this.image != value)
                {
                    this.image = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand BackCommand { get; }
        public ICommand SaveCommand { get; }

        internal override Task InitializeAsync(object parameter)
        {
            this.Image = parameter as string;

            return base.InitializeAsync(parameter);
        }

        private async void NavigateBackAsync()
        {
            var navigationService = DependencyService.Get<INavigationService>();

            await navigationService.NavigateBackAsync();
        }

        private async void SaveImageAsync(IImageContext imageContext)
        {
            using (var stream = File.Create(image))
            {
                await imageContext.SaveAsync(stream);
            }

            var navigationService = DependencyService.Get<INavigationService>();

            await navigationService.NavigateBackAsync();
        }
    }
}
