using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.FunctionCalling.Interfaces;
using AgentFrameworkBestPractices.FunctionCalling.Models;
using AgentFrameworkBestPractices.FunctionCalling.Tools;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.Text.Json.Serialization.Metadata;

namespace AgentFrameworkBestPractices.FunctionCalling.Services;

public class FuncToolChatService : IFuncToolChatService
{
    private readonly IAgentService _agentService;

    public FuncToolChatService(IAgentService agentService)
    {
        _agentService = agentService;
    }

    Dictionary<string, string> helloAgentOptions = new Dictionary<string, string>()
    {
        { "model", "gpt-4o" },
        { "name", "MultiFuncAgent" },
        { "instruction", "You are a multi function agent. you can help for the asking questions about the the topic and you generate the about topic." },
        { "description", "Your task is just answer what about the questions" }
    };

    public async Task<string> ChatFuncTool(string message)
    {
        List<AITool> agentTools = [ AIFunctionFactory.Create(FindWeatherByCityTool.GetWeatherInformation),
                                    AIFunctionFactory.Create(FindProductByNameTool.GetProductFromFakeApi) ];
        
        AIAgent functionalAgent = _agentService.CreateAgent(helloAgentOptions["model"], 
                                                            helloAgentOptions["instruction"], 
                                                            helloAgentOptions["name"], 
                                                            helloAgentOptions["description"], 
                                                            agentTools,
                                                            null);

        AgentRunOptions productRunOptions = new ChatClientAgentRunOptions()
        {
            ChatOptions = new ChatOptions()
            {
                ResponseFormat = ChatResponseFormat.ForJsonSchema<Response>(schemaDescription: "A lot of type in response model")
            }
        };

        AgentRunResponse agentRunResponse = await functionalAgent.RunAsync(message, options: productRunOptions);

        return agentRunResponse.Text;
    }
}
