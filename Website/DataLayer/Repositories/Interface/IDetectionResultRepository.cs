using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface IDetectionResultRepository : IGenericRepository<DetectionResult>
    {
        IEnumerable<DetectionResult> GetResultsConnectedToRequest(int requestId);
    }
}