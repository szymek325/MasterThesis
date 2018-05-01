﻿using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper mapper;

        public FaceDetectionController(IFaceDetectionService faceDetectionService, IMapper mapper)
        {
            this.faceDetectionService = faceDetectionService;
            this.mapper = mapper;
        }

        [HttpGet("[action]")]
        public IEnumerable<FaceDetectionRequest> GetAll()
        {
            return faceDetectionService.GetAllFaceDetections();
        }

        [HttpGet("[action]")]
        public IEnumerable<FaceDetectionRequest> GetRequest(int id)
        {

            return faceDetectionService.GetAllFaceDetections();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(IFormCollection collections)
        {
            var files = mapper.Map<IEnumerable<FileToUpload>>(collections.Files);
            collections.TryGetValue("name", out var requestName);

            var response=await faceDetectionService.CreateRequest(new NewRequest
            {
                Name = requestName,
                Files = files
            });
            return Ok(new {task_Id = response });
        }
    }
}