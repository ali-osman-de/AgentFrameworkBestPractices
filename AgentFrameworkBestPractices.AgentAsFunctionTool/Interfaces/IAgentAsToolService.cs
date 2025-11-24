namespace AgentFrameworkBestPractices.AgentAsFunctionTool.Interfaces;

public interface IAgentAsToolService
{
    Task<string> AgentAsToolChat(string message);
}
