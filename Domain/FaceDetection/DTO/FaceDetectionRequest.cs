using System.Collections.Generic;
using Domain.Files.DTO;

namespace Domain.FaceDetection.DTO
{
    public class FaceDetectionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int DnnFaces { get; set; }
        public int HaarFaces { get; set; }
        public IEnumerable<FileLink> FileLinks { get; set; }
    }
}