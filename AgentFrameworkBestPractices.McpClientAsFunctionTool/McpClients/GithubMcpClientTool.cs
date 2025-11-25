using AgentFrameworkBestPractices.McpClientAsFunctionTool.Interfaces;
using ModelContextProtocol.Client;

namespace AgentFrameworkBestPractices.McpClientAsFunctionTool.McpClients;

public class GithubMcpClientTool : IGithubMcpClientTool
{
    public async Task<McpClient> GithubMcpClient()
    {
        var mcpGithubClient = await McpClient.CreateAsync(new StdioClientTransport(new()
        {
            Name = "MCPServer",
            Command = "npx",
            Arguments = ["-y", "--verbose", "@modelcontextprotocol/server-github"],
        }));

        return mcpGithubClient;
    }
}
