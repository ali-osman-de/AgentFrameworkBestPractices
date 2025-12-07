using System.ClientModel;
using AgentFrameworkBestPractices.Common.Interface;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Data;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using OpenAI;
using OpenAI.Files;
using OpenAI.VectorStores;
using Qdrant.Client;

namespace AgentFrameworkBestPractices.Common.Service;

public class AgentService : IAgentService
{
    private readonly IConfiguration _configuration;

    public AgentService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public AIAgent CreateAgent(string model, string instructions, string name, string descriptions, List<AITool>? agentTools, ChatResponseFormat? responseFormat)
    {
      
        var aiAgent = new OpenAIClient(_configuration["Agent:ApiKey"]).GetChatClient(model)
                                                                      .CreateAIAgent(
                                                                          new ChatClientAgentOptions(
                                                                              instructions: instructions,
                                                                              name: name,
                                                                              description: descriptions,
                                                                              tools: agentTools
                                                                              )
                                                                          //{ ChatOptions = new()
                                                                          //{
                                                                          //    ResponseFormat = responseFormat
                                                                          //}}
                                                                      );

        return aiAgent;
    }

    public async Task<ClientResult<OpenAIFile>> CreateFileClient(string FilePath)
    {
        OpenAIFileClient fileClient = new OpenAIFileClient(_configuration["Agent:ApiKey"]);
        ClientResult<OpenAIFile> uploadResult = await fileClient.UploadFileAsync(
            filePath: FilePath,
            purpose: FileUploadPurpose.Assistants);

        return uploadResult;
    }

    #pragma warning disable OPENAI001
    public async Task<ClientResult<OpenAI.VectorStores.VectorStore>> CreateEmbeddingClient(string FileName,ClientResult<OpenAIFile> File)
    {
        var openAIClient = new OpenAIClient(_configuration["Agent:ApiKey"]);
        VectorStoreClient vectorStoreClient = openAIClient.GetVectorStoreClient();

        ClientResult<OpenAI.VectorStores.VectorStore> vectorStoreCreate = await vectorStoreClient.CreateVectorStoreAsync(options: new VectorStoreCreationOptions()
        {
            Name = FileName,
            FileIds = { File.Value.Id }
        });

        return vectorStoreCreate;
    }
    #pragma warning restore OPENAI001
    public OpenAIClient CreateOpenAIClient()
    {
        OpenAIClient openAIClient = new(_configuration["Agent:ApiKey"]);
        return openAIClient;
    }
    public Task<QdrantVectorStore> CreateEmbeddingClientWithQdrant(QdrantClient client, bool ownsClientOptions, OpenAIClient openAIClient)
    {
        var vectorStore = new QdrantVectorStore(client, ownsClient: ownsClientOptions, new()
        {
            EmbeddingGenerator = openAIClient.GetEmbeddingClient(_configuration["Agent:EmbeddingModel"]).AsIEmbeddingGenerator()
        });

        return Task.FromResult(vectorStore);
    }

    public AIAgent CreateRagAgent(OpenAIClient openAIClient, string model, string instructions, string name, string descriptions, List<AITool>? agentTools, TextSearchProviderOptions textSearchOptions, Func<string, CancellationToken, Task<IEnumerable<TextSearchProvider.TextSearchResult>>> SearchAdapter)
    {
        var aiAgent = openAIClient.GetChatClient(model)
                      .CreateAIAgent(
                          new ChatClientAgentOptions(
                              instructions: instructions,
                              name: name,
                              description: descriptions,
                              tools: agentTools)
                              {
                              AIContextProviderFactory = ctx => new TextSearchProvider(SearchAdapter, ctx.SerializedState, ctx.JsonSerializerOptions, textSearchOptions)
                              });

        return aiAgent;
    }
}
