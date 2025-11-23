namespace AgentFrameworkBestPractices.FunctionCalling.Interfaces;

public interface IFuncToolChatService
{
    Task<string> ChatFuncTool(string message);
}
