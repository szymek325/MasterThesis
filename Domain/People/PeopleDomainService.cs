using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Files;
using Domain.People.DTO;
using DropboxIntegration.Files;
using Microsoft.Extensions.Logging;

namespace Domain.People
{
    public class PeopleDomainService : IPeopleDomainService
    {
        private readonly IFilesClient filesClient;
        private readonly IFileRepository filesRepository;
        private readonly IFilesDomainService filesService;
        private readonly ILogger<PeopleDomainService> logger;
        private readonly IMapper mapper;
        private readonly IPersonRepository peopleRepo;

        public PeopleDomainService(IFilesClient filesClient, IFilesDomainService filesService,
            ILogger<PeopleDomainService> logger, IMapper mapper, IPersonRepository peopleRepo,
            IFileRepository filesRepository)
        {
            this.filesClient = filesClient;
            this.filesService = filesService;
            this.logger = logger;
            this.mapper = mapper;
            this.peopleRepo = peopleRepo;
            this.filesRepository = filesRepository;
        }

        public async Task<int> CreateNew(PersonInput input)
        {
            await filesService.Upload(input.Files, $"/people/{input.Name}");
            var person = new Person
            {
                Name = input.Name,
                Files = input.Files.Select(x => new File
                {
                    Name = x.FileName,
                    Path = $"/people/{input.Name}",
                    FileSourceId = 1
                }).ToList()
            };
            try
            {
                peopleRepo.Add(person);
                peopleRepo.Save();
            }
            catch (Exception ex)
            {
                logger.LogError("exception when saving new Person", ex);
                throw;
            }

            return person.Id;
        }

        public async Task<IEnumerable<PersonOutput>> GetAllPeople()
        {
            var people = peopleRepo.GetAllPeople().ToList();
            try
            {
                foreach (var person in people)
                {
                    if (person.ThumbFile != null) continue;
                    person.ThumbFile = await filesClient.DownloadThumbnail(person.Files.FirstOrDefault()?.Path,
                        person.Files.FirstOrDefault()?.Name);
                    peopleRepo.Update(person);
                    peopleRepo.Save();
                }
            }
            catch (Exception ex)
            {
                logger.LogError("exception when downloading all People", ex);
                throw;
            }

            var respone = mapper.Map<IEnumerable<PersonOutput>>(people);
            return respone;
        }

        public async Task<PersonOutput> GetPersonById(int id)
        {
            try
            {
                var person = peopleRepo.GetPersonById(id);
                var filesWithoutUrl = person.Files.Where(x => x.Url == null).ToList();
                if (filesWithoutUrl.Any())
                {
                    var links = await filesService.GetLinksToFilesInFolder($"/people/{person.Name}");
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
            catch (Exception ex)
            {
                logger.LogError("Exception when retrieving person");
                throw;
            }
        }

        public async Task DeletePersonById(int id)
        {
            try
            {
                var person = peopleRepo.GetPersonById(id);
                peopleRepo.Delete(person.Id);
                peopleRepo.Save();
                await filesService.Delete(person.Files);
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when retrieving person", ex);
                throw;
            }
        }
    }
}