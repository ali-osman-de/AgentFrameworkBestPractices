using System.Text.Json.Serialization;

namespace AgentFrameworkBestPractices.FunctionCalling.Models;

public class WeatherInfo
{
    [JsonPropertyName("city")]
    public string City { get; set; }
}
