using AgentFrameworkBestPractices.API.Interfaces;
using AgentFrameworkBestPractices.API.Services;

namespace AgentFrameworkBestPractices.API.Extensions;

public static class ServiceExtensions
{
    public static void AddServiceExtensions(this IServiceCollection services){

        services.AddScoped<IChatService, ChatService>();

    }
}
