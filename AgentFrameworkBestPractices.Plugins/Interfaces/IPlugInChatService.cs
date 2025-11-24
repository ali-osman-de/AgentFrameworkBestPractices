namespace AgentFrameworkBestPractices.Plugins.Interfaces;

public interface IPlugInChatService
{
    Task<string> PlugInChat(string message);
}
