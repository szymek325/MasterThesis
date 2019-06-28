using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class SensorsReading : EntityBase
    {
        public int Humidity { get; set; }
        public int Temperature { get; set; }
    }
}