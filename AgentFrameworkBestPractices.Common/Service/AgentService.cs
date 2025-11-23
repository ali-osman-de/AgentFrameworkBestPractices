using AgentFrameworkBestPractices.Common.Interface;
using Microsoft.Agents.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;

namespace AgentFrameworkBestPractices.Common.Service;

public class AgentService : IAgentService
{
    private readonly IConfiguration _configuration;

    public AgentService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public AIAgent CreateAgent(string model, string instructions, string descriptions)
    {
        #pragma warning disable OPENAI001
        var aiAgent = new OpenAIClient(_configuration["Agent:ApiKey"]).GetOpenAIResponseClient(model)
                                              .CreateAIAgent(
                                                  instructions: instructions,
                                                  name: "deneme",
                                                  description: descriptions
                                              );
        #pragma warning disable OPENAI001

        return aiAgent;
    }
}
