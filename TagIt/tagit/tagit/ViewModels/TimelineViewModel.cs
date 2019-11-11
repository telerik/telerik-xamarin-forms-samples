using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using tagit.Common;
using tagit.Helpers;
using tagit.Models;
using tagit.Views;
using Xamarin.Forms;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the Timeline view
    /// </summary>
    public class TimelineViewModel : ObservableBase
    {
        public TimelineViewModel()
        {
            Initialize(Device.RuntimePlatform != Device.UWP);
            SelectSingleCommand = new RelayCommand<NotifyCollectionChangedEventArgs>(SelectSingleAsync);
            this.Timeline = new ObservableCollection<ImageCreationEvent>();
        }

        private DateTime? _selectedDate;

        private ObservableCollection<ImageInformation> _selectedImages = new ObservableCollection<ImageInformation>();

        private ObservableCollection<ImageInformation> _taggedImages = new ObservableCollection<ImageInformation>();

        private ObservableCollection<ImageCreationEvent> _timeline = new ObservableCollection<ImageCreationEvent>();
        
        public ICommand SelectSingleCommand { get; }

        public ObservableCollection<ImageInformation> SelectedImages
        {
            get => _selectedImages;
            set => SetProperty(ref _selectedImages, value);
        }

        public ObservableCollection<ImageInformation> TaggedImages
        {
            get => _taggedImages;
            set => SetProperty(ref _taggedImages, value);
        }

        public ObservableCollection<ImageCreationEvent> Timeline
        {
            get => _timeline;
            set => SetProperty(ref _timeline, value);
        }

        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                SetProperty(ref _selectedDate, value);
                UpdateTimelineEvents(value);
            }
        }

        private async void SelectSingleAsync(NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
                await App.NavigationService.PushAsync(new DetailsPage(), args.NewItems[0] as ImageInformation);
        }

        internal async void Initialize(bool isPopulated = true)
        {
            if (isPopulated)
            {
                SelectedImages = new ObservableCollection<ImageInformation>();

                Timeline.Clear();
                TaggedImages.Clear();

                var taggedImages = await StorageHelper.GetTaggedImagesAsync();
                var favorites = taggedImages.Where(p => p.IsTagged).Select(s => s.FileName);

                foreach (var image in taggedImages)
                {
                    image.IsFavorite = favorites.Contains(image.FileName);

                    TaggedImages.Add(image);
                }

                foreach (var image in taggedImages)
                {
                    Timeline.Add(new ImageCreationEvent(
                        image.CreatedDate,
                        image.CreatedDate.AddMinutes(1), (Device.RuntimePlatform == Device.UWP) ? $"         {image.FileName}" : image.Caption,
                        Color.FromHex(image.AccentColor)));
                }
 
                SelectedDate = taggedImages.Count == 0
               ? DateTime.Today
               : taggedImages.OrderByDescending(o => o.CreatedDate).First().CreatedDate.Date;
            }
            
        }

        private void UpdateTimelineEvents(DateTime? value)
        {
            SelectedImages.Clear();

            foreach (var image in TaggedImages)
                if (image.CreatedDate.CompareTo(value.Value) >= 0 &&
                    image.CreatedDate.CompareTo(value.Value.AddDays(1)) < 0)
                    SelectedImages.Add(image);
        }
    }
}