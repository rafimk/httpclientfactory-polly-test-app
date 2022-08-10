namespace consumer.api.Services
{
    public interface IWeatherService
    {
         Task<IEnumerable<WeatherForecast>> GetWeather();
    }
}