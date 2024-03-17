using Microsoft.EntityFrameworkCore;

namespace ToDoApi
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }

    }
}
