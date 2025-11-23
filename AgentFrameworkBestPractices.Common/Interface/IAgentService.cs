using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AgentFrameworkBestPractices.Common.Interface;

public interface IAgentService
{
    AIAgent CreateAgent(string model, string instructions, string name, string descriptions, List<AITool>? agentTools, ChatResponseFormat? responseFormat);

}
