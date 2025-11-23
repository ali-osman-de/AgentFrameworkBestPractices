namespace AgentFrameworkBestPractices.MultiConversation.Interfaces;

public interface IMultiChatService
{

    Task<string> ChatWithThreads(string message);

}
