using Microsoft.Agents.AI;

namespace AgentFrameworkBestPractices.Common.Interface;

public interface IAgentService
{
    AIAgent CreateAgent(string model, string instructions, string descriptions);

}
