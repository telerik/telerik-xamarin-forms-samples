using System;
using System.Threading.Tasks;
using System.Windows.Input;
using tagit.Common;
using tagit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Themes;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the Settings view
    /// </summary>
    public class SettingsViewModel : ObservableBase
    {
        public SettingsViewModel()
        {
            IsFirstRun = StorageHelper.GetIsFirstRun();
            IsDarkThemeEnabled = StorageHelper.GetThemePreference();

            CanDownloadSampleImages = true;

            LoadSampleImagesCommand = new RelayCommand(async () => { await LoadSampleImagesAsync(); });
            ShowDocumentationCommand = new RelayCommand(() => { ShowDocumentationAsync(); });
        }

        private bool _isBusy;

        private bool _isDarkThemeEnabled;

        private bool _isFirstRun;

        private bool _canDownloadSampleImages;

        public ICommand LoadSampleImagesCommand { get; }
        public ICommand ShowDocumentationCommand { get; }

        public bool IsDarkThemeEnabled
        {
            get => _isDarkThemeEnabled;
            set
            {
                if (_isDarkThemeEnabled != value)
                    ToggleAppTheme(value);

                SetProperty(ref _isDarkThemeEnabled, value);
            }
        }

        public bool IsFirstRun
        {
            get => _isFirstRun;
            set => SetProperty(ref _isFirstRun, value);
        }

        public bool CanToggleTheme => Device.RuntimePlatform != Device.UWP;
        
        public bool CanDownloadSampleImages
        {
            get => _canDownloadSampleImages;
            set => SetProperty(ref _canDownloadSampleImages, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public void ToggleAppTheme(bool isDarkEnabled)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    Application.Current.Resources.MergedWith =
                        isDarkEnabled ? typeof(DarkThemeResources) : typeof(LightThemeResources);
                    Application.Current.Resources["AppTextColor"] =
                        (Color) Application.Current.Resources["AppLightColor"];

                    App.ViewModel.Upload.UploadImageSource = isDarkEnabled
                        ? ImageSource.FromResource(UiConstants.UploadImageDarkFileName)
                        : ImageSource.FromResource(UiConstants.UploadImageLightFileName);
                }
                catch
                {
                }
            });

            StorageHelper.SaveThemePreference(isDarkEnabled);
        }

        public void FinalizeAppTheme()
        {
            Application.Current.Resources["AppTextColor"] = IsDarkThemeEnabled
                ? (Color) Application.Current.Resources["AppLightColor"]
                : (Color) Application.Current.Resources["AppDarkColor"];
            Application.Current.Resources["IndicatorTextColor"] = IsDarkThemeEnabled ? Color.White : Color.Black;
        }

        public async Task LoadSampleImagesAsync()
        {
            IsBusy = true;
            CanDownloadSampleImages = false;

            foreach (var image in GettingStartedHelper.SampleImages)
                await ImageHelper.SaveImageToDiskAsync(image, $"{CoreConstants.SampleImagesUrl}{image}");

            App.ViewModel.Picker.RefreshPickerImages();

            CanDownloadSampleImages = true;
            IsBusy = false;
        }

        private void ShowDocumentationAsync()
        {
            //await App.NavigationService.PushAsync(new DocumentationPage());
            Device.OpenUri(new Uri("http://www.telerik.com/xamarin-ui"));
        }
    }
}