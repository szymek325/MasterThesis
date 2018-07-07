using DataLayer.Entities.Common;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public class FileRepository : GenericRepository<IImage>, IFileRepository
    {
        public FileRepository(MasterContext context) : base(context)
        {
        }
    }
}