using System.ClientModel;
using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.RAG.Interfaces;
using AgentFrameworkBestPractices.RAG.Models;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using OpenAI;
using OpenAI.Files;
using OpenAI.VectorStores;
using Qdrant.Client;

namespace AgentFrameworkBestPractices.RAG.Services;

public class RagService : IRagService
{
    private readonly IAgentService _agentService;
    private readonly ITextProviders _textProviders;
    public RagService(IAgentService agentService, ITextProviders textProviders)
    {
        _agentService = agentService;
        _textProviders = textProviders;
    }

    Dictionary<string, string> MainAgentOptions = new Dictionary<string, string>()
    {
        { "model", "gpt-4o" },
        { "name", "Main Agent" },
        { "instruction", "You are a main agent. you can help for the asking questions about the the topic and you generate the about topic." },
        { "description", "Your task is just answer what about the questions" }
    };

    public async Task<string> RagChat(string message)
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "contoso-outdoors-knowledge-base.md");

        ClientResult<OpenAIFile> file = await _agentService.CreateFileClient(filePath);
    
        #pragma warning disable OPENAI001
        var vectorStoreResult = await _agentService.CreateEmbeddingClient("contoso-outdoors-knowledge-base.md", file);
        VectorStore vectorStore = vectorStoreResult.Value;
        #pragma warning restore OPENAI001
        
        var fileSearchTool = new HostedFileSearchTool() { Inputs = [new HostedVectorStoreContent(vectorStore.Id)] };

        AIAgent ragAgent = _agentService.CreateAgent(MainAgentOptions["model"],
                                                     MainAgentOptions["instruction"],
                                                     MainAgentOptions["name"],
                                                     MainAgentOptions["description"],
                                                     [fileSearchTool],
                                                     null);

        AgentRunResponse response = await ragAgent.RunAsync(message);
        return response.Text;
    }

    public async Task<string> RagChatWithQdrant(string message)
    {
        var afOverviewUrl = "https://github.com/MicrosoftDocs/semantic-kernel-docs/blob/main/agent-framework/overview/agent-framework-overview.md";

        OpenAIClient openAIClient = _agentService.CreateOpenAIClient();
        QdrantClient qClient = new("localhost");
        
        QdrantVectorStore qdrantVectorStore = await _agentService.CreateEmbeddingClientWithQdrant(qClient, true, openAIClient);

        var documentationCollection = qdrantVectorStore.GetCollection<Guid, DocumentationChunk>("documentation");

        await UploadDataFromMarkdown(afOverviewUrl, "Agent Framework Overview",documentationCollection, 1000, 200);

        var searchTextFunc = await _textProviders.SearchTextAsync(documentationCollection);

        AIAgent qdrantRagAgent = _agentService.CreateRagAgent(openAIClient,
                                             MainAgentOptions["model"],
                                             MainAgentOptions["instruction"],
                                             MainAgentOptions["name"],
                                             MainAgentOptions["description"],
                                             null,
                                             _textProviders.CreateTextSearchProviderOptionsAsync(),
                                             searchTextFunc);

        AgentThread agentThread = qdrantRagAgent.GetNewThread();

        AgentRunResponse response = await qdrantRagAgent.RunAsync(message, agentThread);
        return response.Text;
    }

    public async Task<bool> UploadDataFromMarkdown(
        string markdownUrl,
        string sourceName,
        Microsoft.Extensions.VectorData.VectorStoreCollection<Guid, DocumentationChunk> vectorStoreCollection,
        int chunkSize,
        int overlap)
    {
        using HttpClient client = new();
        var markdown = await client.GetStringAsync(new Uri(markdownUrl));

        var chunks = new List<DocumentationChunk>();
        for (int i = 0; i < markdown.Length; i += chunkSize)
        {
            var chunk = new DocumentationChunk
            {
                Key = Guid.NewGuid(),
                SourceLink = markdownUrl,
                SourceName = sourceName,
                Text = markdown.Substring(i, Math.Min(chunkSize + overlap, markdown.Length - i))
            };
            chunks.Add(chunk);
        }

        await vectorStoreCollection.EnsureCollectionExistsAsync();

        await vectorStoreCollection.UpsertAsync(chunks);
        return true;
    }

}
