using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.DataAccess.DTO
{
    public class TodoItemAlertDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Alert")]
        public int AlertID { get; set; }

        [ForeignKey("TodoItem")]
        public int TodoItemID { get; set; }

        public bool PlaySound { get; set; }

        public AlertDTO Alert { get; set; }

        public TodoItemDTO TodoItem { get; set; }
    }
}
