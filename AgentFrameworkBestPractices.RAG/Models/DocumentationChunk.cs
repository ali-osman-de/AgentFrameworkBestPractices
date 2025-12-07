using Microsoft.Extensions.VectorData;

namespace AgentFrameworkBestPractices.RAG.Models;

public class DocumentationChunk
{
    [VectorStoreKey]
    public Guid Key { get; set; }
    [VectorStoreData]
    public string SourceLink { get; set; } = string.Empty;
    [VectorStoreData]
    public string SourceName { get; set; } = string.Empty;
    [VectorStoreData]
    public string Text { get; set; } = string.Empty;
    [VectorStoreVector(Dimensions: 3072)]
    public string Embedding => this.Text;
}
