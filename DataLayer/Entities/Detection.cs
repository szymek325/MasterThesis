using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Detection : EntityBase
    {
        public string Name { get; set; }
        public int DnnFaces { get; set; }
        public int HaarFaces { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public IEnumerable<DetectionImage> Images { get; set; }
    }
}