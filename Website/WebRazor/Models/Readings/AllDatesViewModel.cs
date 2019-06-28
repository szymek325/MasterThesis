using System.Collections.Generic;
using Domain.SensorsReading.DTO;

namespace WebRazor.Models.Readings
{
    public class AllDatesViewModel
    {
        public AllDatesViewModel(IEnumerable<DateOutput> dates)
        {
            Dates = dates;
        }

        public IEnumerable<DateOutput> Dates { get; }
    }
}