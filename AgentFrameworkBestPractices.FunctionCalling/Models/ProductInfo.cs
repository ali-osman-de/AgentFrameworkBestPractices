using System.Text.Json.Serialization;

namespace AgentFrameworkBestPractices.FunctionCalling.Models;

public class ProductInfo
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    [JsonPropertyName("rating")]
    public RatingInfo Rating { get; set; }
    public class RatingInfo
    {
        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

}
