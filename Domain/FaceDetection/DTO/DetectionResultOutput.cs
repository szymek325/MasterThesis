using System.Collections.Generic;
using DataLayer.Entities;
using Domain.Files.DTO;

namespace Domain.FaceDetection.DTO
{
    public class DetectionResultOutput
    {
        public FileLink Image { get; set; }
        public string DetectionTypeName { get; set; }
        public IEnumerable<FaceRectangle> FaceRectangles { get; set; }
    }
}