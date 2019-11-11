using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.XamarinForms.DataControls.TreeView;
using TodoApp.Models;
using TodoApp.Services;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class MainPageModel : MainPageModelBase
    {
        public MainPageModel(ITodoService todoService)
            : base(todoService)
        {
            this.currentLayoutType = LayoutType.Grid;
            this.showCompletedTasks = true;
            this.searchResults = new ObservableCollection<TodoItem>();
            this.openDrawerCommand = new Command(OnOpenDrawer);
            this.toggleListViewStyleCommand = new Command(OnToggleListViewStyle);
            this.editItemCommand = new Command<TodoItem>(OnEditItem);
            this.createNewCategoryCommand = new Command(CreateNewCategory);
            this.toggleEditCategoryCommand = new Command(OnToggleEditCategory);
            this.addCategoryCommand = new Command(CreateNewCategory);
            this.deleteCategoryCommand = new Command<CheckedItemsCollection>(OnDeleteCategory, CanDeleteCategory);
            this.editCategoryCommand = new Command<CheckedItemsCollection>(OnEditCategory, CanEditCategory);
            this.toggleShowCompletedTasksCommand = new Command(OnToggleShowCompletedTasks);
            this.searchItemsCommand = new Command<string>(OnSearchItems);
            this.cancelSearchCommand = new Command(OnCancelSearch);
            this.selectDateFilterCommand = new Command(OnSelectDateFilter);
            this.showAboutPageCommand = new Command(OnShowAboutPage);
        }

        private ObservableCollection<Category> categories;
        private ObservableCollection<TodoItem> searchResults;
        private Command openDrawerCommand, toggleListViewStyleCommand, createNewCategoryCommand, toggleEditCategoryCommand, addCategoryCommand,
            toggleShowCompletedTasksCommand, cancelSearchCommand, selectDateFilterCommand, showAboutPageCommand;
        private Command<TodoItem> editItemCommand;
        private Command<CheckedItemsCollection> deleteCategoryCommand, editCategoryCommand;
        private Command<string> searchItemsCommand;
        private bool drawerOpen, categoryEdit, showCompletedTasks, showSearchResults, dateFilteringApplied;
        private TodoItem selectedTodoItem;
        private DateFilter currentDateFilter;
        private Category selectedCategory;
        private LayoutType currentLayoutType;

        public static string Uncheck_Categories_Action = "uncheck";
        public static string Collapse_Categories_Action = "collapse";

        public ICommand ToggleListViewLayoutCommand => this.toggleListViewStyleCommand;
        public ICommand EditItemCommand => this.editItemCommand;
        public ICommand CreateNewCategoryCommand => this.createNewCategoryCommand;
        public ICommand ToggleEditCategoryCommand => this.toggleEditCategoryCommand;
        public ICommand DeleteCategoryCommand => this.deleteCategoryCommand;
        public ICommand EditCategoryCommand => this.editCategoryCommand;
        public ICommand AddCategoryCommand => this.addCategoryCommand;
        public ICommand ToggleShowCompletedTasksCommand => this.toggleShowCompletedTasksCommand;
        public ICommand SearchItemsCommand => this.searchItemsCommand;
        public ICommand CancelSearchCommand => this.cancelSearchCommand;
        public ICommand SelectDateFilterCommand => this.selectDateFilterCommand;
        public ICommand ShowAboutPageCommand => this.showAboutPageCommand;

        public LayoutType CurrentListViewLayout
        {
            get => this.currentLayoutType;
            private set => SetProperty(ref this.currentLayoutType, value);
        }

        public ICollection<Category> Categories
        {
            get => this.categories;
            private set => SetProperty(ref this.categories, value as ObservableCollection<Category>);
        }

        public ObservableCollection<TodoItem> SearchResults => this.searchResults;

        public Category SelectedCategory
        {
            get => this.selectedCategory;
            set
            {
                if (SetProperty(ref this.selectedCategory, value) && value != null)
                {
                    OnSelectedCategoryChanged(value);
                }
            }
        }

        public TodoItem SelectedTodoItem
        {
            get => this.selectedTodoItem;
            set
            {
                if (SetProperty(ref this.selectedTodoItem, value) && value != null)
                {
                    OnEditItem(value);
                }
            }
        }

        public bool IsDrawerOpen
        {
            get => this.drawerOpen;
            set => SetProperty(ref this.drawerOpen, value);
        }

        public bool CategoryEdit
        {
            get => this.categoryEdit;
            private set => SetProperty(ref this.categoryEdit, value);
        }

        public bool ShowCompletedTasks
        {
            get => this.showCompletedTasks;
            private set => SetProperty(ref this.showCompletedTasks, value);
        }

        public bool ShowSearchResults
        {
            get => this.showSearchResults;
            private set => SetProperty(ref this.showSearchResults, value);
        }

        public bool DateFilteringApplied
        {
            get => this.dateFilteringApplied;
            private set => SetProperty(ref this.dateFilteringApplied, value);
        }

        public DateFilter CurrentDateFilter
        {
            get => this.currentDateFilter;
            private set => SetProperty(ref this.currentDateFilter, value);
        }

        public ICommand OpenDrawerCommand => this.openDrawerCommand;

        public override void Init(object initData)
        {
            base.Init(initData);

            if (this.categories == null)
            {
                RebuildCategories();
            }

            MessagingCenter.Subscribe<ITodoService, Category>(this, Services.TodoService.ActionAdd, OnCategoryAdded);
            MessagingCenter.Subscribe<ITodoService, Category>(this, Services.TodoService.ActionUpdate, OnCategoryUpdated);
            MessagingCenter.Subscribe<CheckedItemsCollection, NotifyCollectionChangedAction>(this, "", OnTreeViewItemsCheckedChanged);
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData is DateFilter filter)
            {
                IsDrawerOpen = false;
                DateFilteringApplied = filter != null;
                CurrentDateFilter = filter;
                RebuildCategories();
            }
        }

        public bool BackButtonPressed()
        {
            if (this.IsDrawerOpen)
            {
                this.IsDrawerOpen = false;
                return true;
            }
            if (this.CategoryEdit)
            {
                this.CategoryEdit = false;
                RebuildCategories();
                return true;
            }
            if (this.ShowSearchResults)
            {
                this.OnCancelSearch();
                return true;
            }

            return false;
        }

        protected override void OnTodoItemAdded(TodoItem newTodoItem)
        {
            var category = this.Categories.Where(c => c.ID == newTodoItem.Category.ID).SingleOrDefault();
            if (category != null)
            {
                category.Items.Add(newTodoItem);
            }
        }

        protected override void OnTodoItemUpdated(TodoItem updatedTodoItem)
        {
            RebuildCategories();
        }

        protected override void OnTodoItemDeleted(TodoItem oldTodoItem)
        {
            var category = this.Categories.Where(c => c.ID == oldTodoItem.Category.ID).SingleOrDefault();
            if (category != null)
            {
                category.Items.Remove(oldTodoItem);
            }
        }

        private void OnCategoryAdded(ITodoService sender, Category item)
        {
            RebuildCategories();
        }

        private void OnCategoryUpdated(ITodoService sender, Category item)
        {
            var category = this.categories.Where(c => c.ID == item.ID).SingleOrDefault();
            if (category == null)
                return;

            var index = this.categories.IndexOf(category);
            item.Index = index;
            this.categories[index] = item;
        }

        private async void OnSelectedCategoryChanged(Category category)
        {
            if (category.IsNew)
                CreateNewCategory();
            else
                await this.CoreMethods.PushPageModel<CategoryDetailsPageModel>(category);
            this.SelectedCategory = null;
        }

        private void OnOpenDrawer()
        {
            IsDrawerOpen = !IsDrawerOpen;
        }

        private void OnToggleListViewStyle()
        {
            if (this.ShowSearchResults)
            {
                OnCancelSearch();
            }
            CurrentListViewLayout = this.currentLayoutType == LayoutType.Linear ? LayoutType.Grid : LayoutType.Linear;
            this.CategoryEdit = false;
            this.IsDrawerOpen = false;
        }

        private void OnToggleShowCompletedTasks()
        {
            this.ShowCompletedTasks = !this.showCompletedTasks;
            this.RebuildCategories();
        }

        private void OnEditItem(TodoItem item)
        {
            this.CoreMethods.PushPageModel<TaskEditorPageModel>(item);
        }

        private void OnSearchItems(string term)
        {
            ShowSearchResults = true;
            var items = this.TodoService.SearchForItems(term);
            this.searchResults.Clear();
            foreach (var item in items)
            {
                this.searchResults.Add(item);
            }
        }

        private void OnCancelSearch()
        {
            this.searchResults.Clear();
            ShowSearchResults = false;
        }

        private void OnSelectDateFilter()
        {
            this.IsDrawerOpen = false;
            if (!this.DateFilteringApplied)
            {
                this.CoreMethods.PushPageModel<DateFilterPageModel>();
            }
            else
            {
                this.CurrentDateFilter = null;
                this.DateFilteringApplied = false;
                this.RebuildCategories();
            }
        }

        private void OnShowAboutPage()
        {
            this.CoreMethods.PushPageModel<AboutPageModel>();
            this.OnOpenDrawer();
        }

        private void CreateNewCategory()
        {
            this.CoreMethods.PushPageModel<EditCategoryPageModel>(new Category() { Color = Color.Accent });
        }

        private void OnToggleEditCategory()
        {
            if (this.ShowSearchResults)
            {
                OnCancelSearch();
            }
            this.CurrentListViewLayout = LayoutType.Linear;
            this.CategoryEdit = !this.CategoryEdit;
            this.IsDrawerOpen = false;
            if (this.CategoryEdit)
            {
                MessagingCenter.Send(this, Collapse_Categories_Action, this.CategoryEdit);
            }
        }

        private void OnEditCategory(CheckedItemsCollection items)
        {
            var category = (items != null && items.Count > 0) ? items.OfType<Category>().FirstOrDefault() : null;
            if (category != null)
                this.CoreMethods.PushPageModel<EditCategoryPageModel>(category);
        }

        private bool CanEditCategory(CheckedItemsCollection items)
        {
            return items?.Count == 1 && this.categories.Contains(items.First() as Category, Category.IDEqualityComparer.Instance);
        }

        private async void OnDeleteCategory(CheckedItemsCollection items)
        {
            Category[] selectedCategories = (items != null && items.Count > 0) ?
                items.OfType<Category>().ToArray() : Enumerable.Empty<Category>().ToArray();
            string message = items.Count == 1 ?
                $"Delete category {selectedCategories[0].Name}?" :
                $"Delete selected {selectedCategories.Length} categories?";
            if (await CoreMethods.DisplayAlert("Delete category", message, "Yes", "No"))
            {
                try
                {
                    foreach (Category category in selectedCategories)
                    {
                        await this.TodoService.DeleteAsync(category);
                    }
                }
                catch (Exception)
                {
                    await CoreMethods.DisplayAlert("Error", "Error deleting category", "OK");
                }
                MessagingCenter.Send<MainPageModel, Category[]>(this, Uncheck_Categories_Action, selectedCategories);
                this.RebuildCategories();
            }
        }

        private bool CanDeleteCategory(CheckedItemsCollection items)
        {
            return items?.Count >= 1 && items.OfType<Category>().Any(c => this.categories.Contains(c, Category.IDEqualityComparer.Instance));
        }

        private void OnTreeViewItemsCheckedChanged(CheckedItemsCollection arg1, NotifyCollectionChangedAction arg2)
        {
            this.editCategoryCommand.ChangeCanExecute();
            this.deleteCategoryCommand.ChangeCanExecute();
        }

        private void RebuildCategories()
        {
            Task.Run(() => this.TodoService.GetCategoriesWithItems(this.showCompletedTasks, this.currentDateFilter)).ContinueWith(antc =>
            {
                var categories = new ObservableCollection<Category>(antc.Result);
                categories.Add(new Category() { Name = "Add Category" });

                for (int i = 0; i < categories.Count; i++)
                {
                    categories[i].Index = i;
                }

                this.Categories = categories;
                this.deleteCategoryCommand.ChangeCanExecute();
                this.editCategoryCommand.ChangeCanExecute();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }

    public enum LayoutType
    {
        Linear,
        Grid
    }
}
