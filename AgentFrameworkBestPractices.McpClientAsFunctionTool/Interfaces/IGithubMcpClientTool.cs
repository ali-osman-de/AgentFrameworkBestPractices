using ModelContextProtocol.Client;

namespace AgentFrameworkBestPractices.McpClientAsFunctionTool.Interfaces;

public interface IGithubMcpClientTool
{
    Task<McpClient> GithubMcpClient();
}
