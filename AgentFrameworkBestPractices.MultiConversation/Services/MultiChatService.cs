using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.MultiConversation.Interfaces;
using Microsoft.Agents.AI;

namespace AgentFrameworkBestPractices.MultiConversation.Services;

public class MultiChatService : IMultiChatService
{
    private readonly IAgentService _agentService;
    private readonly AIAgent multiChatAgent;
    private readonly AgentThread thread;

    public MultiChatService(IAgentService agentService)
    {
        _agentService = agentService;
        multiChatAgent = _agentService.CreateAgent(helloAgentOptions["model"], helloAgentOptions["instruction"], helloAgentOptions["name"], helloAgentOptions["description"], null, null);
        thread = multiChatAgent.GetNewThread();
    }

    Dictionary<string, string> helloAgentOptions = new Dictionary<string, string>()
    {
        { "model", "gpt-4o" },
        { "name", "MultiThreadAgent" },
        { "instruction", "You are a conversation agent. you can help for the asking questions." },
        { "description", "Your task is just answer the questions" }
    };

    public async Task<string> ChatWithThreads(string message)
    {           
        AgentRunResponse response = await multiChatAgent.RunAsync(message, thread);
        return response.Text;
    }
}
