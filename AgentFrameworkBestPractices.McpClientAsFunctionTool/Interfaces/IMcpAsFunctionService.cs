namespace AgentFrameworkBestPractices.McpClientAsFunctionTool.Interfaces;

public interface IMcpAsFunctionService
{
    Task<string> McpAsFuncChat(string message);
}
