using System.ComponentModel;

namespace AgentFrameworkBestPractices.FunctionCalling.Tools;

public class FindProductByNameTool
{
    [Description("Get products from fakeApiStore for the user what you want")]
    public static async Task<string> GetProductFromFakeApi()
    {
        using var httpClient = new HttpClient();
        string url = "https://fakestoreapi.com/products";

        var response = await httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return json;
    }
}
