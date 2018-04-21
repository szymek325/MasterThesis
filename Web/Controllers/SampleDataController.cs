using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly ILogger<SampleDataController> logger;
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public SampleDataController(ILogger<SampleDataController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
        }
    }
}