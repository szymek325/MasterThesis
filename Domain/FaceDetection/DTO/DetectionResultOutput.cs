using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities;
using Domain.Files.DTO;

namespace Domain.FaceDetection.DTO
{
    public class DetectionResultOutput
    {
        public int StartX { get; set; }
        public int EndX { get; set; }
        public int StartY { get; set; }
        public int EndY { get; set; }
        public FileLink Image { get; set; }
        public string DetectionTypeName { get; set; }   
    }
}
