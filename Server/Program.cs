using Server;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Thêm SignalR
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

//// Đọc dữ liệu từ file JSON và đăng ký nó như một singleton service
//var dataBinary = File.ReadAllText("WeatherData.json");
//var weatherForecastBinary = new WeatherForecastBinary { WeatherForecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(dataBinary) ?? new List<WeatherForecast>() };
//if (weatherForecastBinary != null)
//{
//    builder.Services.AddSingleton(weatherForecastBinary);
//}

//var dataJson = File.ReadAllText("WeatherDataJson.json");
//var weatherForecastJson = new WeatherForecastJson { WeatherForecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(dataJson) ?? new List<WeatherForecast>() };
//if (weatherForecastJson != null)
//{
//    builder.Services.AddSingleton(weatherForecastJson);
//}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseDefaultFiles();
app.UseStaticFiles();

//app.UseStaticFiles(new StaticFileOptions()
//{
//    OnPrepareResponse = context =>
//    {
//        context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
//        context.Context.Response.Headers["Pragma"] = "no-cache";
//        context.Context.Response.Headers.Add("Expires", "-1");
//    }
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    // Kích hoạt SignalR hub
    endpoints.MapHub<WeatherHub>("/weatherHub");
    endpoints.MapHub<WeatherHubJson>("/weatherHubJson");
});

app.Run();
