using System.Collections.ObjectModel;
using System.Windows.Input;
using QSF.Services;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ImageEditorControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private bool isBusy;

        public FirstLookViewModel()
        {
            this.Images = new ObservableCollection<string>();
            this.OpenCommand = new Command<string>(this.OpenImageAsync);
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

        public ObservableCollection<string> Images { get; }

        public ICommand OpenCommand { get; }

        internal override void OnAppearing()
        {
            this.LoadImagesAsync();
        }

        private async void LoadImagesAsync()
        {
            this.IsBusy = true;
            this.Images.Clear();

            var imagePaths = await StorageHelper.ExtractResourcesAsync("ImageEditor", "FirstLook");

            foreach (var imagePath in imagePaths)
            {
                this.Images.Add(imagePath);
            }

            this.IsBusy = false;
        }

        private async void OpenImageAsync(string imagePath)
        {
            var navigationService = DependencyService.Get<INavigationService>();

            await navigationService.NavigateToAsync<EditImageViewModel>(imagePath);
        }
    }
}
