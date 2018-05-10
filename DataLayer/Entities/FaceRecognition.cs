using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class FaceRecognition : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string Guid { get; set; }

        public int? NeuralNetworkId { get; set; }
        public NeuralNetwork NeuralNetwork { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public int? FileId { get; set; }
        public File File { get; set; }
    }
}