using System.ComponentModel;

namespace AgentFrameworkBestPractices.FunctionCalling.Tools;

public class GetWeatherByCityTool
{
        [Description("Get weather information when user answer is the city")]
        public static string GetWeatherInformation(
            [Description("city name")] string cityName)
        {
            return $"Today {cityName} is partly cloudy and 23 degree";
            
            // Bu kısımda api çağrıları yapılabilir bir sonraki örnekte ona bakacağız. // FindProductByNameTool bulabilirsin
        }

}
