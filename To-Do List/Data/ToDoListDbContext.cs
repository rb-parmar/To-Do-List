using Microsoft.EntityFrameworkCore;
using To_Do_List.Models;

namespace To_Do_List.Data
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext (DbContextOptions<ToDoListDbContext> options) : base(options) { }

        public DbSet<ToDoList> ToDoList { get; set; } = default!;
        public DbSet<ToDoItem> ToDoItem { get; set; } = default!;
    }
}
