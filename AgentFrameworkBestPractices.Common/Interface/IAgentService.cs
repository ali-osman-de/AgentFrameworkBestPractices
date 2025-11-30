using System.ClientModel;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI.Files;
using OpenAI.VectorStores;

namespace AgentFrameworkBestPractices.Common.Interface;

public interface IAgentService
{
    AIAgent CreateAgent(string model, string instructions, string name, string descriptions, List<AITool>? agentTools, ChatResponseFormat? responseFormat);
    #pragma warning disable OPENAI001
    Task<ClientResult<VectorStore>> CreateEmbeddingClient(string FileName,  ClientResult<OpenAIFile> File);
    #pragma warning disable OPENAI001
    Task<ClientResult<OpenAIFile>>  CreateFileClient(string FilePath);
}
