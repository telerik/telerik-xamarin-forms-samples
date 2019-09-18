using QSF.Services;
using QSF.ViewModels;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.Examples.ImageEditorControl.CustomToolbarExample
{
    public class CustomToolbarViewModel : ExampleViewModel
    {
        private string image;
        private bool isBusy;

        public CustomToolbarViewModel()
        {
            this.OpenCommand = new Command(this.OpenImageAsync);
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

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            private set
            {
                if (this.isBusy != value)
                {
                    this.isBusy = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand OpenCommand { get; }

        internal override void OnAppearing()
        {
            this.LoadImageAsync();
        }

        private async void LoadImageAsync()
        {
            this.IsBusy = true;
            this.Image = null;

            var imagePaths = await StorageHelper.ExtractResourcesAsync("ImageEditor", "ProfilePicture");
            var imagePath = imagePaths.FirstOrDefault();

            this.Image = imagePath;
            this.IsBusy = false;
        }

        private async void OpenImageAsync()
        {
            var navigationService = DependencyService.Get<INavigationService>();

            await navigationService.NavigateToAsync<EditImageViewModel>(this.image);
        }
    }
}
