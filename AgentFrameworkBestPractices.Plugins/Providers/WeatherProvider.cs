namespace AgentFrameworkBestPractices.Plugins.Providers;

public sealed class WeatherProvider
{
    public string GetWeather(string cityName)
    {
        return $"The weather in {cityName} is cloudy with a high of 15°C and partly cloudy.";
    }
}
