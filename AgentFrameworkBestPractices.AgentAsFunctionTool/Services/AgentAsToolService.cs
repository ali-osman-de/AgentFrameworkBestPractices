using AgentFrameworkBestPractices.AgentAsFunctionTool.Interfaces;
using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.FunctionCalling.Tools;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AgentFrameworkBestPractices.AgentAsFunctionTool.Services;

public class AgentAsToolService : IAgentAsToolService
{
    private readonly IAgentService _agentService;

    public AgentAsToolService(IAgentService agentService)
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

    Dictionary<string, string> WeatherAgentOptions = new Dictionary<string, string>()
    {
        { "model", "gpt-4o" },
        { "name", "Weather Agent" },
        { "instruction", "You are a weather agent. you can help for weather information" },
        { "description", "Your task is search weather and response the answer by your tool" }
    };

    public async Task<string> AgentAsToolChat(string message)
    {
        AITool weatherTool = AIFunctionFactory.Create(GetWeatherByCityTool.GetWeatherInformation);

        AIAgent weatherAgent = _agentService.CreateAgent(WeatherAgentOptions["model"],
                                                 WeatherAgentOptions["instruction"],
                                                 WeatherAgentOptions["name"],
                                                 WeatherAgentOptions["description"],
                                                 [weatherTool],
                                                 null);

        AIAgent mainAgent = _agentService.CreateAgent(MainAgentOptions["model"],
                                                      MainAgentOptions["instruction"],
                                                      MainAgentOptions["name"],
                                                      MainAgentOptions["description"],
                                                      [weatherAgent.AsAIFunction()],
                                                      null);

        AgentRunResponse response = await mainAgent.RunAsync(message);

        return response.Text;
    }
}
