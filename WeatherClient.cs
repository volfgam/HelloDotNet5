using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace HelloDotNet5
{
    public class WeatherClient
    {
        private readonly HttpClient httpClient;
        private readonly ServiceSettings setthings;

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            this.httpClient = httpClient;
            setthings = options.Value;
        }

        public record Weather(string description);
        public record Main(decimal temp);
        public record Forecast(Weather[] weather, Main main, long dt);
        
        public async Task<Forecast> GetCurrentWeatherAsync(string city) {
            var forecast = await httpClient.GetFromJsonAsync<Forecast>($"https://{setthings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={setthings.ApiKey}&units=metric");
            return forecast;
        }
    }
}