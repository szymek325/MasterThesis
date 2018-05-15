using System.Collections.Generic;
using Domain.SensorsReading;
using Domain.SensorsReading.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class SensorsController : Controller
    {
        private readonly ILogger<SensorsController> logger;
        private readonly IReadingsProvider readingsProvider;

        public SensorsController(ILogger<SensorsController> logger, IReadingsProvider readingsProvider)
        {
            this.logger = logger;
            this.readingsProvider = readingsProvider;
        }

        [HttpGet("[action]")]
        public IEnumerable<Reading> GetAll()
        {
            return readingsProvider.GetAllReadings();
        }

        [HttpGet("[action]")]
        public IEnumerable<DateOutput> GetAllDates()
        {
            return readingsProvider.GetDistinctDates();
        }

        [HttpGet("[action]")]
        public IEnumerable<Reading> GetDataPerDay(string day)
        {
            return readingsProvider.GetReadingsFromDay(day);
        }
    }
}