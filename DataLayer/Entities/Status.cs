using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}