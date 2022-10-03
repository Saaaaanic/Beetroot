using Lesson24_ApiRequest;
using Newtonsoft.Json;

class Program
{
    static HttpClient client = new HttpClient();
    static readonly string key = "835c42534acc58d012a0eb621d668818";
    
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter latitude and longitude");
        var lon = double.Parse(Console.ReadLine());
        var lat = double.Parse(Console.ReadLine());
        Uri url = new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={key}");
        var data = await GetContent(url);
        ShowInfo(data);
    }

    static async Task<Data> GetContent(Uri url)
    {
        var content = await client.GetStringAsync(url);
        Console.WriteLine(content);
        var data = JsonConvert.DeserializeObject<Data>(content);
        return data;
    }

    static void ShowInfo(Data data)
    {
        Console.WriteLine($"Longitude: {data.Coord.Lon}\nLatitude: {data.Coord.Lat}");
        foreach (var weather in data.Weather)
        {
            Console.WriteLine($"Weather: {weather.Main}\nDescription: {weather.Description}");
        }
        Console.WriteLine($"Wind gust: {data.Wind.Gust}\nWind speed: {data.Wind.Speed}");
    }

}
//checked
