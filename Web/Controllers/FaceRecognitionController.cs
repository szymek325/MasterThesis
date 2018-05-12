using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceRecognition;
using Domain.FaceRecognition.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class FaceRecognitionController
    {
        private readonly IFaceRecognitionService faceDetectionService;
        private readonly IMapper mapper;

        public FaceRecognitionController(IFaceRecognitionService faceDetectionService, IMapper mapper)
        {
            this.faceDetectionService = faceDetectionService;
            this.mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<FaceRecoRequest>> GetAll()
        {
            return await faceDetectionService.GetAllFaceRecognitions();
        }
    }
}