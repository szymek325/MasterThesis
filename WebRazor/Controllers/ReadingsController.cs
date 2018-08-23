using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.SensorsReading;
using Domain.SensorsReading.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebRazor.Controllers
{
    public class ReadingsController : Controller
    {
        private readonly ILogger<ReadingsController> logger;
        private readonly IReadingsProvider readingsProvider;

        public ReadingsController(ILogger<ReadingsController> logger, IReadingsProvider readingsProvider)
        {
            this.logger = logger;
            this.readingsProvider = readingsProvider;
        }

        public IEnumerable<Reading> GetAll()
        {
            return readingsProvider.GetAllReadings();
        }

        public IActionResult AllDates()
        {
            var dates=readingsProvider.GetDistinctDates();
            return View(dates);

        }

        public IActionResult DatePerDay(string day)
        {
            var readings=readingsProvider.GetReadingsFromDay(day);
            return View(readings);
        }
    }
}
