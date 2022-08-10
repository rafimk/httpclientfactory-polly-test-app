using Newtonsoft.Json;

namespace consumer.api.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient httpClient;
        public WeatherService()
        {   
            httpClient = new HttpClient();
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeather()
        {
            List<WeatherForecast> weatherForecast = new();

            var response = await httpClient.GetAsync("https://localhost:5000/weatherforecast");
		    response.EnsureSuccessStatusCode();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resp = await response.Content.ReadAsStringAsync();

                weatherForecast = JsonConvert.DeserializeObject<List<WeatherForecast>>(resp);
            }
            
            return weatherForecast == null ? new List<WeatherForecast>() : weatherForecast;
        }
    }
}