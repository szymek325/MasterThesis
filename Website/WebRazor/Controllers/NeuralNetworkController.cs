using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.NeuralNetwork;
using Domain.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebRazor.Models.NeuralNetworks;

namespace WebRazor.Controllers
{
    public class NeuralNetworkController : Controller
    {
        private readonly ILogger<NeuralNetworkController> logger;
        private readonly IMapper mapper;
        private readonly INeuralNetworkService neuralNetworkService;
        private readonly IPeopleService peopleService;

        public NeuralNetworkController(ILogger<NeuralNetworkController> logger, IMapper mapper,
            INeuralNetworkService neuralNetworkService,
            IPeopleService peopleService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.neuralNetworkService = neuralNetworkService;
            this.peopleService = peopleService;
        }

        public async Task<IActionResult> AllNeuralNetworks()
        {
            var requests = await neuralNetworkService.GetAll();
            var model = new NeuralNetworksViewModel
            {
                NeuralNetworks = requests
            };
            return View(model);
        }

        public async Task<IActionResult> NeuralNetwork(int id)
        {
            var request = await neuralNetworkService.GetById(id);
            return View(request);
        }

        public async Task<IActionResult> NewNeuralNetwork()
        {
            var people = await peopleService.GetPeopleCheckboxes();
            var model = new NewNeuralNetworkViewModel
            {
                PeopleCheckboxes = people.ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> Create(NewNeuralNetworkViewModel model)
        {
            if (!ModelState.IsValid)
                return View("NewNeuralNetwork", model);
            try
            {
                var checkedPeople = model.PeopleCheckboxes.Where(x => x.IsChecked);
                var peopleString = "";
                foreach (var checkedPerson in checkedPeople)
                    peopleString = peopleString + $",{checkedPerson.Id}";
                var response = await neuralNetworkService.Create(model.Name, peopleString, model.PhotosPerPerson);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error");
            }

            return RedirectToAction("AllNeuralNetworks", "NeuralNetwork", new {area = ""});
        }
    }
}