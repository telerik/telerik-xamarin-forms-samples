using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using QSF.Services;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ListViewControl.SelectionExample
{
    public class SelectionViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private ViewMode viewMode;
        private string selectedTab;
        private ListItem selectedItem;
        private Func<object, bool> itemFilter;

        public ViewMode ViewMode
        {
            get
            {
                return this.viewMode;
            }
            set
            {
                if (this.viewMode != value)
                {
                    this.viewMode = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(IsReadMode));
                    this.OnPropertyChanged(nameof(IsEditMode));
                }
            }
        }

        public bool IsReadMode
        {
            get
            {
                return this.viewMode == ViewMode.Read;
            }
        }

        public bool IsEditMode
        {
            get
            {
                return this.viewMode == ViewMode.Edit;
            }
        }

        public string SelectedTab
        {
            get
            {
                return this.selectedTab;
            }
            set
            {
                if (this.selectedTab != value)
                {
                    this.selectedTab = value;
                    this.OnPropertyChanged();
                    this.OnFilterChanged();
                }
            }
        }

        public ListItem SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Func<object, bool> ItemFilter
        {
            get
            {
                return this.itemFilter;
            }
            private set
            {
                if (this.itemFilter != value)
                {
                    this.itemFilter = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ListItem> Items { get; private set; }
        public ObservableCollection<string> Tabs { get; private set; }
        public Command EditCommand { get; private set; }
        public Command BackCommand { get; private set; }
        public Command<ListItem> DetailsCommand { get; private set; }
        public Command<ListItem> FavoriteItemCommand { get; private set; }
        public Command<ListItem> DeleteItemCommand { get; private set; }
        public Command<IEnumerable> FavoriteManyCommand { get; private set; }
        public Command<IEnumerable> DeleteManyCommand { get; private set; }

        public SelectionViewModel()
        {
            this.navigationService = DependencyService.Get<INavigationService>();

            this.Items = new ObservableCollection<ListItem>
            {
                new ListItem
                {
                    Date = "Today",
                    Title = "TESLA Announced brand new Model X",
                    Content = "Statistics show that the earlier Model S was bought mostly by men. Only 13% of the buyers were women. When asked women point out as most important features of a car the safety, reliability and extra space. Here comes the Model X.",
                    IsFavorite = true
                },
                new ListItem
                {
                    Date = "Today",
                    Title = "70 kWh for $70k",
                    Content = "Now, on to the awesome news of today. The 70 kWh version of the Model S in the single motor version at $70k costs $5k less than the dual motor version. Importantly, enough options are now standard that you will have bought a great car even if you pick the base version."
                },
                new ListItem
                {
                    Date = "Yesterday",
                    Title = "90 kWh Pack",
                    Content = "New buyers now have the option of upgrading the pack energy from 85 to 90 kWh for $3k, which provides about 6% increased range. For example, this takes our current longest range model, the 85D, to almost 300 miles of highway range at 65mph.",
                    IsFavorite = true
                },
                new ListItem
                {
                    Date = "Yesterday",
                    Title = "Luuudicrous Mode",
                    Content = "That was combined with upgrading the main pack contactor to use inconel (a high temperature space-grade superalloy) instead of steel, so that it remains springy under the heat of heavy current. The net result is that we can safely increase the max pack output from 1300 to 1500 Amps.",
                }
            };
            this.Tabs = new ObservableCollection<string>
            {
                "ALL",
                "FAVORITES"
            };

            this.SelectedTab = this.Tabs.FirstOrDefault();
            this.EditCommand = new Command(this.OnEdit);
            this.BackCommand = new Command(this.OnBack);
            this.DetailsCommand = new Command<ListItem>(this.OnDetails);
            this.FavoriteItemCommand = new Command<ListItem>(this.OnFavoriteItem);
            this.DeleteItemCommand = new Command<ListItem>(this.OnDeleteItem);
            this.FavoriteManyCommand = new Command<IEnumerable>(this.OnFavoriteMany);
            this.DeleteManyCommand = new Command<IEnumerable>(this.OnDeleteMany);
        }

        private void OnEdit()
        {
            this.ViewMode = ViewMode.Edit;
        }

        private void OnBack()
        {
            this.ViewMode = ViewMode.Read;
        }

        private void OnDetails(ListItem item)
        {
            this.SelectedItem = item;

            this.navigationService.NavigateToAsync<DetailsViewModel>(this);
        }

        private void OnFavoriteItem(ListItem item)
        {
            item.IsFavorite = !item.IsFavorite;
        }

        private void OnDeleteItem(ListItem item)
        {
            this.Items.Remove(item);
        }

        private void OnFavoriteMany(IEnumerable collection)
        {
            var items = collection.Cast<ListItem>().ToArray();
            var isFavorite = false;

            foreach (var item in items)
            {
                isFavorite |= item.IsFavorite;
            }

            foreach (var item in items)
            {
                item.IsFavorite = !isFavorite;
            }
        }

        private void OnDeleteMany(IEnumerable collection)
        {
            var items = collection.Cast<ListItem>().ToArray();

            foreach (var item in items)
            {
                this.Items.Remove(item);
            }
        }

        private void OnFilterChanged()
        {
            if (this.SelectedTab == "ALL")
            {
                this.ItemFilter = item => true;
            }
            else
            {
                this.ItemFilter = item => ((ListItem)item).IsFavorite;
            }
        }
    }
}
