using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public  class DetectionImageRepository : GenericRepository<DetectionImage>, IDetectionImageRepository
    {
        public DetectionImageRepository(MasterContext context) : base(context)
        {
        }
    }
}