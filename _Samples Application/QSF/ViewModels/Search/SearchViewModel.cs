using QSF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public class SearchViewModel : PageViewModelBase
    {
        private string searchText;
        private ObservableCollection<ControlNameSearchResultViewModel> searchResults;
        private Dictionary<Services.SearchResultType, Func<SearchResult, ControlNameSearchResultViewModel>> searchResultTypeToViewModelConverter;
        private ControlNameSearchResultViewModel selectedSearchResult;
        private bool hasResults;

        public SearchViewModel()
        {
            this.searchResultTypeToViewModelConverter = new Dictionary<Services.SearchResultType, Func<SearchResult, ControlNameSearchResultViewModel>>();
            this.searchResultTypeToViewModelConverter[Services.SearchResultType.Control] = this.ControlSearchResultToViewModel;
            this.searchResultTypeToViewModelConverter[Services.SearchResultType.ControlDescription] = this.ControlDescriptionSearchResultToViewModel;
            this.searchResultTypeToViewModelConverter[Services.SearchResultType.Example] = this.ExampleSearchResultToViewMovel;
            this.searchResultTypeToViewModelConverter[Services.SearchResultType.ExampleDescription] = this.ExampleDescriptionSearchResultToViewModel;

            this.AppBarLeftButtonCommand = new Command(async () => await this.NavigateBack());

            this.OnSearchTextChanged();
        }

        public string SearchText
        {
            get
            {
                return this.searchText;
            }
            set
            {
                if (this.searchText != value)
                {
                    this.searchText = value;
                    this.OnSearchTextChanged();
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ControlNameSearchResultViewModel> SearchResults
        {
            get
            {
                return this.searchResults;
            }
            private set
            {
                if (this.searchResults != value)
                {
                    this.searchResults = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ControlNameSearchResultViewModel SelectedSearchResult
        {
            get
            {
                return this.selectedSearchResult;
            }
            set
            {
                if (this.selectedSearchResult != value)
                {
                    this.selectedSearchResult = value;
                    this.OnSelectedSearchResultChanged();
                    this.OnPropertyChanged();
                }
            }
        }

        public bool HasResults
        {
            get
            {
                return this.hasResults;
            }
            private set
            {
                if (this.hasResults != value)
                {
                    this.hasResults = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void OnSearchTextChanged()
        {
            var controlsService = DependencyService.Get<IControlsService>();
            List<ControlNameSearchResultViewModel> results = new List<ControlNameSearchResultViewModel>();

            if (string.IsNullOrEmpty(this.SearchText))
            {
                var controls = controlsService.GetAllControls();
                foreach (var control in controls)
                {
                    var highlightedText = new HighlightedTextInfo(control.Name, 0, 0);
                    results.Add(new ControlNameSearchResultViewModel(SearchResultType.AllControls, control.Name, highlightedText));
                }
            }
            else
            {
                var searchService = DependencyService.Get<ISearchService>();
                var searchResults = searchService.Search(this.SearchText);

                foreach (var searchResult in searchResults)
                {
                    var viewModel = this.searchResultTypeToViewModelConverter[searchResult.ResultType](searchResult);
                    results.Add(viewModel);
                }
            }

            this.HasResults = results.Count > 0;

            this.SearchResults = new ObservableCollection<ControlNameSearchResultViewModel>(results);
        }

        private async void OnSelectedSearchResultChanged()
        {
            if (this.SelectedSearchResult == null)
            {
                return;
            }

            var resultType = this.SelectedSearchResult.ResultType;
            if (resultType == SearchResultType.Example || resultType == SearchResultType.ExampleDescription)
            {
                ExampleNameSearchResultViewModel selected = (ExampleNameSearchResultViewModel)this.SelectedSearchResult;

                AnalyticsHelper.TraceSelectSearchResult(this.SearchText, selected);

                await this.NavigationService.NavigateToExampleAsync(new ExampleInfo(selected.ControlName, selected.ExampleName));
            }
            else
            {
                AnalyticsHelper.TraceSelectSearchResult(this.searchText, this.SelectedSearchResult);
                await this.NavigationService.NavigateToAsync<ControlExamplesViewModel>(this.SelectedSearchResult.ControlName);
            }

            this.SelectedSearchResult = null;
        }

        private ControlNameSearchResultViewModel ControlSearchResultToViewModel(SearchResult result)
        {
            var highlightedText = new HighlightedTextInfo(result.ControlName, result.FirstCharIndex, result.LastCharIndex);
            return new ControlNameSearchResultViewModel(SearchResultType.Control, result.ControlName, highlightedText);
        }

        private ControlDescriptionSearchResultViewModel ControlDescriptionSearchResultToViewModel(SearchResult result)
        {
            var controlsService = DependencyService.Get<IControlsService>();
            var control = controlsService.GetControlByName(result.ControlName);

            var highlightedText = new HighlightedTextInfo(control.FullDescription, result.FirstCharIndex, result.LastCharIndex);
            return new ControlDescriptionSearchResultViewModel(SearchResultType.ControlDescription, control.Name, control.FullDescription, highlightedText);
        }

        private ExampleNameSearchResultViewModel ExampleSearchResultToViewMovel(SearchResult result)
        {
            var highlightedText = new HighlightedTextInfo(result.ExampleDisplayName, result.FirstCharIndex, result.LastCharIndex);
            return new ExampleNameSearchResultViewModel(SearchResultType.Example, result.ControlName, result.ExampleName, result.ExampleDisplayName, highlightedText);
        }

        private ExampleDescriptionSearchResultViewModel ExampleDescriptionSearchResultToViewModel(SearchResult result)
        {
            var controlsService = DependencyService.Get<IControlsService>();
            var example = controlsService.GetControlExample(result.ControlName, result.ExampleName);

            var highlightedText = new HighlightedTextInfo(example.Description, result.FirstCharIndex, result.LastCharIndex);
            return new ExampleDescriptionSearchResultViewModel(SearchResultType.ExampleDescription, result.ControlName, result.ExampleName, example.DisplayName, example.Description, highlightedText);
        }
    }
}
