using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Helpers;
using DataLayer.Repositories.Interface;
using Domain.Configuration;
using Domain.Files;
using Domain.People.DTO;
using Microsoft.Extensions.Logging;

namespace Domain.People
{
    public class PeopleService : IPeopleService
    {
        private readonly IFilesDomainService filesService;
        private readonly IGuidProvider guid;
        private readonly IImageRepository imageRepository;
        private readonly ILogger<PeopleService> logger;
        private readonly IMapper mapper;
        private readonly IPersonRepository peopleRepo;

        public PeopleService(IImageRepository imageRepository, IFilesDomainService filesService, IGuidProvider guid,
            ILogger<PeopleService> logger, IMapper mapper, IPersonRepository peopleRepo)
        {
            this.imageRepository = imageRepository;
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
                var person = new Person
                {
                    Name = input.Name,
                    Images = input.Files.Select(x => new ImageAttachment
                    {
                        Name = x.FileName,
                        ImageAttachmentTypeId = ImageTypes.Person
                    }).ToList()
                };
                peopleRepo.Add(person);
                peopleRepo.Save();

                await filesService.Upload(input.Files, $"{nameof(ImageTypes.Person)}/{person.Id}");

                return person.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "exception when saving new Person");
                throw;
            }
        }

        public async Task<IEnumerable<PersonOutput>> GetAllPeople()
        {
            var people = peopleRepo.GetAllPeopleWithImages().ToList();
            try
            {
                foreach (var person in people)
                    if (person.Images.Any() && string.IsNullOrWhiteSpace(person.Images.First().Thumbnail))
                        await filesService.GetThumbnail(person.Images.First());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "exception when downloading all People");
            }

            var respone = mapper.Map<IEnumerable<PersonOutput>>(people);
            return respone;
        }

        public async Task<PersonOutput> GetPersonById(int id)
        {
            var person = peopleRepo.GetPersonById(id);
            var filesWithoutUrl = person.Images.Where(x => x.Url == null).ToList();
            if (filesWithoutUrl.Any())
            {
                var links = await filesService.GetLinksToFilesInFolder($"{nameof(ImageTypes.Person)}/{person.Id}");

                foreach (var file in filesWithoutUrl)
                {
                    file.Url = links.FirstOrDefault(x => x.FileName == file.Name)?.Url;
                    imageRepository.Update(file);
                }

                imageRepository.Save();
            }

            var respone = mapper.Map<PersonOutput>(person);
            return respone;
        }

        public async Task DeletePersonById(int id)
        {
            try
            {
                var person = peopleRepo.GetPersonById(id);
                await filesService.DeleteFiles(person.Images);
                peopleRepo.Delete(person.Id);
                peopleRepo.Save();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception when retrieving person");
                throw;
            }
        }

        public async Task<IEnumerable<PersonAsCheckbox>> GetPeopleCheckboxes()
        {
            var people = peopleRepo.GetAll().ToList();
            var respone = mapper.Map<IEnumerable<PersonAsCheckbox>>(people);
            return respone;
        }
    }
}