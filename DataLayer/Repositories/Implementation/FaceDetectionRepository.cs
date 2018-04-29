using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public class FaceDetectionRepository : GenericRepository<FaceDetection>, IFaceDetectionRepository
    {
        public FaceDetectionRepository(MasterContext context) : base(context)
        {
        }
    }
}