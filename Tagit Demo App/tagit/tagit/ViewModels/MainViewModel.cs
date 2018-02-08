using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using tagit.Common;
using tagit.Helpers;
using tagit.Models;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace tagit.ViewModels
{
    /// <summary>
    ///     The primary view model for all app services
    ///     commands, bindings, methods and state
    /// </summary>
    public class MainViewModel : ObservableBase
    {
        public MainViewModel()
        {   
            Processing = new ProcessingViewModel(this);

            Initialize();
        }

        private ObservableCollection<string> _allCategories = new ObservableCollection<string>();

        private ObservableCollection<ImageInformation> _allImages = new ObservableCollection<ImageInformation>();

        private ObservableCollection<string> _allTags = new ObservableCollection<string>();

        private bool _isBusy;

        private ImageInformation _selectedImage;

        public AnalysisViewModel Analysis = new AnalysisViewModel();
        public SearchResultsViewModel SearchResults = new SearchResultsViewModel();
        
        public FavoritesViewModel Favorites { get; } = new FavoritesViewModel();
        public GalleryViewModel Gallery { get; } = new GalleryViewModel();
        public GettingStartedViewModel GettingStarted { get; } = new GettingStartedViewModel();
        public HomeViewModel Home { get; } = new HomeViewModel();
        public PickerViewModel Picker { get; } = new PickerViewModel();
        public ProcessingViewModel Processing { get; }
        public SettingsViewModel Settings { get; } = new SettingsViewModel();
        public TimelineViewModel Timeline { get; } = new TimelineViewModel();
        public UploadViewModel Upload { get; } = new UploadViewModel();

        public ObservableCollection<ImageInformation> AllImages
        {
            get => _allImages;
            set => SetProperty(ref _allImages, value);
        }

        public ImageInformation SelectedImage
        {
            get => _selectedImage;
            set => SetProperty(ref _selectedImage, value);
        }

        public ObservableCollection<string> AllTags
        {
            get => _allTags;
            set => SetProperty(ref _allTags, value);
        }

        public ObservableCollection<string> AllCategories
        {
            get => _allCategories;
            set => SetProperty(ref _allCategories, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        internal async void Initialize()
        {
            var allCategories = new List<string>();
            var allTags = new List<string>();

            var taggedImages = await StorageHelper.GetTaggedImagesAsync();
            var favorites = (await StorageHelper.GetFavoritesAsync()).Select(s => s.FileName);

            AllImages.Clear();

            foreach (var image in taggedImages)
            {
                image.IsFavorite = favorites.Contains(image.FileName);

                AllImages.Add(image);

                allCategories.AddRange(image.Categories);
                allTags.AddRange(image.Tags);
            }

            UpdateAnalysis(allCategories, allTags);
        }

        internal void UpdateAnalysis(IEnumerable<string> allCategories, IEnumerable<string> allTags)
        {
            AllCategories = new ObservableCollection<string>(allCategories);
            AllTags = new ObservableCollection<string>(allTags);

            if (AllCategories.Count + AllTags.Count > 0) MessagingCenter.Send<object>(this, "TagsAvailable");
        }
    }
}