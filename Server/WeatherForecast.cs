namespace Server
{
    [Serializable]
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public double TemperatureC { get; set; }

        public double TemperatureF => 32 + (double)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class WeatherForecastBinary
    {
        public WeatherForecastBinary() { StartDateEncrypted = DateTime.UtcNow; }
        public string? EncryptedData { get; set; }
        public byte[]? EncryptedDataByte { get; set; }
        public DateTime? StartDateEncrypted { get; set; }
        public DateTime? EndDateEncrypted { get; set; }
    }

    public class WeatherForecastJson
    {
        public List<WeatherForecast> WeatherForecasts { get; set; } = new List<WeatherForecast>();
    }
}
