using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
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
            var fileSystemService = DependencyService.Get<IFileSystemService>();
            var localPath = fileSystemService.GetLocalFolder();
            var cachePath = Path.Combine(localPath, "ImageEditor", "SampleImages");

            this.IsBusy = true;

            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);

                var currentAssembly = Assembly.GetExecutingAssembly();
                var resourceNames = currentAssembly.GetManifestResourceNames();

                foreach (var resourceName in resourceNames)
                {
                    var startIndex = resourceName.IndexOf("SampleImage");

                    if (startIndex >= 0)
                    {
                        var imageName = resourceName.Substring(startIndex);
                        var imagePath = Path.Combine(cachePath, imageName);

                        using (var sourceStream = currentAssembly.GetManifestResourceStream(resourceName))
                        using (var targetStream = File.Create(imagePath))
                        {
                            await sourceStream.CopyToAsync(targetStream);
                        }
                    }
                }
            }

            this.Images.Clear();

            var imagePaths = Directory.EnumerateFiles(cachePath);

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
