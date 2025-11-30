using AgentFrameworkBestPractices.Common.Interface;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.VectorData;
using OpenAI;

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

    public OpenAIClient CreateEmbedding(OpenAIClient chatClient)
    {
        // TO-DO
        VectorStore vectorStore = new InMemoryVectorStore(new()
        {
            EmbeddingGenerator = chatClient.GetEmbeddingClient(embeddingDeploymentName).AsIEmbeddingGenerator()
        });
    }
}
