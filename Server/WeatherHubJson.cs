using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;

namespace Server
{
    public class WeatherHubJson : Hub
    {
        //private readonly List<WeatherForecast> _weatherData;
        //public WeatherHubJson(WeatherForecastJson weatherForecastJson)
        //{
        //    _weatherData = weatherForecastJson.WeatherForecasts;
        //}

        public async Task GetWeatherDataJson()
        {
            // Đọc dữ liệu từ file JSON và đăng ký nó như một singleton service
            var dataJson = File.ReadAllText("WeatherDataJson.json");
            var weatherData = JsonSerializer.Deserialize<List<WeatherForecast>>(dataJson);
            await Clients.All.SendAsync("GetWeatherDataJson", weatherData);
        }
    }
}
