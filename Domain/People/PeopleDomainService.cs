using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Files;
using Domain.People.DTO;
using Dropbox.Api.TeamLog;
using DropboxIntegration.Files;

namespace Domain.People
{
    public class PeopleDomainService : IPeopleDomainService
    {
        private readonly IFilesDomainService filesService;
        private readonly IFilesManager filesManager;
        private readonly IPersonRepository peopleRepo;

        public PeopleDomainService(IFilesDomainService filesService, IFilesManager filesManager, IPersonRepository peopleRepo)
        {
            this.filesService = filesService;
            this.filesManager = filesManager;
            this.peopleRepo = peopleRepo;
        }

        public async Task<int> CreateNew(PersonInput input)
        {
            await filesService.Upload(input.Files, $"/people/{input.Name}");
            var files = input.Files.Select(x => new File
            {
                Name = x.FileName,
                Path = $"/people/{input.Name}",
                FileSourceId = 1
            }).ToList();
            var person = new Person
            {
                Name = input.Name,
                Files = files
            };
            try
            {
                peopleRepo.Add(person);
                peopleRepo.Save();
            }
            catch(Exception ex)
            {

            }

            return person.Id;
        }

        public async Task<IEnumerable<PersonOutput>> GetAllPeople()
        {
            var output = new List<PersonOutput>();
            try
            {
                var people = peopleRepo.GetAllPeople();
                foreach (var person in people)
                {
                    var thumb = await filesManager.DownloadThumbnail(person.Files.FirstOrDefault()?.Path,
                        person.Files.FirstOrDefault()?.Name);
                    output.Add(new PersonOutput
                    {
                        Id = person.Id,
                        Name = person.Name,
                        Thumbnail = thumb
                    });
                }
            }
            catch(Exception ex)
            {

            }


            return output;
        }
    }
}