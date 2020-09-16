using System.Collections.ObjectModel;
using QSF.ViewModels;
using Telerik.XamarinForms.Input;

namespace QSF.Examples.ComboBoxControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private bool isEditable;
        private bool openOnFocus = true;
        private bool isDropDownClosedOnSelection = true;
        private bool isDropDownOpen;
        private bool isClearButtonVisible = true;
        private string placeholder;
        private string displayMemberPath;
        private string searchTextPath;
        private SearchMode searchMode;
        private ComboBoxSelectionMode selectionMode;

        public ConfigurationViewModel()
        {
            this.Cities = new ObservableCollection<City>()
            {
                new City("New York", "USA"),
                new City("Tokyo", "Japan"),
                new City("Sofia", "Bulgaria"),
                new City("Budapest", "Hungary"),
                new City("Madrid", "Spain"),
                new City("Rome", "Italy"),
                new City("Paris", "France"),
                new City("Bucharest", "Romania"),
                new City("Ottawa", "Canada"),
                new City("Manila", "Philippines"),
                new City("Beijing", "China"),
                new City("Jakarta", "Indonesia"),
                new City("London", "UK"),
                new City("Vienna", "Austria"),
                new City("Athens", "Greece"),
                new City("Wellington", "New Zealand"),
                new City("Canberra", "Australia")
            };

            this.Placeholder = "Find City";
            this.DisplayMemberPath = nameof(City.Name);
            this.SearchTextPath = nameof(City.Name);
        }

        public ObservableCollection<City> Cities { get; set; }

        public bool IsEditable
        {
            get
            {
                return this.isEditable;
            }
            set
            {
                if (this.isEditable != value)
                {
                    this.isEditable = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool OpenOnFocus
        {
            get
            {
                return this.openOnFocus;
            }
            set
            {
                if (this.openOnFocus != value)
                {
                    this.openOnFocus = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsDropDownClosedOnSelection
        {
            get
            {
                return this.isDropDownClosedOnSelection;
            }
            set
            {
                if (this.isDropDownClosedOnSelection != value)
                {
                    this.isDropDownClosedOnSelection = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsDropDownOpen
        {
            get
            {
                return this.isDropDownOpen;
            }
            set
            {
                if (this.isDropDownOpen != value)
                {
                    this.isDropDownOpen = value;
                    this.OnPropertyChanged();
                }
            }
        }

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

        public string Placeholder
        {
            get
            {
                return this.placeholder;
            }
            set
            {
                if (this.placeholder != value)
                {
                    this.placeholder = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string DisplayMemberPath
        {
            get
            {
                return this.displayMemberPath;
            }
            set
            {
                if (this.displayMemberPath != value)
                {
                    this.displayMemberPath = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string SearchTextPath
        {
            get
            {
                return this.searchTextPath;
            }
            set
            {
                if (this.searchTextPath != value)
                {
                    this.searchTextPath = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public SearchMode SearchMode
        {
            get
            {
                return this.searchMode;
            }
            set
            {
                if (this.searchMode != value)
                {
                    this.searchMode = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ComboBoxSelectionMode SelectionMode
        {
            get
            {
                return this.selectionMode;
            }
            set
            {
                if (this.selectionMode != value)
                {
                    this.selectionMode = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}