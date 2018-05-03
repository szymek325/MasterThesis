using System;

namespace Domain.SensorsReading.DTO
{
    public class Reading
    {
        public int Id { get; set; }

        public int Humidity { get; set; }
        public int Temperature { get; set; }
        public DateTime CreationTime { get; set; }
    }
}