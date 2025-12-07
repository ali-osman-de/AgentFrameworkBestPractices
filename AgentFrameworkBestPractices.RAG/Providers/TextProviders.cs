using AgentFrameworkBestPractices.RAG.Interfaces;
using AgentFrameworkBestPractices.RAG.Models;
using Microsoft.Agents.AI.Data;
using Microsoft.SemanticKernel.Connectors.Qdrant;

namespace AgentFrameworkBestPractices.RAG.Providers;

public class TextProviders : ITextProviders
{
    public TextSearchProviderOptions CreateTextSearchProviderOptionsAsync()
    {
        TextSearchProviderOptions textSearchOptions = new()
        {
            SearchTime = TextSearchProviderOptions.TextSearchBehavior.BeforeAIInvoke,
            RecentMessageMemoryLimit = 5
        };
        return textSearchOptions;
    }

    public Task<Func<string, CancellationToken, Task<IEnumerable<TextSearchProvider.TextSearchResult>>>> SearchTextAsync(QdrantCollection<Guid, DocumentationChunk> documentationCollection)
    {
        Func<string, CancellationToken, Task<IEnumerable<TextSearchProvider.TextSearchResult>>> searchFunc =
            async (text, ct) =>
            {
                List<TextSearchProvider.TextSearchResult> results = new();
                await foreach (var result in documentationCollection.SearchAsync(text, 50, cancellationToken: ct))
                {
                    results.Add(new TextSearchProvider.TextSearchResult
                    {
                        SourceName = result.Record.SourceName,
                        SourceLink = result.Record.SourceLink,
                        Text = result.Record.Text ?? string.Empty,
                        RawRepresentation = result
                    });
                }
                return results;
            };
        return Task.FromResult(searchFunc);
    }
}
