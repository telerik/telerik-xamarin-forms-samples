using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using tagit.Common;
using tagit.Helpers;
using tagit.Views;
using Xamarin.Forms;

namespace tagit.Models
{
    /// <summary>
    ///     Used to store tagged image information
    /// </summary>
    public class ImageInformation : ObservableBase
    {
        public ImageInformation()
        {
            ToggleFavoriteCommand = new RelayCommand(ToggleFavorite);
            NavigateToDetailsCommand = new RelayCommand(async () => { await NavigateToDetailsAsync(); });
            SaveCommand = new RelayCommand(async () => { await SaveImageAsync(); });
            SelectSingleCommand = new RelayCommand<NotifyCollectionChangedEventArgs>(SelectSingleAsync);
        }

        private string _accentColor;

        private double _adultScore;

        private string _backgroundColor;

        private string _caption;

        private List<string> _categories = new List<string>();

        private DateTime _createdDate;

        private string _description;

        private List<FaceInformation> _faces = new List<FaceInformation>();

        private byte[] _file;

        private string _fileName;

        private string _foregroundColor;

        private bool _isAdult;

        private bool _isBlackAndWhite;

        private bool _isBusy;

        private bool _isClipArt;

        private bool _isFavorite;

        private bool _isLineDrawing;

        private bool _isProcessing;

        private bool _isRacy;

        private bool _isSelected;

        private bool _isTagged;

        private double _racyScore;

        private int _rating;

        private List<string> _tags = new List<string>();

        private string _url;

        private ImageSource imageSource;

        [IgnoreDataMember]
        public ICommand ToggleFavoriteCommand { get; }

        [IgnoreDataMember]
        public ICommand NavigateToDetailsCommand { get; }

        [IgnoreDataMember]
        public ICommand SaveCommand { get; }

        [IgnoreDataMember]
        public ICommand SelectSingleCommand { get; }

        public string Caption
        {
            get => _caption;
            set => SetProperty(ref _caption, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string AccentColor
        {
            get => _accentColor;
            set => SetProperty(ref _accentColor, value);
        }

        public string BackgroundColor
        {
            get => _backgroundColor;
            set => SetProperty(ref _backgroundColor, value);
        }

        public string ForegroundColor
        {
            get => _foregroundColor;
            set => SetProperty(ref _foregroundColor, value);
        }

        public List<FaceInformation> Faces
        {
            get => _faces;
            set => SetProperty(ref _faces, value);
        }

        public bool IsBlackAndWhite
        {
            get => _isBlackAndWhite;
            set => SetProperty(ref _isBlackAndWhite, value);
        }

        public bool IsLineDrawing
        {
            get => _isLineDrawing;
            set => SetProperty(ref _isLineDrawing, value);
        }

        public bool IsClipArt
        {
            get => _isClipArt;
            set => SetProperty(ref _isClipArt, value);
        }

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public byte[] File
        {
            get => _file;
            set
            {
                SetProperty(ref _file, value);
                this.imageSource = null;
            }
        }

        [IgnoreDataMember]
        public ImageSource ImageSource
        {
            get
            {
                if (this.imageSource == null)
                {
                    this.imageSource = Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(this.File));
                }

                return this.imageSource;
            }
        }

        public int Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            set => SetProperty(ref _createdDate, value);
        }

        public List<string> Tags
        {
            get => _tags;
            set => SetProperty(ref _tags, value);
        }

        public List<string> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public double RacyScore
        {
            get => _racyScore;
            set => SetProperty(ref _racyScore, value);
        }

        public bool IsRacy
        {
            get => _isRacy;
            set => SetProperty(ref _isRacy, value);
        }

        public double AdultScore
        {
            get => _adultScore;
            set => SetProperty(ref _adultScore, value);
        }

        public bool IsAdult
        {
            get => _isAdult;
            set => SetProperty(ref _isAdult, value);
        }

        [IgnoreDataMember]
        public bool IsProcessing
        {
            get => _isProcessing;
            set => SetProperty(ref _isProcessing, value);
        }

        [IgnoreDataMember]
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }

        public bool IsTagged
        {
            get => _isTagged;
            set => SetProperty(ref _isTagged, value);
        }

        [IgnoreDataMember]
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string DateLabel => Device.RuntimePlatform == Device.UWP
            ? CreatedDate.ToString("dddd, MMM d, yyyy")
            : CreatedDate.ToString("M/d/yyyy");

        private async void ToggleFavorite()
        {
            if (App.ViewModel.Processing.IsBusy || App.ViewModel.Processing.IsProcessing) return;

            IsFavorite = !IsFavorite;

            var existingImage = App.ViewModel.AllImages.FirstOrDefault(w => w.FileName.Equals(FileName));
            var existingFavorite = App.ViewModel.Favorites.Favorites
                .FirstOrDefault(w => w.FileName.Equals(FileName));

            existingImage.IsFavorite = IsFavorite;

            if (existingImage.IsFavorite)
            {
                if (!App.ViewModel.Favorites.Favorites.Contains(existingFavorite))
                    App.ViewModel.Favorites.Favorites.Add(this);
            }
            else
            {
                if (App.ViewModel.Favorites.Favorites.Contains(existingFavorite))
                    App.ViewModel.Favorites.Favorites.Remove(existingFavorite);
            }

            await StorageHelper.SaveTaggedImagesAsync(App.ViewModel.AllImages.ToList());
        }

        private async void SelectSingleAsync(NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
                await App.NavigationService.PushAsync(new DetailsPage(), args.NewItems[0] as ImageInformation);
        }

        private async Task NavigateToDetailsAsync()
        {
            if (App.ViewModel.Processing.IsProcessing) return;

            App.ViewModel.SelectedImage = this;

            await App.NavigationService.PushAsync(new DetailsPage());
        }

        public async Task SaveImageAsync()
        {
            if (App.ViewModel.Processing.IsProcessing) return;

            IsBusy = true;

            var taggedImages = await StorageHelper.GetTaggedImagesAsync();
            var currentImage = taggedImages.FirstOrDefault(p => p.FileName == this.FileName);

            var index = taggedImages.IndexOf(currentImage);
            taggedImages.Remove(currentImage);
            taggedImages.Insert(index, this);

            await StorageHelper.SaveTaggedImagesAsync(taggedImages);

            IsBusy = false;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ImageInformation;
            if (other == null)
            {
                return false;
            }

            return this.FileName == other.FileName;
        }

        public override int GetHashCode()
        {
            if (this.FileName == null)
            {
                return 0;
            }

            return this.FileName.GetHashCode();
        }
    }
}