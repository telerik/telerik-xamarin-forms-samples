using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using tagit.Common;
using tagit.Localization;
using tagit.Models;
using tagit.Views;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the Home view
    /// </summary>
    public class HomeViewModel : ObservableBase
    {
        public HomeViewModel()
        {
            InitializeNavigation();

            NavigateFromMenuCommand = new RelayCommand<NotifyCollectionChangedEventArgs>(NavigateFromMenuAsync);
            MenuSelectedCommand = new RelayCommand(MenuSelected);
            SearchSelectedCommand = new RelayCommand(SearchSelected);
        }

        public RadSideDrawer Drawer { get; set; }

        public ICommand NavigateFromMenuCommand { get; }
        public ICommand MenuSelectedCommand { get; }
        public ICommand SearchSelectedCommand { get; }

        private bool _isProcessing;

        private ObservableCollection<NavigationItem> _navigationItems;

        private string _pageTitle;

        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        public ObservableCollection<NavigationItem> NavigationItems
        {
            get => _navigationItems;
            set => SetProperty(ref _navigationItems, value);
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            set => SetProperty(ref _isProcessing, value);
        }

        private void MenuSelected()
        {
            App.NavigationService.ToggleDrawer();
        }

        private void SearchSelected()
        {
            App.NavigationService.NavigateToPage(this, PageType.Search);
        }

        internal void ToggleSearchMenu(IList<Xamarin.Forms.ToolbarItem> toolbarItems, bool isEnabled)
        {
            if (toolbarItems.Count == 1)
            {
                if (isEnabled)
                {
                    var toolbarItem = new Xamarin.Forms.ToolbarItem
                    {
                        IconImageSource = ImageSource.FromFile("ic_action_search.png"),
                        Text = AppResources.SearchLabel,
                        Command = App.ViewModel.Home.SearchSelectedCommand
                    };

                    toolbarItems.Insert(0, toolbarItem);
                }
            }
            else if (toolbarItems.Count == 2)
            {
                if (!isEnabled) toolbarItems.RemoveAt(0);
            }
        }

        private async void NavigateFromMenuAsync(NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                var selectedPage = args.NewItems[0] as NavigationItem;

                if (selectedPage != null && selectedPage.PageType == PageType.Settings)
                    await App.NavigationService.PushAsync(new SettingsPage(), "Settings");
                else if (selectedPage != null)
                    App.NavigationService.NavigateToPage(this, selectedPage.PageType);


            }
        }

        internal void InitializeNavigation()
        {
            PageTitle = (App.ViewModel != null && App.ViewModel.Settings.IsFirstRun) ? "" : AppResources.WelcomeTitle;

            NavigationItems = new ObservableCollection<NavigationItem>
            {
                new NavigationItem
                {
                    PageType = PageType.Home,
                    Icon = "ic_action_home.png",
                    Label = AppResources.HomeMenuLabel
                },
                new NavigationItem
                {
                    PageType = PageType.Gallery,
                    Icon = "ic_action_photo_library.png",
                    Label = AppResources.GalleryMenuLabel
                },
                new NavigationItem
                {
                    PageType = PageType.Timeline,
                    Icon = "ic_action_access_time.png",
                    Label = AppResources.TimelineMenuLabel
                },
                new NavigationItem
                {
                    PageType = PageType.Analysis,
                    Icon = "ic_action_compare.png",
                    Label = AppResources.AnalysisMenuLabel
                },
                new NavigationItem
                {
                    PageType = PageType.Favorites,
                    Icon = "ic_action_favorite_border.png",
                    Label = AppResources.FavoritesMenuLabel
                },
                new NavigationItem
                {
                    PageType = PageType.Settings,
                    Icon = "ic_action_settings.png",
                    Label = AppResources.SettingsMenuLabel
                }
            };
        }

        internal async void ShowFirstRun()
        {
            //Show the Getting Started view if it's 
            //the first time a user has opened the app           
            if (Device.RuntimePlatform == Device.iOS)
            {
                await App.NavigationService.PushAsync(new GettingStartedPage());
            }
            else
            {
                await App.NavigationService.PushModalAsync(new GettingStartedPage());
            }
        }

        internal void EnableMainMenu(IList<Xamarin.Forms.ToolbarItem> toolbarItems)
        {
            if (App.ViewModel.Settings.IsFirstRun) return;

            if (toolbarItems.Count == 0)
            {
                var toolbarItem = new Xamarin.Forms.ToolbarItem
                {
                    IconImageSource = ImageSource.FromFile("ic_action_menu.png"),
                    Text = "Menu",
                    Command = App.ViewModel.Home.MenuSelectedCommand
                };

                toolbarItems.Insert(0, toolbarItem);
            }
        }
    }
}