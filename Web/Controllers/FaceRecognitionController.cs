using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceRecognition;
using Domain.FaceRecognition.DTO;
using Domain.Files.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewRequest = Domain.FaceDetection.DTO.NewRequest;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class FaceRecognitionController:Controller
    {
        private readonly IFaceRecognitionService faceRecognitionService;
        private readonly IMapper mapper;
        ;
        public FaceRecognitionController(IFaceRecognitionService faceRecognitionService, IMapper mapper)
        {
            this.faceRecognitionService = faceRecognitionService;
            this.mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<FaceRecoRequest>> GetAll()
        {
            return await faceRecognitionService.GetAllFaceRecognitions();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(IFormCollection collections)
        {
            var files = mapper.Map<IEnumerable<FileToUpload>>(collections.Files);
            collections.TryGetValue("name", out var requestName);
            collections.TryGetValue("neuralNetworkId", out var neuralNetworkId);

            var response = await faceRecognitionService.CreateRequest(new Domain.FaceRecognition.DTO.NewRequest
            {
                Name = requestName,
                NeuralNetworkId = int.Parse(neuralNetworkId),
                Files = files
            });
            return Ok(new { faceRecognitionId = response });
        }

        [HttpGet("[action]")]
        public async Task<FaceRecoRequest> GetRequest(int id)
        {
            var request = await faceRecognitionService.GetRequestDataAsync(id);
            return request;
        }
    }
}