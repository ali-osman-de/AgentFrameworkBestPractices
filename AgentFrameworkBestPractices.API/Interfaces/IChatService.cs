namespace AgentFrameworkBestPractices.API.Interfaces
{
    public interface IChatService
    {

        Task<string> SendChatMessage(string message, CancellationToken cancellationToken);

    }
}
