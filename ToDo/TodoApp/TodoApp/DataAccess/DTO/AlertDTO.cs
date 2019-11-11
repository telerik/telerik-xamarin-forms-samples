using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.DataAccess.DTO
{
    public class AlertDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public TimeSpan TimeBeforeStart { get; set; }
    }
}
