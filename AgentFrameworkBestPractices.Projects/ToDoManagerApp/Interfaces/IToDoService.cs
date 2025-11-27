namespace AgentFrameworkBestPractices.Projects.ToDoManagerApp.Interfaces;

public interface IToDoService
{
    Task<string> ToDoChat(string message);
}
