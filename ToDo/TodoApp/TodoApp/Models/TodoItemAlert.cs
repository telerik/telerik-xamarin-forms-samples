namespace TodoApp.Models
{
    public class TodoItemAlert : BindableBase
    {
        public TodoItemAlert()
        {
        }

        public TodoItemAlert(Alert alert, bool playSound)
        {
            Alert = alert;
            PlaySound = playSound;
        }

        private Alert alert;
        private bool playSound;

        public Alert Alert
        {
            get => this.alert;
            set => SetProperty(ref this.alert, value);
        }

        public bool PlaySound
        {
            get => this.playSound;
            set => SetProperty(ref this.playSound, value);
        }
    }
}
