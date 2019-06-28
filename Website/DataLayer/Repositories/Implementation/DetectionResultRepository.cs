using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class DetectionResultRepository : GenericRepository<DetectionResult>, IDetectionResultRepository
    {
        public DetectionResultRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<DetectionResult> GetResultsConnectedToRequest(int requestId)
        {
            return GetAll().Include(x => x.DetectionType).Include(x => x.Image).Where(x => x.DetectionId == requestId);
        }
    }
}