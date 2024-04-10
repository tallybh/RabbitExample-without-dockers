using Microsoft.AspNetCore.Mvc;
using RabbitExample.Rabbit;

namespace RabbitExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRabbitProducer _rabbitProducer;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRabbitProducer rabbitProducer)
        {
            _logger = logger;
            _rabbitProducer = rabbitProducer;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _rabbitProducer.SendProductMessage<string>("From controller");
            _rabbitProducer.SendLogMessage<string>("Logging");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}