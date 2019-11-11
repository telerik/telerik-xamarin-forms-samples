using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Services;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class CategoryPageModel : FreshBasePageModelEx
    {
        public CategoryPageModel(ITodoService todoService)
        {
            _service = todoService;
            _acceptCommand = new Command(OnSelectCategory);
            _addNewCategoryCommand = new Command(AddNewCategory, CanAddNewCategory);
        }

        private ITodoService _service;
        private ObservableCollection<Category> _categories;
        private Category _selectedCategory, _newCategory;
        private Command _acceptCommand;
        private Command _addNewCategoryCommand;
        private bool _categoryEditorVisible;

        public ICommand AcceptCommand => _acceptCommand;
        public ICommand AddNewCategoryCommand => _addNewCategoryCommand;

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (SetProperty(ref _selectedCategory, value))
                {
                    if (value?.IsNew == true)
                        ShowCategoryEditor();
                    else
                        HideCategoryEditor();
                }
            }
        }

        public Category NewCategory
        {
            get => _newCategory;
            private set => SetProperty(ref _newCategory, value);
        }

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            private set => SetProperty(ref _categories, value);
        }

        public bool CategoryEditorVisible
        {
            get => _categoryEditorVisible;
            private set => SetProperty(ref _categoryEditorVisible, value);
        }

        public override void Init(object initData)
        {
            if (_categories == null)
            {
                Categories = new ObservableCollection<Category>(_service.GetCategories());
                _categories.Add(new Category() { Name = "Add Category" });
            }
            if (initData is Category selectCategory)
            {
                SelectedCategory = _categories.SingleOrDefault(c => c.ID == selectCategory.ID);
            }

            MessagingCenter.Subscribe<ITodoService, Category>(this, Services.TodoService.ActionAdd, OnCategoryAdded);
            MessagingCenter.Subscribe<ITodoService, Category>(this, Services.TodoService.ActionUpdate, OnCategoryUpdated);
            MessagingCenter.Subscribe<ITodoService, Category>(this, Services.TodoService.ActionDelete, OnCategoryRemoved);
        }

        private async void OnSelectCategory()
        {
            await CoreMethods.PopPageModel(_selectedCategory);
        }

        private async void AddNewCategory()
        {
            try
            {
                await _service.AddCategoryAsync(_newCategory);
            }
            catch (Exception)
            {
                await CoreMethods.DisplayAlert("Error", "Error adding category", "OK");
            }
        }

        private bool CanAddNewCategory()
        {
            return !string.IsNullOrEmpty(_newCategory?.Name);
        }

        private void ShowCategoryEditor()
        {
            CategoryEditorVisible = true;
            NewCategory = new Category() { Color = Color.Accent };
        }

        private void HideCategoryEditor()
        {
            CategoryEditorVisible = false;
            NewCategory = null;
        }

        private void OnCategoryAdded(ITodoService sender, Category item)
        {
            int newIndex = Math.Max(_categories.Count - 1, 0);
            _categories.Insert(newIndex, item);
            this.SelectedCategory = item;
        }

        private void OnCategoryUpdated(ITodoService sender, Category item)
        {
            var category = _categories.Where(c => c.ID == item.ID).SingleOrDefault();
            if (category != null)
            {
                var index = _categories.IndexOf(category);
                _categories[index] = item;
            }
        }

        private void OnCategoryRemoved(ITodoService sender, Category item)
        {
            var category = _categories.Where(c => c.ID == item.ID).SingleOrDefault();
            if (category != null)
            {
                _categories.Remove(category);
            }
        }
    }
}
