using AgentFrameworkBestPractices.AgentAsFunctionTool.Interfaces;
using AgentFrameworkBestPractices.AgentAsFunctionTool.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.AgentAsFunctionTool.Extensions;

public static class AsToolServiceExtensions
{
    public static void AddAsToolService(this IServiceCollection services)
    {
        services.AddScoped<IAgentAsToolService, AgentAsToolService>();
    }
}
