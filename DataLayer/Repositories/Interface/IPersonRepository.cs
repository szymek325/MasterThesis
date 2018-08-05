using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        IEnumerable<Person> GetAllPeopleWithImages();
        Person GetPersonById(int id);
    }
}