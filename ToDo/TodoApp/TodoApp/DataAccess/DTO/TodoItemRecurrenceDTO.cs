using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.DataAccess.DTO
{
    public class TodoItemRecurrenceDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Recurrence")]
        public int RecurrenceID { get; set; }

        [ForeignKey("TodoItem")]
        public int TodoItemID { get; set; }

        public bool RepeatAfterCompletion { get; set; }

        public RecurrenceDTO Recurrence { get; set; }

        public TodoItemDTO TodoItem { get; set; }
    }
}
