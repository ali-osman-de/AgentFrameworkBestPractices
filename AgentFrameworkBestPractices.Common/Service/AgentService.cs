using System.ClientModel;
using AgentFrameworkBestPractices.Common.Interface;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.VectorData;
using OpenAI;
using OpenAI.Files;
using OpenAI.VectorStores;

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

}
