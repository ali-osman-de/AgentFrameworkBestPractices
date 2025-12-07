using AgentFrameworkBestPractices.RAG.Models;
using Microsoft.Agents.AI.Data;
using Microsoft.SemanticKernel.Connectors.Qdrant;

namespace AgentFrameworkBestPractices.RAG.Interfaces;

public interface ITextProviders
{
    Task<Func<string, CancellationToken, Task<IEnumerable<TextSearchProvider.TextSearchResult>>>>  SearchTextAsync(QdrantCollection<Guid, DocumentationChunk> documentationCollection);

    TextSearchProviderOptions CreateTextSearchProviderOptionsAsync();
}
