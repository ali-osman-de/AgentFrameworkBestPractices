using System.Text.Json.Serialization;

namespace AgentFrameworkBestPractices.FunctionCalling.Models;

public class Response
{
    [JsonPropertyName("products")]
    public List<ProductInfo>? Products { get; set; }

    [JsonPropertyName("weather")]
    public WeatherInfo? Weather { get; set; }

}
