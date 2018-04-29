using System.Collections.Generic;
using Domain.FaceDetection.DTO;

namespace Domain.FaceDetection
{
    public interface IFaceDetectionService
    {
        IEnumerable<FaceDetectionRequest> GetAllFaceDetections();
    }
}