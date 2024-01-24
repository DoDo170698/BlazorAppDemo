using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;

namespace Server
{
    public class WeatherHub : Hub
    {
        private readonly List<WeatherForecast> _weatherData;
        private const string _key = "SECRETKEYUSEDSYMMETRICENCRYPTION";
        private const string _iv = "19xj02lop5n9t8aw";

        public WeatherHub(List<WeatherForecast> weatherData)
        {
            _weatherData = weatherData;
        }

        public async Task<string> GetWeatherData()
        {
            string jsonData = JsonSerializer.Serialize(_weatherData);
            string encryptedData = EncryptString(jsonData, _key); // Hàm mã hóa của bạn
            return encryptedData;
        }

        public async Task UpdateWeatherData(string encryptedData)
        {
            string decryptedData = EncryptString(encryptedData, _key); // Hàm giải mã của bạn
            var updatedData = JsonSerializer.Deserialize<List<WeatherForecast>>(decryptedData);

            // Cập nhật dữ liệu và broadcast lại cho tất cả các client
            _weatherData.Clear();
            if(updatedData != null)
            {
                _weatherData.AddRange(updatedData);
            }
            await Clients.All.SendAsync("ReceiveWeatherData", updatedData);
        }

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
