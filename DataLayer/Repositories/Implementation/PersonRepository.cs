using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return GetAll().Include(x => x.Files).AsEnumerable();
        }
    }
}