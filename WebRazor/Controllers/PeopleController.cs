using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceDetection.DTO;
using Domain.Files.DTO;
using Domain.People;
using Domain.People.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebRazor.Models.Detection;
using WebRazor.Models.People;

namespace WebRazor.Controllers
{
    public class PeopleController: Controller
    {
        private readonly IMapper mapper;
        private readonly IPeopleService peopleService;
        private readonly ILogger<PeopleController> logger;

        public PeopleController(IMapper mapper, IPeopleService peopleService, ILogger<PeopleController> logger)
        {
            this.mapper = mapper;
            this.peopleService = peopleService;
            this.logger = logger;
        }

        public async Task<IActionResult> AllPeople()
        {
            var requests = await peopleService.GetAllPeople();
            var model = new PeopleViewModel
            {
                People = requests
            };
            return View(model);
        }

        public async Task<IActionResult> Person(int id)
        {
            var request = await peopleService.GetPersonById(id);
            return View(request);
        }

        public IActionResult NewPerson()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("New", model);
            }
            try
            {
                var files = mapper.Map<IEnumerable<FileToUpload>>(model.Files);

                var response = await peopleService.CreateNew(new PersonInput
                {
                    Name = model.Name,
                    Files = files
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error");
            }

            return RedirectToAction("AllPeople", "People", new { area = "" });
        }
    }
}
