using AgentFrameworkBestPractices.McpClientAsFunctionTool.Interfaces;
using AgentFrameworkBestPractices.McpClientAsFunctionTool.McpClients;
using AgentFrameworkBestPractices.McpClientAsFunctionTool.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.McpClientAsFunctionTool.Extensions;

public static class McpClientAsFunctionExtension
{
    public static void AddMcpClientAsTool(this IServiceCollection services){

        services.AddSingleton<IGithubMcpClientTool, GithubMcpClientTool>();
        
        services.AddScoped<IMcpAsFunctionService, McpAsFunctionService>();
    }
}
