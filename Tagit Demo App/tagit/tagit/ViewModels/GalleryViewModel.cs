using System.Collections.ObjectModel;
using System.Linq;
using tagit.Common;
using tagit.Helpers;
using tagit.Models;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the Gallery view
    /// </summary>
    public class GalleryViewModel : ObservableBase
    {
        private ObservableCollection<ImageInformation> _allImages = new ObservableCollection<ImageInformation>();

        private ObservableCollection<ImageInformation> _currentImages = new ObservableCollection<ImageInformation>();

        private bool _isBusy;

        public GalleryViewModel()
        {
            Initialize();
        }

        public ObservableCollection<ImageInformation> AllImages
        {
            get => _allImages;
            set => SetProperty(ref _allImages, value);
        }

        public ObservableCollection<ImageInformation> CurrentImages
        {
            get => _currentImages;
            set => SetProperty(ref _currentImages, value);
        }

        public ObservableCollection<ImageInformation> TaggedImages
        {
            get { return new ObservableCollection<ImageInformation>(_allImages.Where(w => w.IsTagged)); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public async void Initialize()
        {
            var taggedImages = await StorageHelper.GetTaggedImagesAsync();

            foreach (var image in taggedImages)
            {
                if (!AllImages.Contains(image))
                {
                    AllImages.Add(image);
                }
            }
        }
    }
}