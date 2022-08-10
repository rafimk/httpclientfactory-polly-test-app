using consumer.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace consumer.api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherServiceController : ControllerBase
{
    
    private readonly IWeatherService _weatherService;
    private readonly ILogger<WeatherServiceController> _logger;

    public WeatherServiceController(IWeatherService weatherService, ILogger<WeatherServiceController> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await _weatherService.GetWeather();
    }

    [HttpGet("verify")]
    public ActionResult Verify(string eidNo)
    {
        var tempTotal = 0;
        var checkSum = 0;
        var multiplier = 1;
        for (var i = 0; i < 7; ++i) {
            tempTotal =  Convert.ToInt16(eidNo.Substring(i, 1)) * multiplier;
            if (tempTotal > 9) {
                tempTotal = Convert.ToInt16(tempTotal.ToString().Substring(0, 1)) + Convert.ToInt16(tempTotal.ToString().Substring(1, 1));
            }
            checkSum = checkSum + tempTotal;
            multiplier = (multiplier % 2 == 0) ? 1 : 2;
        }

        var result = (checkSum % 10);
        
        if ((checkSum % 10) != 0) {
            return Ok($"Not Valid {result}");
        };

        return Ok($"Valid {result}");
    }
}
