using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.FunctionCalling.Interfaces;
using AgentFrameworkBestPractices.FunctionCalling.Tools;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

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
        { "name", "FuncWeatherAgent" },
        { "instruction", "You are a weather agent. you can help for the asking questions about the weather." },
        { "description", "Your task is just answer the weather questions" }
    };

    public async Task<string> ChatFuncTool(string message)
    {
        List<AITool> agentTools = [ AIFunctionFactory.Create(FindWeatherByCityTool.GetWeatherInformation), 
                                    AIFunctionFactory.Create(FindProductByNameTool.GetProductFromFakeApi) ];
        
        AIAgent functionalAgent = _agentService.CreateAgent(helloAgentOptions["model"], 
                                                            helloAgentOptions["instruction"], 
                                                            helloAgentOptions["name"], 
                                                            helloAgentOptions["description"], 
                                                            agentTools);

        AgentRunResponse agentRunResponse = await functionalAgent.RunAsync(message);

        return agentRunResponse.Text;
    }
}
