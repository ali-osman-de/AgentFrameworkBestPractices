using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.Workflows.Interfaces;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;

namespace AgentFrameworkBestPractices.Workflows.Services;

public class WorkflowChatService : IWorkflowChatService
{
    private readonly IAgentService _agentService;
    private readonly ICreateWorkflow _createWorkFlow;
    public WorkflowChatService(IAgentService agentService, ICreateWorkflow createWorkFlow)
    {
        _agentService = agentService;
        _createWorkFlow = createWorkFlow;
    }

    Dictionary<string, string> firstAgentOptions = new Dictionary<string, string>()
    {
        { "model", "gpt-4o" },
        { "name", "English-Agent" },
        { "instruction", "You are a English agent. you should translate user input to English" },
        { "description", "you should translate user input to English" }
    };


    Dictionary<string, string> secondAgentOptions = new Dictionary<string, string>()
    {
        { "model", "gpt-4o" },
        { "name", "French-Agent" },
        { "instruction", "You are a French agent. you should translate user input to French" },
        { "description", "you should translate user input to French" }
    };

    public async Task<string> ConcurrentWorkflowChat(string message)
    {
        var agentFirst = _agentService.CreateAgent(firstAgentOptions["model"],
                                                   firstAgentOptions["instruction"],
                                                   firstAgentOptions["name"],
                                                   firstAgentOptions["description"],
                                                   null,
                                                   null);

        var agentSecond = _agentService.CreateAgent(secondAgentOptions["model"],
                                                    secondAgentOptions["instruction"],
                                                    secondAgentOptions["name"],
                                                    secondAgentOptions["description"],
                                                    null,
                                                    null);

        var workflow = _createWorkFlow.GetConcurrentWorkflowAsync(agentFirst, agentSecond);

        var agent = workflow.AsAgent("concurrentFlowTranslator-agent", "concurrentFlowTranslator Agent");

        AgentRunResponse response = await agent.RunAsync(message);

        return response.Text;
    }
}
