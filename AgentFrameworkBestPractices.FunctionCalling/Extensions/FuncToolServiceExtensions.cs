using AgentFrameworkBestPractices.FunctionCalling.Interfaces;
using AgentFrameworkBestPractices.FunctionCalling.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.FunctionCalling.Extensions;

public static class FuncToolServiceExtensions
{
    public static void AddFunctionToolService(this IServiceCollection services){
        
        services.AddScoped<IFuncToolChatService, FuncToolChatService>();

    }
}
