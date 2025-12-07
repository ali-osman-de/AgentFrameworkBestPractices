using AgentFrameworkBestPractices.A2A.Interfaces;
using AgentFrameworkBestPractices.A2A.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.A2A.Extensions;

public static class A2AServiceExtensions
{
    public static IServiceCollection AddA2AServices(this IServiceCollection services)
    {
        return services.AddScoped<IA2AService, A2AService>();
    }
}
