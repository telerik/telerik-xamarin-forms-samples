using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using tagit.Common;
using tagit.Helpers;
using tagit.Views;
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
            ShowDocumentationCommand = new RelayCommand(async () => { await ShowDocumentationAsync(); });
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

                    var assemblyName = typeof(UploadViewModel).GetTypeInfo().Assembly.GetName();
                    var assembly = Assembly.Load(new AssemblyName(assemblyName.Name));

                    App.ViewModel.Upload.UploadImageSource = isDarkEnabled
                        ? ImageSource.FromResource(UiConstants.UploadImageDarkFileName, assembly)
                        : ImageSource.FromResource(UiConstants.UploadImageLightFileName, assembly);
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

        private async Task ShowDocumentationAsync()
        {
            await App.NavigationService.PushAsync(new DocumentationPage());
        }
    }
}