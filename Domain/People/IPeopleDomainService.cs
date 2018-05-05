using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.People.DTO;

namespace Domain.People
{
    public interface IPeopleDomainService
    {
        Task<int> CreateNew(PersonInput input);
        Task<IEnumerable<PersonOutput>> GetAllPeople();
        Task<PersonOutput> GetPersonById(int id);
        Task DeletePersonById(int id);
    }
}