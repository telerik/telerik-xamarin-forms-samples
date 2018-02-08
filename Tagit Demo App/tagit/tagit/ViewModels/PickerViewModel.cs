using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using tagit.Common;
using tagit.Helpers;
using tagit.Localization;
using tagit.Models;
using Xamarin.Forms;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the Picker view
    /// </summary>
    public class PickerViewModel : ObservableBase
    {
        public PickerViewModel()
        {
            ContainsImages = true;
            if (Device.RuntimePlatform != Device.Android)
            {
                InitializeAsync();
            }

            SelectSingleCommand = new RelayCommand<NotifyCollectionChangedEventArgs>(SelectSingle);
            StartProcessingCommand = new RelayCommand(async () => { await StartProcessingAsync(); });
        }
        
        private bool _isBusy;
        private bool _containsImages;

        private ObservableCollection<ImageInformation> _pickerImages = new ObservableCollection<ImageInformation>();

        private ObservableCollection<ImageInformation> _selectedImages = new ObservableCollection<ImageInformation>();
        
        public ICommand StartProcessingCommand { get; }
        public ICommand SelectSingleCommand { get; }

        public ObservableCollection<ImageInformation> SelectedImages
        {
            get => _selectedImages;
            set => SetProperty(ref _selectedImages, value);
        }

        public ObservableCollection<ImageInformation> PickerImages
        {
            get => _pickerImages;
            set => SetProperty(ref _pickerImages, value);
        }
        
        public bool ContainsImages
        {
            get => _containsImages;
            set => SetProperty(ref _containsImages, value);
        }
        
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private void SelectSingle(NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
                args.NewItems.OfType<ImageInformation>().ForEach(f => f.IsSelected = true);
            if (args.Action == NotifyCollectionChangedAction.Remove)
                args.OldItems.OfType<ImageInformation>().ForEach(f => f.IsSelected = false);
        }
 
        private async Task StartProcessingAsync()
        {
            await App.NavigationService.PopAsync(AppResources.TaggingTitle);
        }

        internal void InitializeAsync()
        {
            if (PickerImages.Count == 0) RefreshPickerImages();
        }

        internal async void RefreshPickerImages()
        {
            IsBusy = true;
            ContainsImages = true;

            PickerImages.Clear();

            var taggedImages = await StorageHelper.GetTaggedImagesAsync();

            var existingImageFileNames =
                taggedImages.Select(s => s.FileName.Split("/\\".ToCharArray()).LastOrDefault());
            
            var images = await ImageHelper.GetImagesAsync(existingImageFileNames);
            foreach (var image in images.Where(w => !taggedImages.Select(s => s.FileName).Contains(w.FileName)))
                PickerImages.Add(new ImageInformation
                {
                    Caption = image.FileName,
                    Url = image.Url,
                    FileName = image.FileName,
                    File = image.File,
                    CreatedDate = image.CreatedDate
                });


            var allImages = new List<ImageInformation>(taggedImages);
            allImages.AddRange(PickerImages);

            ContainsImages = PickerImages.Count > 0;
         
            IsBusy = false;

        }
    }
}