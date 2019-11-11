using System;
using System.Collections.Generic;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Services;
using Xamarin.Forms;

namespace TodoApp.Views.ViewModels
{
    public class RecurrenceSelectViewModel : BindableBase
    {
        public RecurrenceSelectViewModel(ITodoService todoService, Action<TodoItemRecurrence> finishAction)
        {
            this.finishAction = finishAction;
            acceptCommand = new Command(OnAccept);

            this.recurrences = todoService.GetRecurrences();
            this.todoItemRecurrence = new TodoItemRecurrence();
        }

        private Command acceptCommand;
        private IReadOnlyCollection<Recurrence> recurrences;
        private TodoItemRecurrence todoItemRecurrence;
        private Action<TodoItemRecurrence> finishAction;

        public ICommand AcceptCommand => acceptCommand;
        public IReadOnlyCollection<Recurrence> Recurrences => recurrences;
        public TodoItemRecurrence TodoItemRecurrence
        {
            get => this.todoItemRecurrence;
            set => SetProperty(ref this.todoItemRecurrence, value);
        }

        private void OnAccept()
        {
            this.finishAction(this.TodoItemRecurrence);
        }
    }
}
