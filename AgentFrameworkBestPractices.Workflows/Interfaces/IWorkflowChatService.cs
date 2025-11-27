namespace AgentFrameworkBestPractices.Workflows.Interfaces;

public interface IWorkflowChatService
{
    Task<string> ConcurrentWorkflowChat(string message);
}
