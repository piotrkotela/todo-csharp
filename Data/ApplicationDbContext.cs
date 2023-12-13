using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using todo_csharp.Models;
using todo_csharp.Controllers;

namespace todo_csharp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
