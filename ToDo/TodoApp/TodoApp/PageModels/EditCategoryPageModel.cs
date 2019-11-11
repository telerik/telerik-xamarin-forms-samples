using System;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Services;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class EditCategoryPageModel : FreshBasePageModelEx
    {
        public EditCategoryPageModel(ITodoService todoService)
        {
            _todoService = todoService;
            _acceptCommand = new Command(OnAccept, CanAccept);
        }

        private ITodoService _todoService;
        private Category _category;
        private Command _acceptCommand;
        private System.Threading.Tasks.Task _saveTask;

        public Category Category
        {
            get => _category;
            private set => SetProperty(ref _category, value);
        }

        public ICommand AcceptCommand => _acceptCommand;

        public string AcceptAction => _category.IsNew ? "Add" : "Update";

        public override void Init(object initData)
        {
            base.Init(initData);

            _saveTask = null;

            if (initData is Category category)
                Category = category;
        }

        private void OnAccept()
        {
            if (_saveTask != null)
                return;

            System.Threading.Tasks.Task<Category> task;
            if (_category.IsNew)
                task = _todoService.AddCategoryAsync(_category);
            else
                task = _todoService.UpdateCategoryAsync(_category);

            _saveTask = task.ContinueWith(antc =>
            {
                if (antc.Exception != null)
                {
                    System.Diagnostics.Debug.WriteLine(antc.Exception);
                    string action = _category.IsNew ? "inserting" : "updating";
                    CoreMethods.DisplayAlert("Error", $"Error {action} item", "OK");
                }

                if (antc.IsCompleted)
                {
                    this.CoreMethods.PopPageModel(_category);
                }
            }, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());

        }

        private bool CanAccept()
        {
            return !string.IsNullOrEmpty(_category?.Name);
        }
    }
}
