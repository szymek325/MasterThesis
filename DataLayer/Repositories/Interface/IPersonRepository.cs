using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        IEnumerable<Person> GetAllPeople();
        Person GetPersonById(int id);
    }
}