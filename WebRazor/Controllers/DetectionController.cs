using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceDetection;
using Domain.FaceDetection.DTO;
using Domain.Files.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using WebRazor.Models.Detection;

namespace WebRazor.Controllers
{
    public class DetectionController : Controller
    {
        private readonly IDetectionResultService detectionResultService;
        private readonly IFaceDetectionService faceDetectionService;
        private readonly IMapper mapper;
        private readonly ILogger<DetectionController> logger;

        public DetectionController(IDetectionResultService detectionResultService, IFaceDetectionService faceDetectionService, IMapper mapper,
            ILogger<DetectionController> logger)
        {
            this.detectionResultService = detectionResultService;
            this.faceDetectionService = faceDetectionService;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await faceDetectionService.GetAllFaceDetectionsAsync();
            var model = new DetectionsModel
            {
                DetectionRequests = requests
            };
            return View(model);
        }

        public async Task<IActionResult> Request(int id)
        {
            var request = await faceDetectionService.GetRequestData(id);
            return View(request);
        }

        public async Task<IActionResult> New()
        {
            return View();
        }

        //[HttpGet("[action]")]
        //public async Task<DetectionRequest> GetRequest(int id)
        //{
        //    var request = await faceDetectionService.GetRequestData(id);
        //    return request;
        //}

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(IFormCollection collections)
        {
            try
            {
                var files = mapper.Map<IEnumerable<FileToUpload>>(collections.Files);
                collections.TryGetValue("name", out var requestName);

                var response = await faceDetectionService.CreateRequest(new NewRequest
                {
                    Name = requestName,
                    Files = files
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"error");
            }
            
            return Ok(new {task_Id = 1});
        }

        //[HttpGet("[action]")]
        //public async Task<IEnumerable<DetectionResultOutput>> GetRequestResults(int id)
        //{
        //    var request = await detectionResultService.GetResultsForRequest(id);
        //    return request;
        //}
    }
}