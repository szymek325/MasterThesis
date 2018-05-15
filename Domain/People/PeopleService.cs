using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Configuration;
using Domain.Files;
using Domain.People.DTO;
using DropboxIntegration.Files;
using Microsoft.Extensions.Logging;

namespace Domain.People
{
    public class PeopleService : IPeopleService
    {
        private readonly IFileRepository filesRepository;
        private readonly IFilesDomainService filesService;
        private readonly IGuidProvider guid;
        private readonly ILogger<PeopleService> logger;
        private readonly IMapper mapper;
        private readonly IPersonRepository peopleRepo;

        public PeopleService(IFileRepository filesRepository, IFilesDomainService filesService, IGuidProvider guid,
            ILogger<PeopleService> logger, IMapper mapper, IPersonRepository peopleRepo)
        {
            this.filesRepository = filesRepository;
            this.filesService = filesService;
            this.guid = guid;
            this.logger = logger;
            this.mapper = mapper;
            this.peopleRepo = peopleRepo;
        }

        public async Task<int> CreateNew(PersonInput input)
        {
            try
            {
                var personGuid = guid.NewGuidAsString;
                await filesService.Upload(input.Files, $"{personGuid}");

                var person = new Person
                {
                    Name = input.Name,
                    Guid = personGuid,
                    Files = input.Files.Select(x => new File
                    {
                        Name = x.FileName,
                        ParentGuid = personGuid
                    }).ToList()
                };
                peopleRepo.Add(person);
                peopleRepo.Save();

                return person.Id;
            }
            catch (Exception ex)
            {
                logger.LogError("exception when saving new Person", ex);
                throw;
            }
        }

        public async Task<IEnumerable<PersonOutput>> GetAllPeople()
        {
            var people = peopleRepo.GetAllPeople().ToList();
            try
            {
                foreach (var person in people)
                    if (person.Files.Any() && string.IsNullOrWhiteSpace(person.Files.First().Thumbnail))
                        await filesService.GetThumbnail(person.Files.First());
            }
            catch (Exception ex)
            {
                logger.LogError("exception when downloading all People", ex);
            }

            var respone = mapper.Map<IEnumerable<PersonOutput>>(people);
            return respone;
        }

        public async Task<PersonOutput> GetPersonById(int id)
        {
            var person = peopleRepo.GetPersonById(id);
            var filesWithoutUrl = person.Files.Where(x => x.Url == null).ToList();
            if (filesWithoutUrl.Any())
            {
                var links = await filesService.GetLinksToFilesInFolder($"/{person.Guid}");
                foreach (var file in filesWithoutUrl)
                {
                    file.Url = links.FirstOrDefault(x => x.FileName == file.Name)?.Url;
                    filesRepository.Update(file);
                }

                filesRepository.Save();
            }

            var respone = mapper.Map<PersonOutput>(person);
            return respone;
        }

        public async Task DeletePersonById(int id)
        {
            try
            {
                var person = peopleRepo.GetPersonById(id);
                await filesService.DeleteFiles(person.Files);
                peopleRepo.Delete(person.Id);
                peopleRepo.Save();
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when retrieving person", ex);
                throw;
            }
        }
    }
}