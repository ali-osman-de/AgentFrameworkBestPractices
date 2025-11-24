using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.Plugins.Interfaces;
using AgentFrameworkBestPractices.Plugins.Plugins;
using Microsoft.Agents.AI;

namespace AgentFrameworkBestPractices.Plugins.Services;

public class PlugInChatService : IPlugInChatService
{
    private readonly IAgentService _agentService;
    private readonly AgentPlugin _agentPlugin;
    public PlugInChatService(IAgentService agentService, AgentPlugin agentPlugin)
    {
        _agentService = agentService;
        _agentPlugin = agentPlugin;
    }


    Dictionary<string, string> helloAgentOptions = new Dictionary<string, string>()
    {
        { "model", "gpt-4o" },
        { "name", "MultiFuncAgent" },
        { "instruction", "You are a multi function agent. you can help for the asking questions about the the topic and you generate the about topic." },
        { "description", "Your task is just answer what about the questions" }
    };
    public async Task<string> PlugInChat(string message)
    {
        var plugins = _agentPlugin.AsAITools().ToList(); // plugin mantığı aslında tool ile kombine edilmiş hali 
        AIAgent baseAgent = _agentService.CreateAgent(helloAgentOptions["model"],
                                                      helloAgentOptions["instruction"],
                                                      helloAgentOptions["name"],
                                                      helloAgentOptions["description"],
                                                      plugins,
                                                      null);

        AgentRunResponse runResponse = await baseAgent.RunAsync(message);

        return runResponse.Text;
    }
}
