using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.FaceDetection.DTO;

namespace Domain.FaceDetection
{
    public interface IFaceDetectionService
    {
        Task<IEnumerable<FaceDetectionRequest>> GetAllFaceDetectionsAsync();
        Task<int> CreateRequest(NewRequest request);
        Task<FaceDetectionRequest> GetRequestData(int id);
    }
}