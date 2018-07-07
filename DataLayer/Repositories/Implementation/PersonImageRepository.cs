using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public  class PersonImageRepository : GenericRepository<PersonImage>, IPersonImageRepository
    {
        public PersonImageRepository(MasterContext context) : base(context)
        {
        }
    }
}