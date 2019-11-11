using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.DataAccess.DTO
{
    public class RecurrenceDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }

        public TimeSpan Recurrence { get; set; }
        public int RecurrenceMonths { get; set; }
    }
}
