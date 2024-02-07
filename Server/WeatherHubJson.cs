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
        public async Task GetWeatherDataJson()
        {
            // Đọc dữ liệu từ file JSON và đăng ký nó như một singleton service
            var dataJson = await File.ReadAllTextAsync("WeatherDataJson.json");
            var weatherData = JsonSerializer.Deserialize<List<WeatherForecast>>(dataJson);
            await Clients.All.SendAsync("GetWeatherDataJson", weatherData);
        }
    }
}
