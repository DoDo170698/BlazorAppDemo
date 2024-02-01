using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using MessagePack;
//using System.Text.Json;

namespace Server
{
    public class WeatherHub : Hub
    {
        private const string _key = "SECRETKEYUSEDSYMMETRICENCRYPTION";
        private const string _iv = "19xj02lop5n9t8aw";

        public async Task GetWeatherData()
        {
            // Đọc dữ liệu từ file JSON và đăng ký nó như một singleton service
            var dataBinary = File.ReadAllText("WeatherData.json");
            var weatherData = JsonConvert.DeserializeObject<List<WeatherForecast>>(dataBinary);

            string jsonData = JsonConvert.SerializeObject(weatherData);
            var messagePackData = MessagePackSerializer.Serialize(jsonData);

            await Clients.All.SendAsync("GetWeatherData", messagePackData);
        }
    }
}
