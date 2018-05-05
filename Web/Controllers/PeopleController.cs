using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Files.DTO;
using Domain.People;
using Domain.People.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPeopleDomainService peopleService;

        public PeopleController(IMapper mapper, IPeopleDomainService peopleService)
        {
            this.mapper = mapper;
            this.peopleService = peopleService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(IFormCollection collections)
        {
            var files = mapper.Map<IEnumerable<FileToUpload>>(collections.Files);
            collections.TryGetValue("name", out var requestName);

            var response = await peopleService.CreateNew(new PersonInput
            {
                Name = requestName,
                Files = files
            });
            return Ok(new {person_id = response});
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonOutput>> GetAll()
        {
            return await peopleService.GetAllPeople();
        }

        [HttpGet("[action]")]
        public async Task<PersonOutput> GetPerson(int id)
        {
            var request = await peopleService.GetPersonById(id);
            return request;
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            await peopleService.DeletePersonById(id);
            return Ok(new {deteled_id = id});
        }
    }
}