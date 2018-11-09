using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using tagit.Common;
using tagit.Helpers;
using tagit.Models;
using tagit.Views;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the Favorites view
    /// </summary>
    public class FavoritesViewModel : ObservableBase
    {
        private ObservableCollection<ImageInformation> _favorites = new ObservableCollection<ImageInformation>();

        private bool _isBusy;

        public FavoritesViewModel()
        {
            Initialize();

            ToggleFavoriteCommand = new RelayCommand(ToggleFavorite);
            SelectSingleCommand = new RelayCommand<NotifyCollectionChangedEventArgs>(SelectSingleAsync);
        }

        public ICommand ToggleFavoriteCommand { get; }
        public ICommand SelectSingleCommand { get; }

        public ObservableCollection<ImageInformation> Favorites
        {
            get => _favorites;
            set => SetProperty(ref _favorites, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private async void SelectSingleAsync(NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
                await App.NavigationService.PushAsync(new DetailsPage(), args.NewItems[0] as ImageInformation);
        }

        public async void ToggleFavorite()
        {
            App.ViewModel.SelectedImage.IsFavorite = !App.ViewModel.SelectedImage.IsFavorite;

            var selectedImage = App.ViewModel.SelectedImage;

            var existingFavorite = App.ViewModel.AllImages
                .FirstOrDefault(w => w.FileName.Equals(selectedImage.FileName));

            existingFavorite.IsFavorite = !existingFavorite.IsFavorite;

            if (selectedImage.IsFavorite)
            {
                if (!Favorites.Contains(existingFavorite)) Favorites.Add(selectedImage);
            }
            else
            {
                if (Favorites.Contains(existingFavorite)) Favorites.Remove(existingFavorite);
            }

            await StorageHelper.SaveTaggedImagesAsync(App.ViewModel.AllImages.ToList());
        }

        public async void ToggleFavorite(ImageInformation image, bool isFavorite)
        {
            image.IsFavorite = !image.IsFavorite;

            var existingFavorite =
                App.ViewModel.AllImages.FirstOrDefault(w => w.FileName.Equals(image.FileName));

            if (existingFavorite != null) existingFavorite.IsFavorite = !existingFavorite.IsFavorite;

            if (image.IsFavorite)
            {
                if (existingFavorite == null) Favorites.Add(image);
            }
            else
            {
                if (existingFavorite != null) Favorites.Remove(existingFavorite);
            }

            await StorageHelper.SaveTaggedImagesAsync(App.ViewModel.AllImages.ToList());
        }

        public async void Initialize()
        {
            IsBusy = true;

            Favorites.Clear();

            var images = await StorageHelper.GetTaggedImagesAsync();
            var favorites = images.Where(p => p.IsFavorite);

            foreach (var favorite in favorites)
            {
                this.Favorites.Add(favorite);
            }

            IsBusy = false;
        }
    }
}