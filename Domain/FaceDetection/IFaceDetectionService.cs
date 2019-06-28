using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.FaceDetection.DTO;

namespace Domain.FaceDetection
{
    public interface IFaceDetectionService
    {
        Task<IEnumerable<DetectionRequest>> GetAllFaceDetectionsAsync();
        Task<int> CreateRequest(NewRequest request);
        Task<DetectionRequest> GetRequestData(int id);
    }
}