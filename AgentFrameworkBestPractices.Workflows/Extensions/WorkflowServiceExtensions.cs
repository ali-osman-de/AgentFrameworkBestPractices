using AgentFrameworkBestPractices.Workflows.Interfaces;
using AgentFrameworkBestPractices.Workflows.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.Workflows.Extensions;

public static class WorkflowServiceExtensions
{
    public static void AddWorkflowService(this IServiceCollection services)
    {
        services.AddSingleton<ICreateWorkflow, CreateWorkflow>();
        services.AddScoped<IWorkflowChatService, WorkflowChatService>();
    }

}
