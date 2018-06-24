using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public  class FileRepository : GenericRepository<File>, IFileRepository
    {
        public FileRepository(MasterContext context) : base(context)
        {
        }
    }
}