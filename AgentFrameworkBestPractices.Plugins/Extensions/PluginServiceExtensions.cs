using AgentFrameworkBestPractices.Plugins.Interfaces;
using AgentFrameworkBestPractices.Plugins.Plugins;
using AgentFrameworkBestPractices.Plugins.Providers;
using AgentFrameworkBestPractices.Plugins.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.Plugins.Extensions;

public static class PluginServiceExtensions
{
    public static void AddPluginService(this IServiceCollection services)
    {
        services.AddSingleton<WeatherProvider>();
        services.AddSingleton<AgentPlugin>();

        services.AddScoped<IPlugInChatService, PlugInChatService>();
    }
}
