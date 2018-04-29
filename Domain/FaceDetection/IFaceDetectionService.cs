using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.FaceDetection.DTO;

namespace Domain.FaceDetection
{
    public interface IFaceDetectionService
    {
        IEnumerable<FaceDetectionRequest> GetAllFaceDetections();
        int CreateRequest(NewRequest request);
    }
}