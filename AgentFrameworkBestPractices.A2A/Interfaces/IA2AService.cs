using A2A;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AgentFrameworkBestPractices.A2A.Interfaces;

public interface IA2AService
{
    Task<string> A2AAsFunctionTool(string message);
    IEnumerable<AIFunction> CreateFunctionTools(AIAgent a2aAgent, AgentCard agentCard);
    string FunctionNameSanitizer(string name);
}
