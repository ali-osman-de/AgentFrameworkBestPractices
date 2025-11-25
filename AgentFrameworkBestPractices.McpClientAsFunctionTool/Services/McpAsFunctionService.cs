using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.McpClientAsFunctionTool.Interfaces;
using AgentFrameworkBestPractices.McpClientAsFunctionTool.McpClients;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

namespace AgentFrameworkBestPractices.McpClientAsFunctionTool.Services;

public class McpAsFunctionService : IMcpAsFunctionService
{
    private readonly IAgentService _agentService;
    private readonly IGithubMcpClientTool _githubMcpClientTool;

    public McpAsFunctionService(IAgentService agentService, IGithubMcpClientTool githubMcpClientTool)
    {
        _agentService = agentService;
        _githubMcpClientTool = githubMcpClientTool;
    }

    Dictionary<string, string> helloAgentOptions = new Dictionary<string, string>()
    {
        { "model", "gpt-4o" },
        { "name", "MultiFuncAgent" },
        { "instruction", "You are a multi function agent. you can help for the asking questions about the the topic and you generate the about topic." },
        { "description", "Your task is just answer what about the questions" }
    };

    public async Task<string> McpAsFuncChat(string message)
    {
        IList<McpClientTool> mcpTools = await _githubMcpClientTool.GithubMcpClient().Result.ListToolsAsync();

        AIAgent mcpFuncAgent = _agentService.CreateAgent(helloAgentOptions["model"],
                                                         helloAgentOptions["instruction"],
                                                         helloAgentOptions["name"],
                                                         helloAgentOptions["description"],
                                                         [.. mcpTools.Cast<AITool>()],
                                                         null);

        AgentRunResponse response = await mcpFuncAgent.RunAsync(message);
        return response.Text;
    }
}
