using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;

namespace AgentFrameworkBestPractices.Common.Interface;

public interface IAgentService
{
    AIAgent CreateAgent(string model, string instructions, string name, string descriptions, List<AITool>? agentTools, ChatResponseFormat? responseFormat);

    OpenAIClient CreateEmbedding(IChatClient chatClient);

}
