using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.DataAccess.DTO
{
    public class CategoryDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public uint Color { get; set; }

        public List<TodoItemDTO> TodoItems { get; set; }

        public void CopyFrom(CategoryDTO other)
        {
            this.Name = other.Name;
            this.Color = other.Color;
        }
    }
}
