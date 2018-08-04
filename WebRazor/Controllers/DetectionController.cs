using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceDetection;
using Domain.FaceDetection.DTO;
using Domain.Files.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebRazor.Models.Detection;

namespace WebRazor.Controllers
{
    public class DetectionController : Controller
    {
        private readonly IFaceDetectionService faceDetectionService;
        private readonly IDetectionResultService detectionResultService;
        private readonly IMapper mapper;

        public DetectionController(IFaceDetectionService faceDetectionService,
            IDetectionResultService detectionResultService, IMapper mapper)
        {
            this.faceDetectionService = faceDetectionService;
            this.detectionResultService = detectionResultService;
            this.mapper = mapper;
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

        //[HttpGet("[action]")]
        //public async Task<DetectionRequest> GetRequest(int id)
        //{
        //    var request = await faceDetectionService.GetRequestData(id);
        //    return request;
        //}

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Create(IFormCollection collections)
        //{
        //    var files = mapper.Map<IEnumerable<FileToUpload>>(collections.Files);
        //    collections.TryGetValue("name", out var requestName);

        //    var response = await faceDetectionService.CreateRequest(new NewRequest
        //    {
        //        Name = requestName,
        //        Files = files
        //    });
        //    return Ok(new {task_Id = response});
        //}

        //[HttpGet("[action]")]
        //public async Task<IEnumerable<DetectionResultOutput>> GetRequestResults(int id)
        //{
        //    var request = await detectionResultService.GetResultsForRequest(id);
        //    return request;
        //}
    }
}