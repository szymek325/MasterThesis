using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceDetection;
using Domain.FaceDetection.DTO;
using Domain.Files.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class FaceDetectionController : Controller
    {
        private readonly IFaceDetectionService faceDetectionService;
        private readonly IDetectionResultService detectionResultService;
        private readonly IMapper mapper;

        public FaceDetectionController(IFaceDetectionService faceDetectionService,
            IDetectionResultService detectionResultService, IMapper mapper)
        {
            this.faceDetectionService = faceDetectionService;
            this.detectionResultService = detectionResultService;
            this.mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<DetectionRequest>> GetAll()
        {
            return await faceDetectionService.GetAllFaceDetectionsAsync();
        }

        [HttpGet("[action]")]
        public async Task<DetectionRequest> GetRequest(int id)
        {
            var request = await faceDetectionService.GetRequestData(id);
            return request;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(IFormCollection collections)
        {
            var files = mapper.Map<IEnumerable<FileToUpload>>(collections.Files);
            collections.TryGetValue("name", out var requestName);

            var response = await faceDetectionService.CreateRequest(new NewRequest
            {
                Name = requestName,
                Files = files
            });
            return Ok(new {task_Id = response});
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<DetectionResultOutput>> GetRequestResults(int id)
        {
            var request = await detectionResultService.GetResultsForRequest(id);
            return request;
        }
    }
}