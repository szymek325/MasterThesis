using Domain.People.DTO;

namespace Domain.People
{
    public interface IPeopleDomainService
    {
        void CreateNew(PersonInput input);
    }
}