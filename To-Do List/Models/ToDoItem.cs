using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace To_Do_List.Models
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Item name cannot be less than 3 characters")]
        [MaxLength(250, ErrorMessage = "Item name cannot be longer than 250 characters")]
        [DisplayName("Item Name")]
        public string ItemTitle { get; set; }

        [ForeignKey("List")]
        public int ListId { get; set; }
        public ToDoList List { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        // Source code link for date format
        // https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application
        public DateTime Date { get; set; }

        public PriorityEnum Priority { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [DisplayName("Is Complete?")]
        [DefaultValue(false)]
        public bool IsComplete { get; set; }

        public enum PriorityEnum
        {
            High,
            Medium,
            Low
        } 
    }
}
