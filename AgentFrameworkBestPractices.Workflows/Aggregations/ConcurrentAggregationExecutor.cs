using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;

namespace AgentFrameworkBestPractices.Workflows.Aggregations;

public sealed class ConcurrentAggregationExecutor : Executor<List<ChatMessage>>
{
    public ConcurrentAggregationExecutor(): base("ConcurrentAggregationExecutor")
    {
    }
    public override async ValueTask HandleAsync(
        List<ChatMessage> messages,
        IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {

        var formattedMessages = string.Join(
            Environment.NewLine,
            messages.Select(m => m.Text));

        await context.YieldOutputAsync(formattedMessages, cancellationToken);
    }
}
