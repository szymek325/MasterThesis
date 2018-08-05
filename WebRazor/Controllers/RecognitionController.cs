using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceRecognition;
using Domain.FaceRecognition.DTO;
using Domain.Files.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebRazor.Models.Detection;
using WebRazor.Models.Recognition;

namespace WebRazor.Controllers
{
    public class RecognitionController : Controller
    {
        private readonly IFaceRecognitionService faceRecognitionService;
        private readonly IMapper mapper;
        private readonly ILogger<RecognitionController> logger;

        public RecognitionController(IFaceRecognitionService faceRecognitionService, IMapper mapper, ILogger<RecognitionController> logger)
        {
            this.faceRecognitionService = faceRecognitionService;
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

        public IActionResult NewRecognition()
        {
            return View();
        }

        public async Task<IActionResult> Create(NewDetectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("NewRecognition", model);
            }
            try
            {
                var file = mapper.Map<FileToUpload>(model.File);

                var response = await faceRecognitionService.CreateRequest(new NewRequest
                {
                    Name = model.Name,
                    Files = new List<FileToUpload> { file }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error");
            }

            return RedirectToAction("AllRecognitions", "Recognition", new { area = "" });
        }
    }
}