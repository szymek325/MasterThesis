using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Files.DTO;
using Domain.People;
using Domain.People.DTO;
using Microsoft.Extensions.Logging;
using PeopleUploader.Services.Interfaces;

namespace PeopleUploader.Services.Implementation
{
    public class PeopleCreator : IPeopleCreator
    {
        private readonly IFilesProvider filesProvider;
        private readonly ILogger<PeopleCreator> logger;
        private readonly IPeopleService peopleService;

        public PeopleCreator(ILogger<PeopleCreator> logger, IPeopleService peopleService, IFilesProvider filesProvider)
        {
            this.logger = logger;
            this.peopleService = peopleService;
            this.filesProvider = filesProvider;
        }

        public async Task AddPeopleToSystem()
        {
            try
            {
                var files = filesProvider.GetFiles().ToList();
                var distinctPerson = files.Select(x => x.PersonName).Distinct().ToList();
                Console.WriteLine($"Found {distinctPerson.Count} distinct people");
                logger.LogInformation($"Found {distinctPerson.Count} distinct people");
                foreach (var person in distinctPerson)
                {
                    var personFilesToUpload = files.Where(x => x.PersonName == person).Select(x => new FileToUpload
                    {
                        FileName = x.Name,
                        FileStream = x.FileStream
                    });
                    var personInput = new PersonInput
                    {
                        Files = personFilesToUpload,
                        Name = person
                    };
                    Console.WriteLine($"Uploading {personInput.Name} person with {personInput.Files.Count()}");
                    logger.LogInformation($"Uploading {personInput.Name} person with {personInput.Files.Count()}");
                    await peopleService.CreateNew(personInput);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception when adding people");
            }
        }
    }
}