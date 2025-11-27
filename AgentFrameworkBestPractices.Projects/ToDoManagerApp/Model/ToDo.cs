using System.Text.Json.Serialization;

namespace AgentFrameworkBestPractices.Projects.ToDoManagerApp.Model;

public class ToDo
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

}
