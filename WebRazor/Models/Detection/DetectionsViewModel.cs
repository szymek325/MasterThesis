using System.Collections.Generic;
using Domain.FaceDetection.DTO;

namespace WebRazor.Models.Detection
{
    public class DetectionsViewModel
    {
        public IEnumerable<DetectionRequest> DetectionRequests { get; set; }
    }
}