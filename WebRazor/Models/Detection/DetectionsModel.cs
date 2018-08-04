using System.Collections.Generic;
using Domain.FaceDetection.DTO;

namespace WebRazor.Models.Detection
{
    public class DetectionsModel
    {
        public IEnumerable<DetectionRequest> DetectionRequests { get; set; }
    }
}