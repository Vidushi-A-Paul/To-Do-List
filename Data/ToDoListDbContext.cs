using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Data
{
    public class ToDoListDbContext: DbContext
    {
        public ToDoListDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ToDoListApp.Models.ToDoItem> TodoItems { get; set; }
    }
}
