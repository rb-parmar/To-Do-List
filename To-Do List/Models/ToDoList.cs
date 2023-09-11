using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace To_Do_List.Models
{
    public class ToDoList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [DisplayName("List Name")]
        public string ListName { get; set; }

        public HashSet<ToDoItem> Items { get; set; } = new HashSet<ToDoItem>();
    }
}
