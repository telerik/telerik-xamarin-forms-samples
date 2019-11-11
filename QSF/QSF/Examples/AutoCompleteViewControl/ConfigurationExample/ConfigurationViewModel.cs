using QSF.ViewModels;
using System.Collections.ObjectModel;
using Telerik.XamarinForms.Input.AutoComplete;
using Telerik.XamarinForms.Input.AutoCompleteView;

namespace QSF.Examples.AutoCompleteViewControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private bool isClearButtonVisible = true;
        private bool showSuggestionView = true;
        private SuggestionsDisplayMode displayMode;
        private CompletionMode completionMode;
        private SuggestMode suggestMode;
        private double? thresholdValue;
        private string watermarkText = "Search City Names";
        private string formatterPropertyName;
        private DisplayTextFormatter formatter;

        public ConfigurationViewModel()
        {
            this.Cities = new ObservableCollection<City>
            {
                new City("New York", 8632000, "USA"),
                new City("Tokyo", 9273000, "Japan"),
                new City("Sofia", 1236000, "Bulgaria"),
                new City("Budapest", 1756000, "Hungary"),
                new City("Madrid", 3166000, "Spain"),
                new City("Rome", 2873000, "Italy"),
                new City("Paris", 2200000, "France"),
                new City("Bucharest", 1836000, "Romania"),
                new City("Ottawa", 934000, "Canada"),
                new City("Manila", 1780000, "Philippines"),
                new City("Beijing", 21710000, "China"),
                new City("Jakarta", 9608000, "Indonesia"),
                new City("London", 8136000, "UK"),
                new City("Vienna", 1868000, "Austria"),
                new City("Athens", 664046, "Greece"),
                new City("Wellington", 207900, "New Zealand"),
                new City("Canberra", 356585, "Australia")
            };

            this.ThresholdValue = 0;
            this.SuggestMode = SuggestMode.Suggest;
            this.FormatterPropertyName = nameof(City.Name);
        }

        public ObservableCollection<City> Cities { get; set; }

        public bool IsClearButtonVisible
        {
            get
            {
                return this.isClearButtonVisible;
            }
            set
            {
                if (this.isClearButtonVisible != value)
                {
                    this.isClearButtonVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool ShowSuggestionView
        {
            get
            {
                return this.showSuggestionView;
            }
            set
            {
                if (this.showSuggestionView != value)
                {
                    this.showSuggestionView = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public SuggestionsDisplayMode DisplayMode
        {
            get
            {
                return this.displayMode;
            }
            set
            {
                if (this.displayMode != value)
                {
                    this.displayMode = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public CompletionMode CompletionMode
        {
            get
            {
                return this.completionMode;
            }
            set
            {
                if (this.completionMode != value)
                {
                    this.completionMode = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public SuggestMode SuggestMode
        {
            get
            {
                return this.suggestMode;
            }
            set
            {
                if (this.suggestMode != value)
                {
                    this.suggestMode = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double? ThresholdValue
        {
            get
            {
                return this.thresholdValue;
            }
            set
            {
                if (this.thresholdValue != value)
                {
                    this.thresholdValue = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string FormatterPropertyName
        {
            get
            {
                return this.formatterPropertyName;
            }
            set
            {
                if (this.formatterPropertyName != value)
                {
                    this.formatterPropertyName = value;
                    this.Formatter = new DisplayTextFormatter(this.formatterPropertyName);
                    this.OnPropertyChanged();
                }
            }
        }

        public DisplayTextFormatter Formatter
        {
            get
            {
                return this.formatter;
            }
            set
            {
                if (this.formatter != value)
                {
                    this.formatter = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string WatermarkText
        {
            get
            {
                return this.watermarkText;
            }
            set
            {
                if (this.watermarkText != value)
                {
                    this.watermarkText = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
