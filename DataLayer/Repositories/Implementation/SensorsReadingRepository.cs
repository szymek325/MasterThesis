using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public class SensorsReadingRepository : GenericRepository<SensorsReading>, ISensorsReadingRepository
    {
        public SensorsReadingRepository(MasterContext context) : base(context)
        {
        }
    }
}