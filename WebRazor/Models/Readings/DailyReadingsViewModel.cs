using System;
using System.Collections.Generic;
using Domain.SensorsReading.DTO;

namespace WebRazor.Models.Readings
{
    public class DailyReadingsViewModel
    {
        public DailyReadingsViewModel(DateTime date, IEnumerable<Reading> readings)
        {
            Date = date;
            Readings = readings;
        }

        public DateTime Date { get; set; }
        public IEnumerable<Reading> Readings { get; set; }
    }
}