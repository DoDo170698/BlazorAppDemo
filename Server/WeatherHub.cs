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
using Newtonsoft.Json.Linq;
using System.ComponentModel;
//using System.Text.Json;

namespace Server
{
    public class WeatherHub : Hub
    {
        private const string _key = "SECRETKEYUSEDSYMMETRICENCRYPTION";
        private const string _iv = "19xj02lop5n9t8aw";

        public async Task GetWeatherData()
        {
            var dataBinary = await File.ReadAllTextAsync("WeatherData.txt");
            //var messagePackData = Convert.FromBase64String(dataBinary);

            await Clients.All.SendAsync("GetWeatherData", dataBinary);
        }
    }
}
