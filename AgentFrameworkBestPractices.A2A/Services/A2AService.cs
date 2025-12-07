using System.Text.RegularExpressions;
using A2A;
using AgentFrameworkBestPractices.A2A.Interfaces;
using AgentFrameworkBestPractices.Common.Interface;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AgentFrameworkBestPractices.A2A.Services;

public partial class A2AService : IA2AService
{
    private readonly IAgentService _agentService;

    public A2AService(IAgentService agentService)
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
    public async Task<string> A2AAsFunctionTool(string message)
    {
        string a2aAgentHostUri = "exampleLocalHostUri"; // TODO: replace with your A2A agent host
        A2ACardResolver a2ACardResolver = new A2ACardResolver(new Uri(a2aAgentHostUri));

        AgentCard agentCard = await a2ACardResolver.GetAgentCardAsync();

        AIAgent a2aAgent = agentCard.GetAIAgent();

        AIAgent baseAgent = _agentService.CreateAgent(helloAgentOptions["model"], 
                                                            helloAgentOptions["instruction"], 
                                                            helloAgentOptions["name"], 
                                                            helloAgentOptions["description"], 
                                                            [.. CreateFunctionTools(a2aAgent, agentCard)],
                                                            null);
        AgentRunResponse agentRunResponse = await baseAgent.RunAsync(message);

        return agentRunResponse.Text;
    }


    public IEnumerable<AIFunction> CreateFunctionTools(AIAgent a2aAgent, AgentCard agentCard)
    {
        foreach (var skill in agentCard.Skills)
        {
            AIFunctionFactoryOptions options = new()
            {
                Name = FunctionNameSanitizer(skill.Name),
                Description = $$"""
                {
                    "description": "{{skill.Description}}",
                    "tags": "[{{string.Join(", ", skill.Tags ?? [])}}]",
                    "examples": "[{{string.Join(", ", skill.Examples ?? [])}}]",
                    "inputModes": "[{{string.Join(", ", skill.InputModes ?? [])}}]",
                    "outputModes": "[{{string.Join(", ", skill.OutputModes ?? [])}}]"
                }
                """,
            };

            yield return AIFunctionFactory.Create(RunAgentAsync, options);
        }

        async Task<string> RunAgentAsync(string input, CancellationToken cancellationToken)
        {
            var response = await a2aAgent.RunAsync(input, cancellationToken: cancellationToken).ConfigureAwait(false);

            return response.Text;
        }
    }

    public string FunctionNameSanitizer(string name)
    {
        return Regex.Replace(name, "_", "");
    }

}
