using System;
using System.Collections.Generic;
using Domain.SensorsReading.DTO;

namespace Domain.SensorsReading
{
    public interface IReadingsProvider
    {
        IEnumerable<Reading> GetAllReadings();
        IEnumerable<DateOutput> GetDistinctDates();
        IEnumerable<Reading> GetReadingsFromDay(DateTime day);
        IEnumerable<Reading> GetReadingsFromDay(string day);
    }
}