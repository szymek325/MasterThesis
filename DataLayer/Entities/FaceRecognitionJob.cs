using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class FaceRecognitionJob
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
    }
}