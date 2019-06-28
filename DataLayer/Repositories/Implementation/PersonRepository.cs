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

        public IEnumerable<Person> GetAllPeopleWithImages()
        {
            return GetAll()
                .Include(x => x.Images)
                .OrderByDescending(x => x.CreationTime);
        }

        public Person GetPersonById(int id)
        {
            return GetAll()
                .Include(x => x.Images)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}