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

        //private readonly List<WeatherForecast> _weatherData;
        //public WeatherHub(WeatherForecastBinary weatherForecastBinary)
        //{
        //    _weatherData = weatherForecastBinary.WeatherForecasts;
        //}

        public async Task GetWeatherData()
        {
            // Đọc dữ liệu từ file JSON và đăng ký nó như một singleton service
            var dataBinary = File.ReadAllText("WeatherData.json");
            var weatherData = JsonConvert.DeserializeObject<List<WeatherForecast>>(dataBinary);
            var weatherForecastBinary = new WeatherForecastBinary();
            string jsonData = JsonConvert.SerializeObject(weatherData);
            var messagePackData = MessagePackSerializer.Serialize(jsonData);

            //MemoryStream ms = new MemoryStream();
            //using (BsonWriter writer = new BsonWriter(ms))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(writer, weatherData);
            //}
            //string data = Convert.ToBase64String(ms.ToArray());

            //JsonSerializer jsonSerializer = new JsonSerializer();
            //MemoryStream objBsonMemoryStream = new MemoryStream();
            //BsonWriter bsonWriterObject = new BsonWriter(objBsonMemoryStream);
            //jsonSerializer.Serialize(bsonWriterObject, weatherData);

            weatherForecastBinary.EndDateEncrypted = DateTime.UtcNow;
            weatherForecastBinary.EncryptedDataByte = messagePackData;
            await Clients.All.SendAsync("GetWeatherData", weatherForecastBinary);
        }

        //public async Task GetWeatherData()
        //{
        //    // Đọc dữ liệu từ file JSON và đăng ký nó như một singleton service
        //    var dataBinary = File.ReadAllText("WeatherData.json");
        //    var weatherData = JsonSerializer.Deserialize<List<WeatherForecast>>(dataBinary);

        //    var data = new WeatherForecastBinary();
        //    string jsonData = JsonSerializer.Serialize(weatherData);
        //    string encryptedData = EncryptString(jsonData, _key); // Hàm mã hóa của bạn

        //    data.EndDateEncrypted = DateTime.UtcNow;
        //    data.EncryptedData = encryptedData;
        //    await Clients.All.SendAsync("GetWeatherData", data);
        //}

        //public async Task UpdateWeatherData(string encryptedData)
        //{
        //    string decryptedData = EncryptString(encryptedData, _key); // Hàm giải mã của bạn
        //    var updatedData = JsonSerializer.Deserialize<List<WeatherForecast>>(decryptedData);

        //    await Clients.All.SendAsync("ReceiveWeatherData", updatedData);
        //}

        public static string EncryptString(string text, string keyString)
        {
            byte[] iv = Encoding.ASCII.GetBytes(_iv);
            byte[] array;

            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(keyString);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(text);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }
    }
}
