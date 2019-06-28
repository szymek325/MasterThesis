using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.FaceDetection.DTO;

namespace Domain.FaceDetection
{
    public interface IDetectionResultService
    {
        Task<IEnumerable<DetectionResultOutput>> GetResultsForRequest(int requestId);
    }
}