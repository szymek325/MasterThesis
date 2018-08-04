using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceDetection;
using Domain.FaceDetection.DTO;
using Domain.Files.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebRazor.Models.Detection;

namespace WebRazor.Controllers
{
    public class DetectionController : Controller
    {
        private readonly IDetectionResultService detectionResultService;
        private readonly IFaceDetectionService faceDetectionService;
        private readonly ILogger<DetectionController> logger;
        private readonly IMapper mapper;

        public DetectionController(IDetectionResultService detectionResultService,
            IFaceDetectionService faceDetectionService, IMapper mapper,
            ILogger<DetectionController> logger)
        {
            this.detectionResultService = detectionResultService;
            this.faceDetectionService = faceDetectionService;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IActionResult> AllDetections()
        {
            var requests = await faceDetectionService.GetAllFaceDetectionsAsync();
            var model = new DetectionsViewModel
            {
                DetectionRequests = requests
            };
            return View(model);
        }

        public async Task<IActionResult> Detection(int id)
        {
            var request = await faceDetectionService.GetRequestData(id);
            return View(request);
        }

        public IActionResult NewDetection()
        {
            return View();
        }

        public async Task<IActionResult> Create(NewDetectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("NewDetection", model);
            }
            try
            {
                var file = mapper.Map<FileToUpload>(model.File);
                
                var response = await faceDetectionService.CreateRequest(new NewRequest
                {
                    Name = model.Name,
                    Files = new List<FileToUpload> {file}
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error");
            }

           return RedirectToAction("AllDetections", "Detection", new { area = "" });
        }
    }
}