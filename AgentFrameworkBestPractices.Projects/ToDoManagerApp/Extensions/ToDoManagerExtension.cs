using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Data;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Interfaces;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Tools;

namespace AgentFrameworkBestPractices.Projects.ToDoManagerApp.Extensions;

public static class ToDoManagerExtensions
{
    public static void AddToDoServiceExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IToDoService, ToDoService>();
        services.AddScoped<ToDoManagerTool>();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("ToDoApp"));
        });
    }
}
