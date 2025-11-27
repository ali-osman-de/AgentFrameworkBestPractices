using AgentFrameworkBestPractices.API.Interfaces;
using AgentFrameworkBestPractices.API.Services;
using AgentFrameworkBestPractices.AgentAsFunctionTool.Extensions;
using AgentFrameworkBestPractices.Common.Extensions;
using AgentFrameworkBestPractices.FunctionCalling.Extensions;
using AgentFrameworkBestPractices.McpClientAsFunctionTool.Extensions;
using AgentFrameworkBestPractices.Plugins.Extensions;
using AgentFrameworkBestPractices.MultiConversation.Extensions;
using AgentFrameworkBestPractices.Workflows.Extensions;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Extensions;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Data;
using Microsoft.EntityFrameworkCore;

namespace AgentFrameworkBestPractices.API.Extensions;

public static class ServiceExtensions
{
    public static void AddServiceExtensions(this IServiceCollection services, IConfiguration configuration){

        services.AddScoped<IChatService, ChatService>();
        services.AddCommonServiceExtensions(); // single olması lazım agent üretimi

        services.AddMultiConversationService();
        services.AddFunctionToolService();
        services.AddAsToolService();
        services.AddPluginService();
        services.AddMcpClientAsTool();
        services.AddWorkflowService();
        services.AddToDoServiceExtension(configuration);
    }
}
