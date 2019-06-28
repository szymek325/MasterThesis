using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Migrations;
using Domain.Files.DTO;

namespace Domain.FaceDetection.DTO
{
    public class DetectionResultOutput
    {
        public FileLink Image { get; set; }
        public string DetectionTypeName { get; set; }
        public IEnumerable<FaceRectangle> FaceRectangles { get; set; }
        public string ProcessingTime { get; set; }
    }
}