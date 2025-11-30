using System.ClientModel;
using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.RAG.Interfaces;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI.Files;
using OpenAI.VectorStores;

namespace AgentFrameworkBestPractices.RAG.Services;

public class RagService : IRagService
{
    private readonly IAgentService _agentService;
    public RagService(IAgentService agentService)
    {
        _agentService = agentService;
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
}
