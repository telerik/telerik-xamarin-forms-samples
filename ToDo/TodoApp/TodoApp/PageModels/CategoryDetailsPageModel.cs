using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TodoApp.Models;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class CategoryDetailsPageModel : MainPageModelBase
    {
        public CategoryDetailsPageModel(Services.ITodoService todoService)
            : base(todoService)
        {
            this.completeItemCommand = new Command<TodoItem>(OnCompleteItem);
            this.deleteItemCommand = new Command<TodoItem>(OnDeleteItem);
            this.noItemsImageName = "no_tasks.png";
        }

        private Category _category;
        private TodoItem _selectedTask;
        private IReadOnlyList<TodoItem> _tasks;
        private bool _tasksUnavailable;
        private Command<TodoItem> completeItemCommand, deleteItemCommand;
        private string noItemsImageName;

        public ICommand CompleteItemCommand => this.completeItemCommand;
        public ICommand DeleteItemCommand => this.deleteItemCommand;
        public string NoItemsImageName => this.noItemsImageName;

        public Category Category
        {
            get => _category;
            private set => SetProperty(ref _category, value);
        }

        public bool TasksUnavailable
        {
            get => _tasksUnavailable;
            private set => SetProperty(ref _tasksUnavailable, value);
        }

        public IReadOnlyList<TodoItem> Tasks
        {
            get => _tasks;
            private set
            {
                if (SetProperty(ref _tasks, value))
                {
                    this.InvalidateTasksUnavailable();
                }
            }
        }

        public TodoItem SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (SetProperty(ref _selectedTask, value) && _selectedTask != null)
                    ShowTodoItemDetails(value.ID);
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            this.Category = this.TodoService.GetCategories().SingleOrDefault(c => c.ID == (int)initData);
            Device.BeginInvokeOnMainThread(() => this.Tasks = _category.Items);
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            this.SelectedTask = null;

            base.ViewIsDisappearing(sender, e);
        }

        protected override void OnTodoItemAdded(TodoItem newTodoItem)
        {
            if (newTodoItem.Category.ID == _category.ID && !this.Tasks.Contains(newTodoItem))
            {
                var newTasks = this._tasks.ToList();
                newTasks.Add(newTodoItem);
                this.Tasks = newTasks;
            }
            this.InvalidateTasksUnavailable();
        }

        protected override void OnTodoItemDeleted(TodoItem oldTodoItem)
        {
            if (oldTodoItem.Category.ID == _category.ID && this.Tasks.Contains(oldTodoItem))
            {
                var newTasks = this._tasks.ToList();
                newTasks.Remove(oldTodoItem);
                this.Tasks = newTasks;
            }
            this.InvalidateTasksUnavailable();
        }

        protected override void OnTodoItemUpdated(TodoItem updatedTodoItem)
        {
            var item = this.Tasks.SingleOrDefault(c => c.ID == updatedTodoItem.ID);
            if (item == null)
                return;

            if (updatedTodoItem.Category.ID != _category.ID)
            {
                var newTasks = this._tasks.ToList();
                newTasks.Remove(item);
                this.Tasks = newTasks;
            }
            else
            {
                item.Name = updatedTodoItem.Name;
                item.Completed = updatedTodoItem.Completed;
            }
            this.InvalidateTasksUnavailable();
        }

        protected override async void OnAddNewTodoItemCommandExecuted()
        {
            await this.CoreMethods.PushPageModel<TaskEditorPageModel>(new TodoItem() { Category = _category });
        }

        private void InvalidateTasksUnavailable()
        {
            TasksUnavailable = _tasks == null || _tasks.Count == 0;
        }

        private void ShowTodoItemDetails(int id)
        {
            var item = this.TodoService.GetTodoItem(id);
            this.CoreMethods.PushPageModel<TaskEditorPageModel>(item);
        }

        private async void OnCompleteItem(TodoItem item)
        {
            item.Completed = !item.Completed;
            try
            {
                await this.TodoService.UpdateTodoItemAsync(item);
            }
            catch (Exception)
            {
                await CoreMethods.DisplayAlert("Error", "Error marking item as done", "OK");
            }
        }

        private async void OnDeleteItem(TodoItem item)
        {
            if (await CoreMethods.DisplayAlert("Delete item", $"Delete item {item.Name}?", "Yes", "No"))
            {
                try
                {
                    await this.TodoService.DeleteAsync(item);
                }
                catch (Exception)
                {
                    await CoreMethods.DisplayAlert("Error", "Error deleting item", "OK");
                }
            }
        }
    }
}
