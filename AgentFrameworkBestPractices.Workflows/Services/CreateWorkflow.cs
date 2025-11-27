using AgentFrameworkBestPractices.Workflows.Aggregations;
using AgentFrameworkBestPractices.Workflows.Interfaces;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;

namespace AgentFrameworkBestPractices.Workflows.Services;

public class CreateWorkflow : ICreateWorkflow
{
    public Workflow GetConcurrentWorkflowAsync(AIAgent firstAgent, AIAgent secondAgent)
    {

        var startExecutor = new ChatForwardingExecutor("Start");
        var aggregationExecutor = new ConcurrentAggregationExecutor();

        return new WorkflowBuilder(startExecutor)
            .AddFanOutEdge(startExecutor, [firstAgent, secondAgent])
            .AddFanInEdge([firstAgent, secondAgent], aggregationExecutor)
            .WithOutputFrom(aggregationExecutor)
            .Build();
    }
}
