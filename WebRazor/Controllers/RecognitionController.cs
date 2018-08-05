using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceRecognition;
using Domain.FaceRecognition.DTO;
using Domain.Files.DTO;
using Domain.NeuralNetwork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using WebRazor.Models.Recognition;

namespace WebRazor.Controllers
{
    public class RecognitionController : Controller
    {
        private readonly IFaceRecognitionService faceRecognitionService;
        private readonly ILogger<RecognitionController> logger;
        private readonly IMapper mapper;
        private readonly INeuralNetworkService neuralNetworkService;

        public RecognitionController(IFaceRecognitionService faceRecognitionService,
            INeuralNetworkService neuralNetworkService, IMapper mapper,
            ILogger<RecognitionController> logger)
        {
            this.faceRecognitionService = faceRecognitionService;
            this.neuralNetworkService = neuralNetworkService;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IActionResult> AllRecognitions()
        {
            var requests = await faceRecognitionService.GetAllFaceRecognitions();
            var model = new RecognitionsViewModel
            {
                RecognitionRequests = requests
            };
            return View(model);
        }

        public async Task<IActionResult> Recognition(int id)
        {
            var request = await faceRecognitionService.GetRequestData(id);
            return View(request);
        }

        public async Task<IActionResult> NewRecognition()
        {
            var model = new NewRecognitionViewModel
            {
                NeuralNetworks = await GetNeuralNetworksSelectListItems()
            };
            return View(model);
        }

        public async Task<IActionResult> Create(NewRecognitionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.NeuralNetworks = await GetNeuralNetworksSelectListItems();
                return View("NewRecognition", model);
            }

            try
            {
                var file = mapper.Map<FileToUpload>(model.File);

                var response = await faceRecognitionService.CreateRequest(new NewRequest
                {
                    Name = model.Name,
                    NeuralNetworkId = model.NeuralNetworkId,
                    Files = new List<FileToUpload> {file}
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error");
            }

            return RedirectToAction("AllRecognitions", "Recognition", new {area = ""});
        }

        private async Task<List<SelectListItem>> GetNeuralNetworksSelectListItems()
        {
            var completedNeuralNetworks = await neuralNetworkService.GetAllCompleted();
            var neuralNetworksSelectList = mapper.Map<List<SelectListItem>>(completedNeuralNetworks);
            return neuralNetworksSelectList;
        }
    }
}