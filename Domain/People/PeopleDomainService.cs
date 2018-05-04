using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Files;
using Domain.People.DTO;
using Dropbox.Api.TeamLog;
using DropboxIntegration.Files;
using Microsoft.Extensions.Logging;

namespace Domain.People
{
    public class PeopleDomainService : IPeopleDomainService
    {
        private readonly IFilesDomainService filesService;
        private readonly IFilesClient filesClient;
        private readonly IPersonRepository peopleRepo;
        private readonly ILogger<PeopleDomainService> logger;
        private readonly IMapper mapper;

        public PeopleDomainService(IFilesDomainService filesService, IFilesClient filesClient, IPersonRepository peopleRepo, ILogger<PeopleDomainService> logger, IMapper mapper)
        {
            this.filesService = filesService;
            this.filesClient = filesClient;
            this.peopleRepo = peopleRepo;
            this.logger = logger;
            this.mapper = mapper;
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
            catch(Exception ex)
            {
                logger.LogError("exception when saving new Person",ex);
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
                    person.ThumbFile = await filesClient.DownloadThumbnail(person.Files.FirstOrDefault()?.Path, person.Files.FirstOrDefault()?.Name);
                    peopleRepo.Update(person);
                    peopleRepo.Save();
                }
            }
            catch(Exception ex)
            {
                logger.LogError("exception when downloading all People", ex);
                throw;
            }

            var respone = mapper.Map<IEnumerable<PersonOutput>>(people);
            return respone;
        }
    }
}