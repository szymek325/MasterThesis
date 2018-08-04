using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.NeuralNetwork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebRazor.Models.Detection;
using WebRazor.Models.NeuralNetworks;

namespace WebRazor.Controllers
{
    public class NeuralNetworkController : Controller
    {
        private readonly ILogger<NeuralNetworkController> logger;
        private readonly IMapper mapper;
        private readonly INeuralNetworkService neuralNetworkService;

        public NeuralNetworkController(INeuralNetworkService neuralNetworkService, IMapper mapper,
            ILogger<NeuralNetworkController> logger)
        {
            this.neuralNetworkService = neuralNetworkService;
            this.mapper = mapper;
            this.logger = logger;
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

        public IActionResult NewNeuralNetwork()
        {
            return View();
        }

        public async Task<IActionResult> Create(NewDetectionViewModel model)
        {
            if (!ModelState.IsValid)
                return View("NewNeuralNetwork", model);
            try
            {
                var response = await neuralNetworkService.Create("", "");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error");
            }

            return RedirectToAction("AllNeuralNetworks", "NeuralNetwork", new {area = ""});
        }
    }
}
