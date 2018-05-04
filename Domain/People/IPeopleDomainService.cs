using System.Threading.Tasks;
using Domain.People.DTO;

namespace Domain.People
{
    public interface IPeopleDomainService
    {
        Task<int> CreateNew(PersonInput input);
    }
}