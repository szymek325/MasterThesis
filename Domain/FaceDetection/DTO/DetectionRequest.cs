using System;
using System.Collections.Generic;
using Domain.Files.DTO;

namespace Domain.FaceDetection.DTO
{
    public class DetectionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? CompletionTime { get; set; }
        public FileLink FileLink { get; set; }
        public IEnumerable<DetectionResultOutput> Results { get; set; }
    }
}