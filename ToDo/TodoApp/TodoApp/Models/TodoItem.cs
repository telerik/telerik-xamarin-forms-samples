using System;

namespace TodoApp.Models
{
    public class TodoItem : BindableBase
    {
        public TodoItem()
        {
        }

        public TodoItem(int id, string name, string content, bool completed, DateTime? start, TimeSpan? duration, bool allday,
            Category category, TodoItemAlert alert, TodoItemRecurrence recurrence, Priority priority)
        {
            ID = id;
            _name = name;
            _content = content;
            _completed = completed;
            _start = start;
            _duration = duration;
            _allDay = allday;
            _category = category;
            _alert = alert;
            _recurrence = recurrence;
            _priority = priority;
        }

        private string _name, _content;
        private bool _completed, _allDay;
        private DateTime? _start;
        private TimeSpan? _duration;
        private Category _category;
        private TodoItemAlert _alert;
        private TodoItemRecurrence _recurrence;
        private Priority _priority;

        public bool IsNew => ID <= 0;

        public int ID { get; }
        public bool AllDay
        {
            get => _allDay;
            set => SetProperty(ref _allDay, value);
        }
        public bool Completed
        {
            get => _completed;
            set => SetProperty(ref _completed, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public DateTime? Start
        {
            get => _start;
            private set => SetProperty(ref _start, value);
        }
        public TimeSpan? Duration
        {
            get => _duration;
            private set => SetProperty(ref _duration, value);
        }
        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }
        public TodoItemAlert Alert
        {
            get => _alert;
            set
            {
                if (SetProperty(ref _alert, value))
                {
                    this.RaisePropertyChanged(nameof(HasAlert));
                }
            }
        }
        public TodoItemRecurrence Recurrence
        {
            get => _recurrence;
            set
            {
                if (SetProperty(ref _recurrence, value))
                {
                    this.RaisePropertyChanged(nameof(IsRecurring));
                }
            }
        }
        public Priority Priority
        {
            get => _priority;
            set => SetProperty(ref _priority, value);
        }

        public bool HasStartAndDuration => this.Start != null && this.Duration != null;
        public bool HasAlert => this.Alert != null;
        public bool IsRecurring => this.Recurrence != null;
        public bool HasPriority => this.Priority != null;
        public string StartAndDuration
        {
            get
            {
                if (!this.HasStartAndDuration)
                    return null;

                if (this.Duration < TimeSpan.FromDays(1))
                {
                    return $"{this.Start.Value.ToShortDateString()} {this.Start.Value.ToShortTimeString()} - {this.Start.Value.Add(this.Duration.Value).ToShortTimeString()}";
                }
                else if (this.Duration == TimeSpan.FromDays(1))
                {
                    return this.Start.Value.ToShortDateString();
                }
                else
                {
                    return $"{this.Start.Value.ToShortDateString()} - {this.Start.Value.Add(this.Duration.Value).ToShortDateString()}";
                }
            }
        }

        public void SetStartAndDuration(DateTime? start, TimeSpan? duration, bool isAllDay)
        {
            this.Start = start;
            this.Duration = duration;
            this.AllDay = isAllDay;
            this.RaisePropertyChanged(nameof(StartAndDuration));
        }
    }
}
