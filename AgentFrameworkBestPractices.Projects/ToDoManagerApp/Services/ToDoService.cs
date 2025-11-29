using AgentFrameworkBestPractices.Common.Interface;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Interfaces;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Model;
using AgentFrameworkBestPractices.Projects.ToDoManagerApp.Tools;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFrameworkBestPractices.Projects.ToDoManagerApp.Services;

public class ToDoService : IToDoService
{
    private readonly IAgentService _agentService;
    private readonly ToDoManagerTool _toDoManagerTool;
    Dictionary<string, string> ToDoAgentOptions = new Dictionary<string, string>()
    {
        { "model", "gpt-4o" },
        { "name", "To-Do Manager Agent" },
        { "instruction", "You are a To Do Manager Agent, You have some tools you can do anything with that tools" },
        { "description", "Your task is do something about the input with tools." }
    };

    public ToDoService(IAgentService agentService, ToDoManagerTool toDoManagerTool)
    {
        _agentService = agentService;
        _toDoManagerTool = toDoManagerTool;
    }

    public async Task<string> ToDoChat(string message)
    {
        List<AITool> toDoAgentTools = [ AIFunctionFactory.Create(_toDoManagerTool.AddNewToDo),
                                        AIFunctionFactory.Create(_toDoManagerTool.ListToDo),
                                        AIFunctionFactory.Create(_toDoManagerTool.RemoveToDo),
                                        AIFunctionFactory.Create(_toDoManagerTool.UpdateToDo) ];

        // AgentRunOptions toDoAgentRunOptions = new ChatClientAgentRunOptions()
        // {
        //     ChatOptions = new ChatOptions()
        //     {
        //         ResponseFormat = ChatResponseFormat.ForJsonSchema<ToDo>(schemaDescription: "To-Do Response Model")
        //     }
        // };

        AIAgent toDoAgent = _agentService.CreateAgent(ToDoAgentOptions["model"],
                                                      ToDoAgentOptions["instruction"],
                                                      ToDoAgentOptions["name"],
                                                      ToDoAgentOptions["description"],
                                                      toDoAgentTools,
                                                      null);

        AgentThread agentThread = toDoAgent.GetNewThread();

        AgentRunResponse response = await toDoAgent.RunAsync(message, agentThread);
        
        return response.Text;
    }
}
