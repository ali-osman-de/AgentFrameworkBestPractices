using System.ClientModel;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Data;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using OpenAI;
using OpenAI.Files;
using OpenAI.VectorStores;
using Qdrant.Client;

namespace AgentFrameworkBestPractices.Common.Interface;

public interface IAgentService
{
    AIAgent CreateAgent(string model, string instructions, string name, string descriptions, List<AITool>? agentTools, ChatResponseFormat? responseFormat);
    #pragma warning disable OPENAI001
    Task<ClientResult<VectorStore>> CreateEmbeddingClient(string FileName,  ClientResult<OpenAIFile> File);

    Task<QdrantVectorStore> CreateEmbeddingClientWithQdrant(QdrantClient client, bool ownsClientOptions, OpenAIClient openAIClient);

    #pragma warning disable OPENAI001
    Task<ClientResult<OpenAIFile>>  CreateFileClient(string FilePath);
    OpenAIClient CreateOpenAIClient();

    AIAgent CreateRagAgent(OpenAIClient openAIClient, string model, string instructions, string name, string descriptions, List<AITool>? agentTools, TextSearchProviderOptions textSearchOptions, Func<string, CancellationToken, Task<IEnumerable<TextSearchProvider.TextSearchResult>>> SearchAdapter);
}
