using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.Common.Service;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.Common.Extensions;

public static class CommonServiceExtensions
{
    public static void AddCommonServiceExtensions(this IServiceCollection services){


        services.AddSingleton<IAgentService, AgentService>();

    }

}
