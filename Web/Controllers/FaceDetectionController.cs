using System.Collections.Generic;
using Domain.FaceDetection;
using Domain.FaceDetection.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class FaceDetectionController : Controller
    {
        private readonly IFaceDetectionService faceDetectionService;

        public FaceDetectionController(IFaceDetectionService faceDetectionService)
        {
            this.faceDetectionService = faceDetectionService;
        }

        [HttpGet("[action]")]
        public IEnumerable<FaceDetectionRequest> GetAll(IFormCollection collections)
        {
            return faceDetectionService.GetAllFaceDetections();
        }
    }
}