using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;

namespace AgentFrameworkBestPractices.Workflows.Interfaces;

public interface ICreateWorkflow
{
    Workflow GetConcurrentWorkflowAsync(AIAgent firstAgent, AIAgent secondAgent);
}
