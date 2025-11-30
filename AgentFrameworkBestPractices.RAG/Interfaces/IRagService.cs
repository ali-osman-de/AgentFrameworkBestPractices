namespace AgentFrameworkBestPractices.RAG.Interfaces;

public interface IRagService
{
    Task<string> RagChat(string message);
    
}
