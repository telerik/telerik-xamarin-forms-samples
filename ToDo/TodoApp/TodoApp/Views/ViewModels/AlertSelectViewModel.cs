using System;
using System.Collections.Generic;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Services;
using Xamarin.Forms;

namespace TodoApp.Views.ViewModels
{
    public class AlertSelectViewModel : BindableBase
    {
        public AlertSelectViewModel(ITodoService todoService, Action<TodoItemAlert> finishAction)
        {
            this.finishAction = finishAction;
            acceptCommand = new Command(OnAccept);

            this.alerts = todoService.GetAlerts();
            this.todoItemAlert = new TodoItemAlert();
        }

        private Command acceptCommand;
        private IReadOnlyCollection<Alert> alerts;
        private TodoItemAlert todoItemAlert;
        private Action<TodoItemAlert> finishAction;

        public ICommand AcceptCommand => acceptCommand;
        public IReadOnlyCollection<Alert> Alerts => alerts;
        public TodoItemAlert TodoItemAlert
        {
            get => this.todoItemAlert;
            set => SetProperty(ref this.todoItemAlert, value);
        }

        private void OnAccept()
        {
            this.finishAction(this.TodoItemAlert);
        }
    }
}
