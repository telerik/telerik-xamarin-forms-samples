using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using tagit.Common;
using tagit.Localization;
using tagit.Views;
using Xamarin.Forms;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the Upload view
    /// </summary>
    public class UploadViewModel : ObservableBase
    {
        public UploadViewModel()
        {
            SetUploadImageSource(false);

            StartUploadCommand = new RelayCommand(async () => { await StartUploadAsync(); });
        }

        public UploadViewModel(bool useDarkTheme = false)
        {
            SetUploadImageSource(useDarkTheme);
        }

        private ImageSource _uploadImageSource;
        
        public ICommand StartUploadCommand { get; }

        public ImageSource UploadImageSource
        {
            get => _uploadImageSource;
            set => SetProperty(ref _uploadImageSource, value);
        }

        private async Task StartUploadAsync()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                App.ViewModel.Picker.IsBusy = true;
                App.ViewModel.Picker.PickerImages.Clear();
                App.ViewModel.Picker.ContainsImages = true;
            }

            await App.NavigationService.PushAsync(new PickerPage(), "Select images");

            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
            {
                App.ViewModel.Picker?.InitializeAsync();
            }
            
            return;
        }

        private void SetUploadImageSource(bool useDarkTheme)
        {
            try
            {
                var assemblyName = typeof(UploadViewModel).GetTypeInfo().Assembly.GetName();
                var assembly = Assembly.Load(new AssemblyName(assemblyName.Name));

                UploadImageSource = useDarkTheme
                    ? ImageSource.FromResource(UiConstants.UploadImageDarkFileName, assembly)
                    : ImageSource.FromResource(UiConstants.UploadImageLightFileName, assembly);
            }
            catch (Exception ex)
            {
            }
        }
    }
}