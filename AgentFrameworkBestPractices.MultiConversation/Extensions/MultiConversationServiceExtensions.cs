using AgentFrameworkBestPractices.MultiConversation.Interfaces;
using AgentFrameworkBestPractices.MultiConversation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.MultiConversation.Extensions;

public static class MultiConversationServiceExtensions
{
    public static void AddMultiConversationService(this IServiceCollection services){

        services.AddSingleton<IMultiChatService, MultiChatService>();
    }
}
