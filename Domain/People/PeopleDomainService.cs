using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Files;
using Domain.People.DTO;

namespace Domain.People
{
    public class PeopleDomainService : IPeopleDomainService
    {
        private readonly IFilesDomainService filesService;
        private readonly IPersonRepository peopleRepo;

        public PeopleDomainService(IPersonRepository peopleRepo)
        {
            this.peopleRepo = peopleRepo;
        }

        public PeopleDomainService(IPersonRepository peopleRepo, IFilesDomainService filesService)
        {
            this.peopleRepo = peopleRepo;
            this.filesService = filesService;
        }

        public async Task<int> CreateNew(PersonInput input)
        {
            await filesService.Upload(input.Files, $"/people/{input.Name}");
            var files = input.Files.Select(x => new File
            {
                Name = x.FileName,
                FileSourceId = 1
            }).ToList();
            var person = new Person
            {
                Name = input.Name,
                Files = files
            };
            peopleRepo.Add(person);
            peopleRepo.Save();

            return person.Id;
        }
    }
}