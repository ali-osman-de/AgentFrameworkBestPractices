using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Data;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Interfaces;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace AgentFrameworkBestPractices.Projects.ToDoManagerApp.Extensions;

public static class ToDoManagerExtensions
{
    public static void AddToDoServiceExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IToDoService, ToDoService>();
        services.AddScoped<DatabaseService>();
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("ToDoApp");
            options.UseSqlite(connectionString);
        });
    }
}
