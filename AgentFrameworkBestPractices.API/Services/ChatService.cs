using AgentFrameworkBestPractices.API.Interfaces;
using AgentFrameworkBestPractices.Common.Interface;
using Microsoft.Agents.AI;
using System.Threading.Tasks;

namespace AgentFrameworkBestPractices.API.Services
{
    public class ChatService : IChatService
    {
        private readonly IAgentService _agentService;

        public ChatService(IAgentService agentService)
        {
            _agentService = agentService;
        }

        Dictionary<string, string> helloAgentOptions = new Dictionary<string, string>()
        {
            { "model", "gpt-4o" },
            { "name", "HelloAgent" },
            { "instruction", "You are greatful agent, someone come to you, you should say Merhabalar Efenim." },
            { "description", "Your task is just welcoming in the instruction" }
        };


        public async Task<string> SendChatMessage(string message, CancellationToken cancellationToken)
        {
            AIAgent chatAgent = _agentService.CreateAgent(helloAgentOptions["model"], helloAgentOptions["instruction"], helloAgentOptions["name"], helloAgentOptions["description"], null, null);

            AgentRunResponse agentResponse = await chatAgent.RunAsync(message);

            return agentResponse.Text;
        }
    }
}
