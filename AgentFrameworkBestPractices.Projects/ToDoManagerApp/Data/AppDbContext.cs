using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Model;
using Microsoft.EntityFrameworkCore;

namespace AgentFrameworkBestPractices.Projects.ToDoManagerApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ToDo> ToDos { get; set; }

}
