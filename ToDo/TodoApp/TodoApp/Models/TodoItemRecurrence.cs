namespace TodoApp.Models
{
    public class TodoItemRecurrence : BindableBase
    {
        public TodoItemRecurrence()
        {
        }

        public TodoItemRecurrence(Recurrence recurrence, bool repeatAfterCompletion)
        {
            Recurrence = recurrence;
            RepeatAfterCompletion = repeatAfterCompletion;
        }

        private Recurrence recurrence;
        private bool repeatAfterCompletion;

        public Recurrence Recurrence
        {
            get => this.recurrence;
            set => SetProperty(ref this.recurrence, value);
        }

        public bool RepeatAfterCompletion
        {
            get => this.repeatAfterCompletion;
            set => SetProperty(ref this.repeatAfterCompletion, value);
        }
    }
}
