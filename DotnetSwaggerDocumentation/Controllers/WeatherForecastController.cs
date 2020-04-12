using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetSwaggerDocumentation.API.Controllers
{
    /// <summary>
    /// List the weather forecasts.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> logger;

        /// <summary>
        /// Constructs the controller state.
        /// </summary>
        /// <param name="logger">Injected logger by the dependency injection container.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Get the weather forecast 
        /// </summary>
        /// <returns>Returns a list of five weather forecast elements, with random temperatures
        /// between -20 and 55 degrees.</returns>
        /// <response code="200">Returns the list of weather forecast elements.</response>
        /// <response code="500">If an exception is thrown.</response>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();
        }
    }
}
