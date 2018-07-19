using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public class DetectionResultImageRepository : GenericRepository<DetectionResultImage>, IDetectionResultImageRepository
    {
        public DetectionResultImageRepository(MasterContext context) : base(context)
        {
        }
    }
}