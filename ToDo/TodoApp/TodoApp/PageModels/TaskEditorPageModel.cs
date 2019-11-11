using System;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Services;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class TaskEditorPageModel : FreshBasePageModelEx
    {
        public TaskEditorPageModel(ITodoService todoService)
        {
            this.todoService = todoService;
            this.acceptCommand = new Command(OnAccept);
            this.selectCategoryCommand = new Command(OnSelectCategory);
            this.selectTaskContentCommand = new Command(OnSelectTaskContent);
            this.selectDateTimeCommand = new Command(OnSelectDateTime);
            this.selectPriorityCommand = new Command(OnSelectPriority);
            this.selectAlertCommand = new Command(OnSelectAlert);
            this.selectRecurrenceCommand = new Command(OnSelectRecurrence);

            this.alertSelectModel = new Views.ViewModels.AlertSelectViewModel(todoService, OnAlertSelected);
            this.recurrenceSelectModel = new Views.ViewModels.RecurrenceSelectViewModel(todoService, OnRecurrenceSelected);
        }

        private ICommand acceptCommand, selectCategoryCommand, selectTaskContentCommand, selectDateTimeCommand, selectPriorityCommand, selectAlertCommand, selectRecurrenceCommand;
        private ITodoService todoService;
        private string title;
        private TodoItem todoItem;
        private Views.ViewModels.AlertSelectViewModel alertSelectModel;
        private Views.ViewModels.RecurrenceSelectViewModel recurrenceSelectModel;

        public static string OpenPopup_SelectAlert_Action = "open_select_alert";
        public static string ClosePopup_SelectAlert_Action = "close_select_alert";
        public static string OpenPopup_SelectRecurrence_Action = "open_select_recurrence";
        public static string ClosePopup_SelectRecurrence_Action = "close_select_recurrence";

        public string Title
        {
            get => this.title;
            private set => SetProperty(ref this.title, value);
        }

        public string AcceptAction => this.todoItem.IsNew ? "Add" : "Update";

        public TodoItem TodoItem
        {
            get => this.todoItem;
            private set => SetProperty(ref this.todoItem, value);
        }

        public string TaskContent => this.todoItem.Content?.Replace(Environment.NewLine, " ") ?? "Add Note";
        public string TaskCategory => this.todoItem.Category?.Name ?? "Category";
        public string TaskPriority => this.todoItem.Priority?.Name ?? "Set Priority";
        public string TaskDuration => this.todoItem.StartAndDuration ?? "Set Date and Time";
        public string TaskAlert => this.todoItem.Alert?.Alert?.Description ?? "Alert";
        public string TaskRecurrence => this.todoItem.Recurrence?.Recurrence?.Description ?? "Recurrence";

        public ICommand AcceptCommand => this.acceptCommand;
        public ICommand SelectCategoryCommand => this.selectCategoryCommand;
        public ICommand SelectTaskContentCommand => this.selectTaskContentCommand;
        public ICommand SelectDateTimeCommand => this.selectDateTimeCommand;
        public ICommand SelectPriorityCommand => this.selectPriorityCommand;
        public ICommand SelectAlertCommand => this.selectAlertCommand;
        public ICommand SelectRecurrenceCommand => this.selectRecurrenceCommand;

        public Views.ViewModels.AlertSelectViewModel AlertSelectModel => this.alertSelectModel;
        public Views.ViewModels.RecurrenceSelectViewModel RecurrenceSelectModel => this.recurrenceSelectModel;

        public override void Init(object initData)
        {
            if (initData is TodoItem todoItemToEdit)
            {
                this.todoItem = todoItemToEdit;
            }

            Title = this.todoItem.IsNew ? "Add New" : (this.todoItem.Name ?? "Edit");
        }

        public override void ReverseInit(object returnedData)
        {
            switch (returnedData)
            {
                case Category category:
                    this.TodoItem.Category = category;
                    this.RaisePropertyChanged(nameof(this.TaskCategory));
                    break;
                case TextInputPageModel.Model model:
                    this.todoItem.Content = model.Text;
                    this.RaisePropertyChanged(nameof(this.TaskContent));
                    break;
                case DateTimeInputPageModel.Model model:
                    this.todoItem.SetStartAndDuration(model.StartDate, model.Duration, model.AllDay);
                    this.RaisePropertyChanged(nameof(this.TaskDuration));
                break;
                case Priority priority:
                    this.TodoItem.Priority = priority;
                    this.RaisePropertyChanged(nameof(this.TaskPriority));
                    break;
            }
        }

        private async void OnSelectCategory(object obj)
        {
            await this.CoreMethods.PushPageModel<CategoryPageModel>(this.todoItem.Category);
        }

        private async void OnSelectTaskContent()
        {
            await this.CoreMethods.PushPageModel<TextInputPageModel>(new TextInputPageModel.Model(this.todoItem.Content));
        }

        private async void OnSelectDateTime()
        {
            DateTime start = this.todoItem.Start ?? DateTime.Now;
            TimeSpan duration = this.todoItem.Duration ?? TimeSpan.FromHours(1);
            await this.CoreMethods.PushPageModel<DateTimeInputPageModel>(
                new DateTimeInputPageModel.Model(start, duration, this.todoItem.AllDay));
        }

        private async void OnSelectPriority()
        {
            await this.CoreMethods.PushPageModel<PriorityPageModel>(this.todoItem.Priority);
        }

        private void OnSelectAlert()
        {
            if (this.todoItem.Alert != null)
            {
                this.alertSelectModel.TodoItemAlert = this.todoItem.Alert;
            }
            MessagingCenter.Send<TaskEditorPageModel>(this, OpenPopup_SelectAlert_Action);
        }

        private void OnSelectRecurrence()
        {
            if (this.todoItem.Recurrence != null)
            {
                this.recurrenceSelectModel.TodoItemRecurrence = this.todoItem.Recurrence;
            }
            MessagingCenter.Send<TaskEditorPageModel>(this, OpenPopup_SelectRecurrence_Action);
        }

        private void OnAlertSelected(TodoItemAlert alert)
        {
            this.todoItem.Alert = alert;
            MessagingCenter.Send<TaskEditorPageModel>(this, ClosePopup_SelectAlert_Action);
            this.RaisePropertyChanged(nameof(this.TaskAlert));
        }

        private void OnRecurrenceSelected(TodoItemRecurrence recurrence)
        {
            this.todoItem.Recurrence = recurrence;
            MessagingCenter.Send<TaskEditorPageModel>(this, ClosePopup_SelectRecurrence_Action);
            this.RaisePropertyChanged(nameof(this.TaskRecurrence));
        }

        private bool IsValid => !string.IsNullOrEmpty(this.todoItem.Name) &&
            this.todoItem.Category != null;

        private async void OnAccept()
        {
            if (!this.IsValid)
            {
                await CoreMethods.DisplayAlert("Validation error", "Todo item needs Name and Category!", "OK");
                return;
            }

            bool success = false;
            try
            {
                if (this.todoItem.IsNew)
                    await todoService.AddTodoItemAsync(this.todoItem);
                else
                    await todoService.UpdateTodoItemAsync(this.todoItem);
                success = true;
            }
            catch (Exception)
            {
                string action = this.todoItem.IsNew ? "inserting" : "updating";
                await CoreMethods.DisplayAlert("Error", $"Error {action} item", "OK");
            }

            if (success)
            {
                await this.CoreMethods.PopPageModel(this.todoItem);
            }
        }
    }
}
