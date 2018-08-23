using System;
using System.Collections.Generic;
using Domain.SensorsReading;
using Domain.SensorsReading.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebRazor.Models.Readings;

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
            var dates = readingsProvider.GetDistinctDates();
            var model = new AllDatesViewModel(dates);
            return View(model);
        }

        public IActionResult DailyReadings(DateTime day)
        {
            var readings = readingsProvider.GetReadingsFromDay(day);
            var model = new DailyReadingsViewModel(day, readings);
            return View(model);
        }
    }
}