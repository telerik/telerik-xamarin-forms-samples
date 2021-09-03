using System;
using System.Threading.Tasks;
using System.Windows.Input;
using tagit.Common;
using tagit.Helpers;
using Xamarin.Essentials;
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

        private ResourceDictionary theme;

        public ICommand LoadSampleImagesCommand { get; }
        public ICommand ShowDocumentationCommand { get; }

        public bool IsDarkThemeEnabled
        {
            get => _isDarkThemeEnabled;
            set
            {
                if (_isDarkThemeEnabled != value || theme == null)
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
                    if (this.theme != null)
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(this.theme);
                    }

                    if (isDarkEnabled)
                    {
                        this.theme = new DarkThemeResources();
                    }
                    else
                    {
                        this.theme = new LightThemeResources();
                    }

                    Application.Current.Resources.MergedDictionaries.Add(this.theme);

                    Application.Current.Resources["AppTextColor"] = isDarkEnabled
                            ? (Color)Application.Current.Resources["AppLightColor"]
                            : (Color)Application.Current.Resources["AppDarkColor"];

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
            Launcher.OpenAsync(new Uri("http://www.telerik.com/xamarin-ui"));
        }
    }
}