using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    private static readonly string API_KEY = "70241a17f2e2469554a9ae100e406e2e";
    private static readonly string API_URL = "http://api.openweathermap.org/data/2.5/weather";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Введите долготу:");
        string lon = Console.ReadLine();
        Console.WriteLine("Введите широту:");
        string lat = Console.ReadLine();

        try
        {
            string response = await GetWeatherDataAsync(lat, lon);
            ParseAndDisplayWeatherData(response);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка: " + e.Message);
        }

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    private static async Task<string> GetWeatherDataAsync(string lat, string lon)
    {
        string urlString = $"{API_URL}?lat={lat}&lon={lon}&appid={API_KEY}&units=metric&lang=ru";
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(urlString);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

    private static void ParseAndDisplayWeatherData(string response)
{
    var jsonObject = JObject.Parse(response);
    
    // Вывод всего JSON ответа
    Console.WriteLine("JSON ответ:");
    Console.WriteLine(jsonObject.ToString());

    string city = jsonObject["name"].ToString();
    double temp = double.Parse(jsonObject["main"]["temp"].ToString());
    int humidity = int.Parse(jsonObject["main"]["humidity"].ToString());
    string description = jsonObject["weather"][0]["description"].ToString();

    Console.WriteLine($"Город: {city}");
    Console.WriteLine($"Температура: {temp}°C");
    Console.WriteLine($"Влажность: {humidity}%");  
    Console.WriteLine($"Описание: {description}");
    Console.WriteLine("Hello, Sun!")
}

}
