using AgentFrameworkBestPractices.Plugins.Providers;
using Microsoft.Extensions.AI;

namespace AgentFrameworkBestPractices.Plugins.Plugins;
public sealed class AgentPlugin
{
    private readonly WeatherProvider _weatherProvider;

    public AgentPlugin(WeatherProvider weatherProvider)
    {
        _weatherProvider = weatherProvider;
    }
    public string GetWeather(string cityName)
    {
        return _weatherProvider.GetWeather(cityName);
    }

    public IEnumerable<AITool> AsAITools()
    {
        yield return AIFunctionFactory.Create(this.GetWeather);
    }
}

