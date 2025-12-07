
using AgentFrameworkBestPractices.RAG.Models;
using Microsoft.Extensions.VectorData;

namespace AgentFrameworkBestPractices.RAG.Interfaces;

public interface IRagService
{
    Task<string> RagChat(string message);
    
    Task<string> RagChatWithQdrant(string message);

    Task<bool> UploadDataFromMarkdown(string markdownUrl, string sourceName, VectorStoreCollection<Guid, DocumentationChunk> vectorStoreCollection, int chunkSize, int overlap);
}
