namespace BlazorWeb.Models
{
    public class WeatherForecastBinary
    {
        public string? EncryptedData { get; set; }
        public byte[]? EncryptedDataByte { get; set; }
        public DateTime? StartDateEncrypted { get; set; }
        public DateTime? EndDateEncrypted { get; set; }
    }
}
