using AgentFrameworkBestPractices.FunctionCalling.Models;
using Microsoft.Agents.AI;
using System.ComponentModel;
using System.Text.Json;

namespace AgentFrameworkBestPractices.FunctionCalling.Tools;

public class FindProductByNameTool
{
    [Description("Get products from fakeApiStore for the user what you want")]
    public static async Task<List<ProductInfo>> GetProductFromFakeApi()
    {
        using var httpClient = new HttpClient();
        string url = "https://fakestoreapi.com/products";

        var response = await httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var products = JsonSerializer.Deserialize<List<ProductInfo>>(json);

        return products;

    }
}
