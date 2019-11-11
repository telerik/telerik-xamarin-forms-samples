using QSF.Services;
using QSF.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.ImageEditor;
using Xamarin.Forms;

namespace QSF.Examples.ImageEditorControl.CustomToolbarExample
{
    public class EditImageViewModel : ViewModelBase
    {
        private string image;
        private bool canTakePhoto;
        private bool canPickPhoto;

        public EditImageViewModel()
        {
            this.BackCommand = new Command(this.NavigateBackAsync);
            this.SaveCommand = new Command<IImageContext>(this.SaveImageAsync);
            this.TakePhotoCommand = new Command(this.TakePhotoAsync, () => this.canTakePhoto);
            this.PickPhotoCommand = new Command(this.PickPhotoAsync, () => this.canPickPhoto);
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

        public bool CanTakePhoto
        {
            get
            {
                return this.canTakePhoto;
            }
            private set
            {
                if (this.canTakePhoto != value)
                {
                    this.canTakePhoto = value;
                    this.OnPropertyChanged();
                    this.TakePhotoCommand.ChangeCanExecute();
                }
            }
        }

        public bool CanPickPhoto
        {
            get
            {
                return this.canPickPhoto;
            }
            private set
            {
                if (this.canPickPhoto != value)
                {
                    this.canPickPhoto = value;
                    this.OnPropertyChanged();
                    this.PickPhotoCommand.ChangeCanExecute();
                }
            }
        }

        public Command BackCommand { get; }
        public Command SaveCommand { get; }
        public Command TakePhotoCommand { get; }
        public Command PickPhotoCommand { get; }

        internal override async Task InitializeAsync(object parameter)
        {
            await base.InitializeAsync(parameter);

            this.Image = parameter as string;

            if (await MediaHelper.InitializeAsync())
            {
                this.CanTakePhoto = MediaHelper.CanTakePhoto;
                this.CanPickPhoto = MediaHelper.CanPickPhoto;
            }
        }

        private async void NavigateBackAsync()
        {
            var navigationService = DependencyService.Get<INavigationService>();

            await navigationService.NavigateBackAsync();
        }

        private async void SaveImageAsync(IImageContext imageContext)
        {
            var imagePaths = await StorageHelper.EnumerateFilesAsync("ImageEditor", "ProfilePicture");
            var imagePath = imagePaths.FirstOrDefault();

            if (!string.IsNullOrEmpty(imagePath))
            {
                using (var stream = File.OpenWrite(imagePath))
                {
                    var maximumSize = new Size(1000, 1000);

                    await imageContext.SaveAsync(stream, ImageFormat.Png, 1.0, maximumSize);
                }
            }

            var navigationService = DependencyService.Get<INavigationService>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                await navigationService.NavigateBackAsync();
            });
        }

        private async void TakePhotoAsync()
        {
            var imagePath = await MediaHelper.TakePhotoAsync();

            if (!string.IsNullOrEmpty(imagePath))
            {
                this.Image = imagePath;
            }
        }

        private async void PickPhotoAsync()
        {
            var imagePath = await MediaHelper.PickPhotoAsync();

            if (!string.IsNullOrEmpty(imagePath))
            {
                this.Image = imagePath;
            }
        }
    }
}
