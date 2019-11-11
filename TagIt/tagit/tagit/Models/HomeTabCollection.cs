using System.Collections.ObjectModel;
using tagit.Common;
using tagit.Localization;

namespace tagit.Models
{
    /// <summary>
    ///     Used during image processing to display
    ///     cleanly formatted tab and list content
    /// </summary>
    public class HomeTabCollection : ObservableBase
    {
        public HomeTabCollection()
        {
            ProcessingTab = new HomeTabInformation { Label = "...", IsSelected = true };
            CompleteTab =
                new HomeTabInformation { Label = AppResources.ProcessingTaggedLabel.ToUpper(), IsSelected = false };
            ToBeReviewedTab =
                new HomeTabInformation { Label = AppResources.ProcessingReviewLabel.ToUpper(), IsSelected = false };
        }

        private HomeTabInformation _completeTab;

        private HomeTabInformation _processingTab;

        private HomeTabInformation _toBeReviewedTab;
        
        public HomeTabInformation ProcessingTab
        {
            get => _processingTab;
            set => SetProperty(ref _processingTab, value);
        }

        public HomeTabInformation CompleteTab
        {
            get => _completeTab;
            set => SetProperty(ref _completeTab, value);
        }

        public HomeTabInformation ToBeReviewedTab
        {
            get => _toBeReviewedTab;
            set => SetProperty(ref _toBeReviewedTab, value);
        }

        public void Reset()
        {
            ProcessingTab.Label = "...";
            CompleteTab.Label = AppResources.ProcessingTaggedLabel.ToUpper();
            ToBeReviewedTab.Label = AppResources.ProcessingReviewLabel.ToUpper();
        }
    }

    public class HomeTabInformation : ObservableBase
    {
        private ObservableCollection<ImageInformation> _images = new ObservableCollection<ImageInformation>();

        private bool _isSelected;
        private string _label;

        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        public ObservableCollection<ImageInformation> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}