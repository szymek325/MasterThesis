using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.FaceRecognition.DTO;

namespace Domain.FaceRecognition
{
    public interface IFaceRecognitionService
    {
        Task<IEnumerable<FaceRecoRequest>> GetAllFaceRecognitions();
        Task<int> CreateRequest(NewRequest request);
        Task<FaceRecoRequest> GetRequestData(int id);
    }
}