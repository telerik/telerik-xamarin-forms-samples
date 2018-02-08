using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using tagit.Common;
using tagit.Helpers;
using tagit.Models;
using tagit.Views;
using Telerik.XamarinForms.Input.AutoComplete;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the SearchResults view
    /// </summary>
    public class SearchResultsViewModel : ObservableBase
    {
        public SearchResultsViewModel()
        {
            ToggleFavoriteCommand = new RelayCommand(ToggleFavorite);
            SuggestionSelectedCommand = new RelayCommand<SuggestionItemSelectedEventArgs>(SuggestionSelected);
            SelectSingleCommand = new RelayCommand<NotifyCollectionChangedEventArgs>(SelectSingleAsync);

            Initialize();
        }

        private ObservableCollection<ImageInformation> _searchableImages = new ObservableCollection<ImageInformation>();

        private string _searchTextValue;
        private string _searchQueryLabel;

        private ObservableCollection<ImageInformation> _searchResults = new ObservableCollection<ImageInformation>();

        private ObservableCollection<string> _searchTags = new ObservableCollection<string>();

        private ObservableCollection<string> _searchTokens = new ObservableCollection<string>();

        public ICommand ToggleFavoriteCommand { get; }
        public ICommand SuggestionSelectedCommand { get; }
        public ICommand SelectSingleCommand { get; }

        public ObservableCollection<string> SearchTokens
        {
            get => _searchTokens;
            set => SetProperty(ref _searchTokens, value);
        }

        public string SearchTextValue
        {
            get => _searchTextValue;
            set
            {
                SetProperty(ref _searchTextValue, value);

                if (string.IsNullOrEmpty(value)) ClearSearchResults();
            }
        }

        private void ClearSearchResults()
        {
            this.SearchResults.Clear();
        }

        public string SearchQueryLabel
        {
            get => _searchQueryLabel;
            set => SetProperty(ref _searchQueryLabel, value);
        }

        public ObservableCollection<ImageInformation> SearchResults
        {
            get => _searchResults;
            set => SetProperty(ref _searchResults, value);
        }

        public ObservableCollection<string> SearchTags
        {
            get => _searchTags;
            set => SetProperty(ref _searchTags, value);
        }

        public ObservableCollection<ImageInformation> SearchableImages
        {
            get => _searchableImages;
            set => SetProperty(ref _searchableImages, value);
        }

        private void Initialize()
        {
            if (App.ViewModel != null) PopulateSearchableImages(App.ViewModel.AllTags, App.ViewModel.AllImages);
        }

        private void SuggestionSelected(SuggestionItemSelectedEventArgs args)
        {
            SearchImages((string)args.DataItem);
        }

        private async void SelectSingleAsync(NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
                await App.NavigationService.PushAsync(new DetailsPage(), args.NewItems[0] as ImageInformation);
        }

        public void ToggleFavorite()
        {
            App.ViewModel.SelectedImage.IsFavorite = !App.ViewModel.SelectedImage.IsFavorite;

            var selectedImage = App.ViewModel.SelectedImage;

            var existingFavorite = App.ViewModel.AllImages
                .FirstOrDefault(w => w.FileName.Equals(selectedImage.FileName));

            if (existingFavorite != null) existingFavorite.IsFavorite = !existingFavorite.IsFavorite;

            StorageHelper.SaveFavoritesAsync(App.ViewModel.AllImages.Where(w => w.IsFavorite).ToList());
        }

        public void PopulateSearchableImages(IEnumerable<string> tags, IEnumerable<ImageInformation> images)
        {
            SearchTags = new ObservableCollection<string>(tags.Distinct());
            SearchableImages = new ObservableCollection<ImageInformation>(images);
        }

        public void SearchImages(string token)
        {
            SearchTokens.Clear();

            SearchTokens.Add(token);

            SearchImages();
        }

        public void SearchImages(List<string> tokens)
        {
            SearchTokens.Clear();

            foreach (var token in tokens)
                SearchTokens.Add(token);

            SearchImages();
        }

        private void SearchImages()
        {
            var searchResults = new List<ImageInformation>();

            SearchResults.Clear();

            foreach (var token in SearchTokens.Distinct())
                searchResults.AddRange(SearchableImages.Where(w =>
                    w.Tags.Contains(token, StringComparer.OrdinalIgnoreCase)));

            foreach (var result in searchResults.Distinct())
                SearchResults.Add(result);

            SearchQueryLabel = $"Images tagged as {string.Join(",", SearchTokens.Distinct())}";
        }
    }
}