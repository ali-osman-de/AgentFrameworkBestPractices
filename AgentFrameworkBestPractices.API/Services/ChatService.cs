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

        public async Task<string> SendChatMessage(string message, CancellationToken cancellationToken)
        {
            AIAgent chatAgent = _agentService.CreateAgent("gpt-4o", "deneme versiyonu", "denemedir");

            AgentRunResponse agentResponse = await chatAgent.RunAsync(message);

            return agentResponse.Text;
        }
    }
}
