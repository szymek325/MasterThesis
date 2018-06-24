using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public  class RecognitionImageRepository : GenericRepository<RecognitionImage>, IRecognitionImageRepository
    {
        public RecognitionImageRepository(MasterContext context) : base(context)
        {
        }
    }
}