using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.DataAccess.DTO
{
    public class TodoItemDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public bool AllDay { get; set; }

        public bool Completed { get; set; }

        [Required]
        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime? Start { get; set; }

        public TimeSpan? Duration { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        [ForeignKey("Priority")]
        public int? PriorityID { get; set; }

        public CategoryDTO Category { get; set; }
        public TodoItemAlertDTO Alert { get; set; }
        public TodoItemRecurrenceDTO Recurrence { get; set; }
        public PriorityDTO Priority { get; set; }

        public void CopyFrom(TodoItemDTO other)
        {
            this.AllDay = other.AllDay;
            this.Completed = other.Completed;
            this.Name = other.Name;
            this.Content = other.Content;
            this.Start = other.Start;
            this.Duration = other.Duration;
            this.CategoryID = other.CategoryID;
            this.PriorityID = other.PriorityID;
            if (other.Alert != null)
            {
                this.Alert = new TodoItemAlertDTO()
                {
                    AlertID = other.Alert.AlertID,
                    PlaySound = other.Alert.PlaySound
                };
            }
            else
            {
                this.Alert = null;
            }
            if (other.Recurrence != null)
            {
                this.Recurrence = new TodoItemRecurrenceDTO()
                {
                    RecurrenceID = other.Recurrence.RecurrenceID,
                    RepeatAfterCompletion = other.Recurrence.RepeatAfterCompletion
                };
            }
            else
            {
                this.Recurrence = null;
            }
        }
    }
}
