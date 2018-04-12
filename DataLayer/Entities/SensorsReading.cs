using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class SensorsReading
    {
        [Key]
        public int Id { get; set; }

        public int Humidity { get; set; }
        public int Temperature { get; set; }
    }
}