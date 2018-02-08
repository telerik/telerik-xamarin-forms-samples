using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using tagit.Common;
using tagit.Helpers;
using tagit.Localization;
using tagit.Models;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the Processing view
    /// </summary>
    public class ProcessingViewModel : ObservableBase
    {
        public ProcessingViewModel(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private HomeTabCollection _homeTabs = new HomeTabCollection();

        private bool _isBusy;

        private bool _isImageCollectionEmpty;

        private bool _isProcessing;

        private ObservableCollection<ImageInformation> _taggedImages = new ObservableCollection<ImageInformation>();
        
        private MainViewModel viewModel { get; }

        public RadTabView ProcessingTabView { get; set; }

        public HomeTabCollection HomeTabs
        {
            get => _homeTabs;
            set => SetProperty(ref _homeTabs, value);
        }

        public bool IsImageCollectionEmpty
        {
            get => _isImageCollectionEmpty;
            set => SetProperty(ref _isImageCollectionEmpty, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            set => SetProperty(ref _isProcessing, value);
        }

        public ObservableCollection<ImageInformation> TaggedImages
        {
            get => _taggedImages;
            set => SetProperty(ref _taggedImages, value);
        }

        public async void LoadTaggedImages()
        {
            IsBusy = true;

            var taggedImages = await StorageHelper.GetTaggedImagesAsync();

            var allCategories = new List<string>();
            var allTags = new List<string>();
            
            viewModel.AllImages.Clear();

            foreach (var image in taggedImages)
            {
                viewModel.AllImages.Add(image);

                allCategories.AddRange(image.Categories);
                allTags.AddRange(image.Tags);
            }

            TaggedImages = new ObservableCollection<ImageInformation>(taggedImages);

            viewModel.UpdateAnalysis(allCategories.Distinct(), allTags.Distinct());

            viewModel.SearchResults.PopulateSearchableImages(allTags.Distinct(), taggedImages);


            IsProcessing = false;
            IsBusy = false;
        }

        private async void SaveTaggedImages()
        {
            var taggedImages = await StorageHelper.GetTaggedImagesAsync();

            taggedImages.AddRange(HomeTabs.CompleteTab.Images);

            TaggedImages = new ObservableCollection<ImageInformation>(taggedImages);

            viewModel.Initialize();
        }


        public async void StartProcessingImages()
        {
            //Make sure the Processing view
            //has ready tabs
            HomeTabs.Reset();

            IsBusy = true;
            IsProcessing = true;

            var allCategories = new List<string>(viewModel.AllCategories);
            var allTags = new List<string>(viewModel.AllTags);

            viewModel.AllImages.ForEach(f => f.IsSelected = false);
            viewModel.Gallery.CurrentImages.ForEach(f => f.IsProcessing = true);

            TaggedImages = new ObservableCollection<ImageInformation>(await StorageHelper.GetTaggedImagesAsync());

            foreach (var image in HomeTabs.CompleteTab.Images)
            {
                //Perform image analysis via the Computer Vision API
                var analysis = await AnalysisHelper.AnalyzeImageAsync(image.File);

                image.ApplyAnalysis(analysis);

                HomeTabs.ProcessingTab.Images.Remove(image);
                HomeTabs.ProcessingTab.Label =
                    $"{AppResources.ProcessingTaggingLabel} {viewModel.Gallery.CurrentImages.Count - HomeTabs.ProcessingTab.Images.Count} of {viewModel.Gallery.CurrentImages.Count}...";

                //Mark racy or adult content as
                //in need of review
                if (image.IsAdult || image.IsRacy) HomeTabs.ToBeReviewedTab.Images.Add(image);
                 
                HomeTabs.CompleteTab.Label =
                    $"{AppResources.ProcessingTaggedLabel.ToUpper()} ({HomeTabs.CompleteTab.Images.Where(w => !string.IsNullOrEmpty(w.Caption)).Count()})";

                HomeTabs.ToBeReviewedTab.Label =
                       $"{AppResources.ProcessingReviewLabel.ToUpper()} ({HomeTabs.ToBeReviewedTab.Images.Count})";

                if (image.Categories != null) allCategories.AddRange(image.Categories);
                if (image.Tags != null) allTags.AddRange(image.Tags);

                TaggedImages.Add(image);
            }

            HomeTabs.ProcessingTab.Label = AppResources.ProcessingCompleteLabel;

            IsImageCollectionEmpty = true;

            //Save all tagged images locally
            await StorageHelper.SaveTaggedImagesAsync(TaggedImages.ToList());

            //Reset the picker to only display
            //untagged images
            viewModel.Picker.RefreshPickerImages();

         
            //Populate tag and category collections for searching, etc.
            viewModel.UpdateAnalysis(allCategories.Distinct(), allTags.Distinct());
            LoadTaggedImages();

            viewModel.SearchResults.SearchTags = new ObservableCollection<string>(allTags.Distinct());
            
            //Inform the Home page of tag availability
            //in order to display the Search menu
            MessagingCenter.Send<object>(this, "TagsAvailable");

            IsProcessing = false;
            IsBusy = false;
        }

        public void InitializeCurrentImages()
        {
            viewModel.Gallery.CurrentImages.Clear();
            HomeTabs.ProcessingTab.Images.Clear();
            HomeTabs.CompleteTab.Images.Clear();
            HomeTabs.ToBeReviewedTab.Images.Clear();

            foreach (var image in viewModel.Picker.PickerImages.Where(w => w.IsSelected))
            {
                viewModel.Gallery.CurrentImages.Add(image);
                HomeTabs.ProcessingTab.Images.Add(image);

                image.Caption = string.Empty;

                HomeTabs.CompleteTab.Images.Add(image);
            }

            IsImageCollectionEmpty = viewModel.Gallery.CurrentImages.Count == 0;
        }
    }
}