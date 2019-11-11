using System;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Services;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public abstract class MainPageModelBase : FreshBasePageModelEx
    {
        public MainPageModelBase(ITodoService todoService)
        {
            this.todoService = todoService;
            this.addNewCommand = new Command(OnAddNewTodoItemCommandExecuted);
        }

        private ICommand addNewCommand;
        private Services.ITodoService todoService;

        public ICommand AddNewCommand => this.addNewCommand;
        protected Services.ITodoService TodoService => this.todoService;

        public override void Init(object initData)
        {
            base.Init(initData);

            MessagingCenter.Subscribe<ITodoService, TodoItem>(this, Services.TodoService.ActionAdd, OnTodoItemAdded);
            MessagingCenter.Subscribe<ITodoService, TodoItem>(this, Services.TodoService.ActionUpdate, OnTodoItemUpdated);
            MessagingCenter.Subscribe<ITodoService, TodoItem>(this, Services.TodoService.ActionDelete, OnTodoItemRemoved);
        }

        protected virtual async void OnAddNewTodoItemCommandExecuted()
        {
            await this.CoreMethods.PushPageModel<TaskEditorPageModel>(new TodoItem());
        }

        protected virtual void OnTodoItemAdded(TodoItem newTodoItem)
        {
        }

        protected virtual void OnTodoItemUpdated(TodoItem updatedTodoItem)
        {
        }

        protected virtual void OnTodoItemDeleted(TodoItem oldTodoItem)
        {
        }

        private void OnTodoItemAdded(ITodoService sender, TodoItem item) => OnTodoItemAdded(item);
        private void OnTodoItemUpdated(ITodoService sender, TodoItem item) => OnTodoItemUpdated(item);
        private void OnTodoItemRemoved(ITodoService sender, TodoItem item) => OnTodoItemDeleted(item);
    }
}
